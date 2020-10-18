using AgroSup.Core.Domain;
using AgroSup.Core.Repositories;
using AgroSup.WebApp.ViewModels.AdminPanel.Plants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgroSup.WebApp.Controllers
{
    [RequestFormLimits(ValueCountLimit = 5000)]
    [Authorize(Roles = "Administrator")]
    public class AdminPlantsController : Controller
    {
        private readonly IPlantRepository _plantRepository;

        public AdminPlantsController(IPlantRepository plantRepository)
        {
            _plantRepository = plantRepository;
        }

        public async Task<IActionResult> Index()
        {
            var plants = await _plantRepository.GetAll();
            var model = plants.Select(x => new PlantViewModel
            {
                Id = x.Id,
                EfaNitrogenRate = x.EfaNitrogenRate,
                Name = x.Name,
                Delete = false
            }).ToList();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Index(IEnumerable<PlantViewModel> plants)
        {
            if (!ModelState.IsValid)
            {
                TempData["message"] = "Błąd: Wystąpił problem. Nie wprowadzono zmian";
                return View(plants);
            }

            var PlantsToSave = plants.Select(x => new Plant
            {
                Id = x.Id,
                EfaNitrogenRate = x.EfaNitrogenRate,
                Name = x.Name,
            });


            foreach (var item in PlantsToSave)
            {
                await _plantRepository.Update(item);
            }

            var plantsToDelete = plants.Where(x => x.Delete == true);

            foreach (var item in plantsToDelete)
            {
                var plant = await _plantRepository.GetById(item.Id);
                await _plantRepository.Delete(plant);
            }
            TempData["message"] = "Zmiany zapisano pomyślnie!";
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(PlantViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var plant = new Plant
            {
                EfaNitrogenRate = model.EfaNitrogenRate,
                Name = model.Name,
            };
            await _plantRepository.Add(plant);
            TempData["message"] = "Dodano nową rośline!";
            ModelState.Clear();
            return View();
        }

    }
}
