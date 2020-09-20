using AgroSup.Core.Domain;
using AgroSup.Core.Repositories;
using AgroSup.WebApp.ViewModels.Manages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgroSup.WebApp.Controllers
{
    [Authorize]
    public class ManagesController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly IFieldRepository _fieldRepository;
        private readonly IYearPlanRepository _yearPlanRepository;
        private readonly IOperatorRepository _operatorRepository;
        private readonly IPlantRepository _plantRepository;
        
        public ManagesController(
            UserManager<User> userManager,
            IUserRepository userRepository,
            IFieldRepository fieldRepository,
            IYearPlanRepository yearPlanRepository,
            IOperatorRepository operatorRepository,
            IPlantRepository plantRepository
            )
        {
            _userManager = userManager;
            _userRepository = userRepository;
            _fieldRepository = fieldRepository;
            _yearPlanRepository = yearPlanRepository;
            _operatorRepository = operatorRepository;
            _plantRepository = plantRepository;
        }

        public async Task<IActionResult> CropPlan()
        {
            var managedYearPlan = await GetManagedYearPlan();

            if (managedYearPlan == null)
            {
                return RedirectToAction("Index", "YearPlan");
            }
            
            var plants = GetUser().Result.ChoosedPlants;

            var fields = await _fieldRepository.GetByYearPlan(managedYearPlan);
            var yearPlan2 = await _yearPlanRepository.GetByYearBack(managedYearPlan, 2);
            var fields2 = await _fieldRepository.GetByYearPlan(yearPlan2);
            var yearPlan1 = await _yearPlanRepository.GetByYearBack(managedYearPlan, 1);
            var fields1 = await _fieldRepository.GetByYearPlan(yearPlan1);

            CropPlanViewModel model = new CropPlanViewModel()
            {
                StartYear = managedYearPlan.StartYear,
                EndYear = managedYearPlan.EndYear,
                Fields = fields.Select(x => new FieldViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Area = x.GetArea,
                    PlantId = x.Plant != null ? x.Plant.Id : Guid.NewGuid(),
                    PlantName2 = Field.GetPlantName(fields2,x),
                    PlantName1 = Field.GetPlantName(fields1,x),
                    PlantVariety = x.PlantVariety,
                }).ToList(),
                Plants = plants.Select(x=>new SelectListItem()
                {
                    Text = x.Plant.Name,
                    Value = x.Plant.Id.ToString(),
                })
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CropPlan(CropPlanViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            
            var managedYearPlan = await GetManagedYearPlan();

            if (managedYearPlan == null)
            {
                return RedirectToAction("Index", "YearPlan");
            }
            foreach(var item in model.Fields)
            {
                var field = await _fieldRepository.GetById(item.Id);
                var plant = await _plantRepository.GetById(item.PlantId);
                field.Plant = plant;
                field.PlantVariety = item.PlantVariety;
                await _fieldRepository.Update(field);
            }
            return RedirectToAction("CropPlan");
        }

        public async Task<IActionResult> Summary()
        {
            var managedYearPlan = await GetManagedYearPlan();

            if (managedYearPlan == null)
            {
                return RedirectToAction("Index", "YearPlan");
            }

            var operators = await _operatorRepository.GetByYearPlan(managedYearPlan);

            var withoutOperator = new SummaryDisplayGroup()
            {
                Name = "Bez dopłat",
                ShowEfa = false,
                FuelArea = 2,
                NotEstabilishedArea = 5,
                TotalArea = 5,
            };

            var model = new SummaryViewModel()
            {
                DisplayGroups = operators.Select(x => new SummaryDisplayGroup()
                {
                    Name = x.GetName,
                    TotalArea = x.GetTotalArea(),
                    NotEstabilishedArea = x.GetNotStabilishedArea(),
                    FuelArea = x.GetFuelArea(),
                    ShowEfa = true,
                    EfaPercent = x.GetEfaPercent(),
                    Plants = x.GetPlantsList().Select(y => new SummaryPlant()
                    {
                        Name = y.Name,
                        Area = x.GetAreaByPlant(y),
                        AreaPercent = (int)(100/x.GetTotalArea() * x.GetAreaByPlant(y))
                    })
                })
            };

            return View(model);
        }

        public async Task<IActionResult> ParcelSummary()
        {
            var managedYearPlan = await GetManagedYearPlan();

            if (managedYearPlan == null)
            {
                return RedirectToAction("Index", "YearPlan");
            }

            var operators = await _operatorRepository.GetByYearPlan(managedYearPlan);
            var fields = await _fieldRepository.GetByYearPlan(managedYearPlan);
            IList<Parcel> parcels = new List<Parcel>();

            fields.ToList().ForEach(x =>
            {
                x.Parcels.ForEach(y => parcels.Add(y));
            });
            
            var user = await GetUser();
            var userPlants = user.ChoosedPlants;

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
                Parcels = parcels.Select(x=>new ParcelSummaryParcel()
                {
                    FieldName = x.Field.Name,
                    CultivatedArea = x.GetCultivatedArea(),
                    FuelApplication = x.GetFuelApplication(),
                    Number = x.Number,
                    OperatorName = x.GetOperatorName(),
                    PlantName = x.GetPlantName(),
                })
            };
            return View(model);
        }


        // Methods
        private async Task<YearPlan> GetManagedYearPlan()
        {
            var loggedUserId = Guid.Parse(_userManager.GetUserId(User));
            var loggedUser = await _userRepository.GetById(loggedUserId);
            var managedYearPlan = loggedUser.ManagedYearPlan;
            return managedYearPlan;
        }
        private async Task<User> GetUser()
        {
            var loggedUserId = Guid.Parse(_userManager.GetUserId(User));
            var loggedUser = await _userRepository.GetById(loggedUserId);
            return loggedUser;
        }
    }
}
