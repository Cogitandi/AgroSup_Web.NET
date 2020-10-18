using AgroSup.Core.Domain;
using AgroSup.Core.Repositories;
using AgroSup.WebApp.ViewModels.Plants;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgroSup.WebApp.Controllers
{
    public class PlantsController : BaseController
    {
        private readonly IPlantRepository _plantRepository;

        public PlantsController(
            IPlantRepository plantRepository,
            UserManager<User> userManager,
            IUserRepository userRepository
            ) : base(userManager, userRepository)
        {
            _plantRepository = plantRepository;

        }

        public async Task<IActionResult> ChoosePlants()
        {
            var userPlants = LoggedUser.ChoosedPlants;

            var model = new PlantViewModel()
            {
                AvailablePlants = await GetAvailablePlants(),
                SelectedPlants = userPlants.Select(x => x.Plant.Id.ToString()).ToList(),
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ChoosePlants(PlantViewModel model)
        {
            if (ModelState.IsValid)
            {
                var choosedPlants = model.SelectedPlants.Select(x => _plantRepository.GetById(Guid.Parse(x)).Result);
                var PlantsToUser = choosedPlants.Select(x => new UserPlant()
                {
                    Plant = x
                }).ToList();

                LoggedUser.ChoosedPlants = PlantsToUser;
                await UpdateLoggedUser();
                TempData["message"] = "Pomyślnie zapisano zmiany";
                return RedirectToAction("ChoosePlants");
            }

            return View(model);
        }

        // Methods
        private async Task<IList<SelectListItem>> GetAvailablePlants()
        {
            var plants = await _plantRepository.GetAll();
            return plants.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
            }).ToList();
        }

    }
}
