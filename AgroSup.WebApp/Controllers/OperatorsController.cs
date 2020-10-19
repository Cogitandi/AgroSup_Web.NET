using AgroSup.Core.Domain;
using AgroSup.Core.Repositories;
using AgroSup.WebApp.ViewModels.Operators;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AgroSup.WebApp.Controllers
{
    public class OperatorsController : BaseController
    {
        private readonly IOperatorRepository _operatorRepository;

        public OperatorsController(
            UserManager<User> userManager,
            IUserRepository userRepository,
            IOperatorRepository operatorRepository
            ) : base(userManager, userRepository)
        {
            _operatorRepository = operatorRepository;
        }

        public async Task<IActionResult> Index()
        {
            if (ManagedYearPlan == null)
            {
                return ActionIfNotChoosedManagedYearPlan();
            }

            var operators = await _operatorRepository.GetByYearPlan(ManagedYearPlan);
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
            if (ManagedYearPlan == null)
            {
                return ActionIfNotChoosedManagedYearPlan();
            }

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OperatorViewModel model)
        {
            if (ManagedYearPlan == null)
            {
                return ActionIfNotChoosedManagedYearPlan();
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            Operator @operator = new Operator()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                ArimrNumber = model.ArimrNumber,
                YearPlan = ManagedYearPlan,
            };
            await _operatorRepository.Add(@operator);
            TempData["message"] = "Nowa osoba została dodana";
            ModelState.Clear();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            if (ManagedYearPlan == null)
            {
                return ActionIfNotChoosedManagedYearPlan();
            }

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
            if (ManagedYearPlan == null)
            {
                return ActionIfNotChoosedManagedYearPlan();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var @operator = await _operatorRepository.GetById(model.Id);
            @operator.FirstName = model.FirstName;
            @operator.LastName = model.LastName;
            @operator.ArimrNumber = model.ArimrNumber;
            await _operatorRepository.Update(@operator);
            TempData["message"] = "Zmiany zostały zapisane pomyślnie";
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (ManagedYearPlan == null)
            {
                return ActionIfNotChoosedManagedYearPlan();
            }

            var @operator = await _operatorRepository.GetById(id);
            await _operatorRepository.Delete(@operator);
            return RedirectToAction("Index");
        }
    }
}
