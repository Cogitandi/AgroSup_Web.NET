using AgroSup.Core.Domain;
using AgroSup.Core.Repositories;
using AgroSup.WebApp.ViewModels.YearPlans;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AgroSup.WebApp.Controllers
{
    public class YearPlansController : BaseController
    {
        private readonly IYearPlanRepository _yearPlanRepository;

        public YearPlansController(
            UserManager<User> userManager,
            IUserRepository userRepository,
            IYearPlanRepository yearPlanRepository) : base(userManager, userRepository)
        {
            _yearPlanRepository = yearPlanRepository;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userYearPlans = await _yearPlanRepository.GetByUser(LoggedUser);
            var model = userYearPlans.Select(x => new YearPlanViewModel
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
            var userYearPlans = await _yearPlanRepository.GetByUser(LoggedUser);

            ViewBag.YearPlans = new SelectList(userYearPlans, "Id", "GetYearPlanName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(YearPlanViewModel model, Guid yearPlanImportId)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var yearPlan = new YearPlan()
            {
                StartYear = model.StartYear,
                EndYear = model.StartYear + 1,
                User = LoggedUser,
            };

            var yearPlanImport = await _yearPlanRepository.GetByIdToImport(yearPlanImportId);
            if (yearPlanImport != null)
            {
                yearPlan.GetDataToImport(yearPlanImport);
            }
            await _yearPlanRepository.Add(yearPlan);
            TempData["message"] = "Utworzono nowy plan: " + yearPlan.GetYearPlanName;
            ModelState.Clear();
            return View();

        }
        public async Task<IActionResult> SetManagedYearPlan(Guid id)
        {
            var yearPlan = await _yearPlanRepository.GetById(id);
            LoggedUser.ManagedYearPlan = yearPlan;
            await UpdateLoggedUser();
            TempData["message"] = "Zarządzasz teraz: " + yearPlan.GetYearPlanName;
            return RedirectToAction("Index");
        }

        // Validation
        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> UniqueYearPlan(YearPlanViewModel model)
        {
            // Get list of user's yearplans
            var userYearPlans = await _yearPlanRepository.GetByUser(LoggedUser);
            var list = userYearPlans.ToList();

            foreach (var item in list)
            {
                if (item.StartYear == model.StartYear)
                {
                    // If user have yearplan with startYear passed by user error
                    return Json($"Posiadasz już plan na ten rok");
                }
            }
            return Json(true);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            if (ManagedYearPlan == null)
            {
                return ActionIfNotChoosedManagedYearPlan();
            }
            var field = await _yearPlanRepository.GetById(id);
            await _yearPlanRepository.Delete(field);
            return RedirectToAction("Index");
        }



    }
}
