using AgroSup.Core.Domain;
using AgroSup.Core.Repositories;
using AgroSup.WebApp.ViewModels.Manages.CropPlan;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AgroSup.WebApp.Controllers
{
    public class CropPlanController : BaseController
    {
        private readonly IFieldRepository _fieldRepository;
        private readonly IYearPlanRepository _yearPlanRepository;
        private readonly IPlantRepository _plantRepository;

        public CropPlanController(
            UserManager<User> userManager,
            IUserRepository userRepository,
            IFieldRepository fieldRepository,
            IYearPlanRepository yearPlanRepository,
            IPlantRepository plantRepository) : base(userManager, userRepository)
        {

            _fieldRepository = fieldRepository;
            _yearPlanRepository = yearPlanRepository;
            _plantRepository = plantRepository;
        }
        public async Task<IActionResult> Index()
        {
            if (ManagedYearPlan == null)
            {
                return ActionIfNotChoosedManagedYearPlan();
            }

            var plants = LoggedUser.ChoosedPlants;


            var yearPlan2 = await _yearPlanRepository.GetByYearBack(ManagedYearPlan, 2);
            var yearPlan1 = await _yearPlanRepository.GetByYearBack(ManagedYearPlan, 1);

            var fields = await _fieldRepository.GetByYearPlan(ManagedYearPlan);
            var fields1 = await _fieldRepository.GetByYearPlan(yearPlan1);
            var fields2 = await _fieldRepository.GetByYearPlan(yearPlan2);

            CropPlanViewModel model = new CropPlanViewModel()
            {
                StartYear = ManagedYearPlan.StartYear,
                EndYear = ManagedYearPlan.EndYear,
                Fields = fields.Select(x => new FieldViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Area = x.GetFieldArea() / 100f,
                    PlantId = x.Plant != null ? x.Plant.Id : Guid.NewGuid(),
                    PlantName2 = Field.GetPlantName(fields2, x),
                    PlantName1 = Field.GetPlantName(fields1, x),
                    PlantVariety = x.PlantVariety,
                }).ToList(),

                Plants = plants.Select(x => new SelectListItem()
                {
                    Text = x.Plant.Name,
                    Value = x.Plant.Id.ToString(),
                })
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CropPlanViewModel model)
        {
            if (ManagedYearPlan == null)
            {
                return ActionIfNotChoosedManagedYearPlan();
            }

            if (model.Fields == null)
            {
                return RedirectToAction("Index");
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            foreach (var item in model.Fields)
            {
                var field = await _fieldRepository.GetById(item.Id);
                var plant = await _plantRepository.GetById(item.PlantId);
                field.Plant = plant;
                field.PlantVariety = item.PlantVariety;
                await _fieldRepository.Update(field);
            }
            TempData["message"] = "Pomyślnie zapisano zmiany";
            return RedirectToAction("Index");
        }

    }
}
