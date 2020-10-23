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
        public async Task AssignPlant(Guid id)
        {
            var plant = await _plantRepository.GetById(id);
            if(plant != null)
            {
                LoggedUser.AddPlantToChoosed(plant);
            await UpdateLoggedUser();
            }
        }
        [HttpPost]
        public async Task UnAssignPlant(Guid id)
        {
            var plant = await _plantRepository.GetById(id);
            if (plant != null)
            {
                LoggedUser.RemovePlantFromChoosed(plant);
                await UpdateLoggedUser();
            }
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
