using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgroSup.Core.Domain;
using AgroSup.Core.Repositories;
using AgroSup.WebApp.ViewModels.AdminPanel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgroSup.WebApp.Controllers
{
    [RequestFormLimits(ValueCountLimit = 5000)]
    [Authorize(Roles = "Administrator")]
    public class AdminPanelController : Controller
    {
        private readonly IPlantRepository _plantRepository;

        public AdminPanelController(IPlantRepository plantRepository)
        {
            _plantRepository = plantRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Plants()
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
        public IActionResult NewPlant()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> NewPlant(PlantViewModel model)
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
            return RedirectToAction("Plants");
        }
        [HttpPost]
        public async Task<IActionResult> SavePlants(IEnumerable<PlantViewModel> plants)
        {
            var PlantsToSave = plants.Select(x => new Plant
            {
                Id = x.Id,
                EfaNitrogenRate = x.EfaNitrogenRate,
                Name = x.Name,
            });


            foreach(var item in PlantsToSave)
            {
                await _plantRepository.Update(item);
            }

            var plantsToDelete = plants.Where(x => x.Delete == true);

            foreach(var item in plantsToDelete)
            {
                var plant = await _plantRepository.GetById(item.Id);
                await _plantRepository.Delete(plant);
            }

            return RedirectToAction("Plants");
        }
    }
}
