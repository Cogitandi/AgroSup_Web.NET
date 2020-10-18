using AgroSup.Core.Domain;
using AgroSup.Core.Repositories;
using AgroSup.WebApp.ViewModels.Manages;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgroSup.WebApp.Controllers
{
    public class ManagesController : BaseController
    {
        private readonly IFieldRepository _fieldRepository;
        private readonly IOperatorRepository _operatorRepository;

        public ManagesController(
            UserManager<User> userManager,
            IUserRepository userRepository,
            IFieldRepository fieldRepository,
            IOperatorRepository operatorRepository
            ) : base(userManager, userRepository)
        {
            _fieldRepository = fieldRepository;
            _operatorRepository = operatorRepository;
        }

        public async Task<IActionResult> Summary()
        {
            if (ManagedYearPlan == null)
            {
                return ActionIfNotChoosedManagedYearPlan();
            }

            var fields = await _fieldRepository.GetByYearPlan(ManagedYearPlan);
            var operators = await _operatorRepository.GetByYearPlan(ManagedYearPlan);

            var withoutOperator = new SummaryDisplayGroup()
            {
                Name = "Bez ARiMR",
                ShowEfa = false,
                FuelArea = Field.GetFuelAreaWithoutOperator(fields) / 100f,
                TotalArea = Field.GetCultivatedAreaWithoutOperator(fields) / 100f,
                Plants = Field.GetPlantNameListWithoutOperator(fields).Select(x => new SummaryPlant()
                {
                    Name = x,
                    Area = Field.GetCultivatedAreaByPlantNameWithoutOperator(fields, x) / 100f,
                    AreaPercent = (int)(100 * Field.GetCultivatedAreaByPlantNameWithoutOperator(fields, x) / Field.GetCultivatedAreaWithoutOperator(fields))
                })
            };
            var TotalFarm = new SummaryDisplayGroup()
            {
                Name = "Całość",
                ShowEfa = false,
                FuelArea = Field.GetTotalFuelArea(fields) / 100f,
                TotalArea = Field.GetTotalCultivatedArea(fields) / 100f,
                Plants = Field.GetTotalPlantNameList(fields).Select(x => new SummaryPlant()
                {
                    Name = x,
                    Area = Field.GetTotalCultivatedAreaByPlantName(fields, x) / 100f,
                    AreaPercent = (int)(100 * Field.GetTotalCultivatedAreaByPlantName(fields, x) / Field.GetTotalCultivatedArea(fields))
                })
            };

            var model = new SummaryViewModel()
            {
                DisplayGroups = operators.Select(x => new SummaryDisplayGroup()
                {
                    Name = x.GetName,
                    TotalArea = x.GetTotalArea() / 100f,
                    FuelArea = x.GetFuelArea() / 100f,
                    ShowEfa = true,
                    EfaPercent = x.GetEfaPercent(),
                    Plants = x.GetPlantNameList().Select(y => new SummaryPlant()
                    {
                        Name = y,
                        Area = x.GetAreaByPlant(y) / 100f,
                        AreaPercent = (int)(100 * x.GetAreaByPlant(y) / x.GetTotalArea())
                    })
                }).Append(withoutOperator).Append(TotalFarm)
            };

            return View(model);
        }

        public async Task<IActionResult> ParcelSummary()
        {
            if (ManagedYearPlan == null)
            {
                return ActionIfNotChoosedManagedYearPlan();
            }

            var operators = await _operatorRepository.GetByYearPlan(ManagedYearPlan);
            var fields = await _fieldRepository.GetByYearPlan(ManagedYearPlan);
            IList<Parcel> parcels = new List<Parcel>();

            foreach (var field in fields)
            {
                foreach (var parcel in field.Parcels)
                {
                    parcels.Add(parcel);
                }
            }
            var userPlants = LoggedUser.ChoosedPlants;

            var model = new ParcelSummaryViewModel()
            {
                OperatorSelectList = operators.Select(x => new SelectListItem()
                {
                    Text = x.GetName,
                    Value = x.GetName
                }),
                PlantSelectList = userPlants.Select(x => new SelectListItem()
                {
                    Text = x.Plant.Name,
                    Value = x.Plant.Name
                }),
                Parcels = parcels.Select(x => new ParcelSummaryParcel()
                {
                    FieldName = x.Field.Name,
                    CultivatedArea = x.CultivatedArea / 100f,
                    FuelApplication = x.GetFuelApplication(),
                    Number = x.Number,
                    OperatorName = x.GetOperatorName(),
                    PlantName = x.GetPlantName(),
                })
            };
            return View(model);
        }

        public async Task<IActionResult> FieldSummary()
        {
            if (ManagedYearPlan == null)
            {
                return ActionIfNotChoosedManagedYearPlan();
            }

            var operators = await _operatorRepository.GetByYearPlan(ManagedYearPlan);
            var fields = await _fieldRepository.GetByYearPlan(ManagedYearPlan);
            IList<Parcel> parcels = new List<Parcel>();

            foreach (var field in fields)
            {
                foreach (var parcel in field.Parcels)
                {
                    parcels.Add(parcel);
                }
            }

            var userPlants = LoggedUser.ChoosedPlants;

            var model = new FieldSummaryViewModel()
            {
                OperatorSelectList = operators.Select(x => new SelectListItem()
                {
                    Text = x.GetName,
                    Value = x.GetName
                }),
                PlantSelectList = userPlants.Select(x => new SelectListItem()
                {
                    Text = x.Plant.Name,
                    Value = x.Plant.Name
                }),
                Parcels = parcels.Select(x => new FieldSummaryParcel()
                {
                    FieldNumber = x.Field.Number,
                    FieldName = x.Field.Name,
                    CultivatedArea = x.CultivatedArea / 100f,
                    FuelApplication = x.GetFuelApplication(),
                    Number = x.Number,
                    OperatorName = x.GetOperatorName(),
                    PlantName = x.GetPlantName(),
                })
            };
            return View(model);
        }
    }
}
