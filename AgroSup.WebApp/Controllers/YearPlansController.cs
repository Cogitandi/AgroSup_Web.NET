using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgroSup.Core.Domain;
using AgroSup.Core.Repositories;
using AgroSup.WebApp.ViewModels.YearPlans;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AgroSup.WebApp.Controllers
{
    [Authorize]
    public class YearPlansController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IYearPlanRepository _yearPlanRepository;
        

        public YearPlansController(UserManager<User> userManager, IYearPlanRepository yearPlanRepository)
        {
            _userManager = userManager;
            _yearPlanRepository = yearPlanRepository;
        }

        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var loggedUser = await _userManager.GetUserAsync(User);
            var userYearPlans = await _yearPlanRepository.GetByUser(loggedUser);
            var model = userYearPlans.Select(x=> new YearPlanViewModel
            {
                Id = x.Id,
                StartYear = x.StartYear,
                EndYear = x.EndYear
            });
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var loggedUser = await _userManager.GetUserAsync(User);
            var userYearPlans = await _yearPlanRepository.GetByUser(loggedUser);
            //ViewBag.YearPlans = new SelectList(userYearPlans, "Id", "GetYearPlanName");
            var model = new YearPlanViewModel() { };
            model.AddYearPlansToSelect(userYearPlans);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(YearPlanViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            var loggedUser = await _userManager.GetUserAsync(User);

            YearPlan yearPlan = new YearPlan()
            {
                StartYear = model.StartYear,
                EndYear = model.StartYear + 1,
                User = loggedUser
            };
           await _yearPlanRepository.Create(yearPlan);
            return RedirectToAction("Index");
       
        }
    }
}
