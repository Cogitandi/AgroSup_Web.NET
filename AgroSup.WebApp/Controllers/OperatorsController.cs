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
            var managedYearPlan = await getManagedYearPlan();
            var operators = await _operatorRepository.GetByYearPlan(managedYearPlan);
            var model = operators.Select(x => new OperatorViewModel()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                ArimrNumber = x.ArimrNumber
            });
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OperatorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var managedYearPlan = await getManagedYearPlan();

            Operator @operator = new Operator()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                ArimrNumber = model.ArimrNumber,
                YearPlan = managedYearPlan,
            };
            await _operatorRepository.Add(@operator);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var @operator = await _operatorRepository.GetById(id);

            if (@operator == null)
            {
                return NotFound();
            }
            
            var model = new OperatorViewModel()
            {
                Id = @operator.Id,
                FirstName = @operator.FirstName,
                LastName = @operator.LastName,
                ArimrNumber = @operator.ArimrNumber
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(OperatorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var @operator = await _operatorRepository.GetById(model.Id);
            @operator.FirstName = model.FirstName;
            @operator.LastName = model.LastName;
            @operator.ArimrNumber = model.ArimrNumber;
            await _operatorRepository.Update(@operator);
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var @operator = await _operatorRepository.GetById(id);
            await _operatorRepository.Delete(@operator);
            return RedirectToAction("Index");
        }

        // Methods
        private async Task<YearPlan> getManagedYearPlan()
        {
            var loggedUserId = Guid.Parse(_userManager.GetUserId(User));
            var loggedUser = await _userRepository.GetById(loggedUserId);
            var managedYearPlan = loggedUser.ManagedYearPlan;
            return managedYearPlan;
        }
    }
}
