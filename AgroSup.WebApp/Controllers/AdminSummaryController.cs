using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgroSup.Core.Repositories;
using AgroSup.WebApp.ViewModels.AdminPanel.Summary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgroSup.WebApp.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminSummaryController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AdminSummaryController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Index()
        {
            var Users = await _userRepository.GetAll();
            var Model = new SummaryViewModel
            {
                TotalUsers = Users.Count(),
            };
            return View(Model);
        }
    }
}
