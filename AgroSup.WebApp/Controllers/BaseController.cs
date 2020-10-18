using AgroSup.Core.Domain;
using AgroSup.Core.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace AgroSup.WebApp.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;
        protected User LoggedUser;
        protected YearPlan ManagedYearPlan;

        public BaseController(UserManager<User> userManager, IUserRepository userRepository)
        {
            _userManager = userManager;
            _userRepository = userRepository;
            
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            LoggedUser = GetLoggedUser();
            ManagedYearPlan = GetManagedYearPlan(LoggedUser);
        }

        private User GetLoggedUser()
        {
            var loggedUserId = _userManager.GetUserId(User);
            //var loggedUserName = User.Identity.Name;
            var loggedUser = _userRepository.GetById(Guid.Parse(loggedUserId)).GetAwaiter().GetResult();
            return loggedUser;
        }
        protected async Task UpdateLoggedUser()
        {
            await _userRepository.Update(LoggedUser);
        }
        private YearPlan GetManagedYearPlan(User loggedUser)
        {
            var ManagedYearPlan = loggedUser.ManagedYearPlan;
            return ManagedYearPlan;
        }
        protected IActionResult ActionIfNotChoosedManagedYearPlan()
        {
            TempData["message"] = "Wybierz plan, którym chcesz zarządzać";
            return RedirectToAction("index", "yearplans");
        }
    }
}
