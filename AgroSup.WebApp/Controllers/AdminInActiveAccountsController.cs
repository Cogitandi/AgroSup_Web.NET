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
            var AllUsers = await _userRepository.GetAll();
            var selectedUsersAmount = AgroSup.Core.Domain.User.GetInActiveUsers(AllUsers).Count();

            var model = new InActiveAccountViewModel()
            {
                PossibleAccountToDelete = selectedUsersAmount
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteAccounts()
        {
            var AllUsers = await _userRepository.GetAll();
            var SelectedUsers = AgroSup.Core.Domain.User.GetInActiveUsers(AllUsers);
            await _userRepository.Delete(SelectedUsers);
            TempData["message"] = "Konta zostały usunięte!";
            return RedirectToAction("Index");
        }
    }
}
