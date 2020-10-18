using AgroSup.Core.Repositories;
using AgroSup.WebApp.ViewModels.AdminPanel.InActiveAccounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AgroSup.WebApp.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminInActiveAccountsController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AdminInActiveAccountsController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<IActionResult> Index()
        {
            // stworzone i nigdy nie zalogowane przez 90 dni
            // lub nie logowane od 90dni i nie utworzono nic
            int DaysWithoutLogin = 90;

            var AllUsers = await _userRepository.GetAll();
            var TodayDate = DateTime.Now;

            var selectedUsersAmount = AllUsers
                .Where(x => x.YearPlans.Count() == 0)
                .Where(x => TodayDate.Subtract(x.CreateDate).Days >= DaysWithoutLogin)
                .Where(x => x.LastLoginDate == null || TodayDate.Subtract(x.LastLoginDate).Days >= DaysWithoutLogin)
                .Count();

            var model = new InActiveAccountViewModel()
            {
                PossibleAccountToDelete = selectedUsersAmount
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteAccounts()
        {
            // stworzone i nigdy nie zalogowane przez 90 dni
            // lub nie logowane od 90dni i nie utworzono nic
            int DaysWithoutLogin = 90;

            var AllUsers = await _userRepository.GetAll();
            var TodayDate = DateTime.Now;

            var SelectedUsers = AllUsers
                .Where(x => x.YearPlans.Count() == 0)
                .Where(x => TodayDate.Subtract(x.CreateDate).Days >= DaysWithoutLogin)
                .Where(x => x.LastLoginDate == null || TodayDate.Subtract(x.LastLoginDate).Days >= DaysWithoutLogin)
                .ToList();
            await _userRepository.Delete(SelectedUsers);
            TempData["message"] = "Konta zostały usunięte!";
            return RedirectToAction("Index");
        }
    }
}
