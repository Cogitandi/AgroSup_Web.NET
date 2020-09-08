using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgroSup.Core.Domain;
using AgroSup.Core.Repositories;
using AgroSup.WebApp.ViewModels.Operators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AgroSup.WebApp.Controllers
{
    [Authorize]
    public class OperatorsController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly IOperatorRepository _operatorRepository;

        public OperatorsController(
            UserManager<User> userManager,
            IUserRepository userRepository,
            IOperatorRepository operatorRepository
            )
        {
            _userManager = userManager;
            _userRepository = userRepository;
            _operatorRepository = operatorRepository;
        }
        public async Task<IActionResult> Index()
        {
            var loggedUser = await _userManager.GetUserAsync(User);
            var operators = await _operatorRepository.GetByUser(loggedUser);
            var model = operators.Select(x => new OperatorViewModel()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                ArimrNumber = x.ArimrNumber
            });
            return View(model);
        }
    }
}
