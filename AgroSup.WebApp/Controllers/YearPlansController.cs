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
    }
}
