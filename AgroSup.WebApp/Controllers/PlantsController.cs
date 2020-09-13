using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgroSup.Core.Domain;
using AgroSup.Core.Repositories;
using AgroSup.WebApp.ViewModels.Plants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AgroSup.WebApp.Controllers
{
    [Authorize]
    public class PlantsController : Controller
    {
        private readonly IPlantRepository _plantRepository;
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;

        public PlantsController(
            IPlantRepository plantRepository,
            UserManager<User> userManager,
            IUserRepository userRepository
            )
        {
            _plantRepository = plantRepository;
            _userManager = userManager;
            _userRepository = userRepository;

        }

        public async Task<IActionResult> ChoosePlants()
        {
            var loggedUser = await getLoggedUser();
            var userPlants = loggedUser.ChoosedPlants;

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
           if(ModelState.IsValid)
            {
                var loggedUser = await getLoggedUser();
                var choosedPlants = model.SelectedPlants.Select(x=> _plantRepository.GetById(Guid.Parse(x)).Result);
                var PlantsToUser = choosedPlants.Select(x => new UserPlant()
                {
                    Plant = x
                }).ToList();

                loggedUser.ChoosedPlants = PlantsToUser;
                await _userRepository.Update(loggedUser);
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

        private async Task<User> getLoggedUser()
        {
            var loggedUserId = Guid.Parse(_userManager.GetUserId(User));
            var loggedUser = await _userRepository.GetById(loggedUserId);
            return loggedUser;
        }
    }
}
