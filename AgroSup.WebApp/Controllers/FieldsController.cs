using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgroSup.Core.Domain;
using AgroSup.Core.Repositories;
using AgroSup.Infrastructure.Repositories;
using AgroSup.WebApp.ViewModels.Fields;
using AgroSup.WebApp.ViewModels.Operators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AgroSup.WebApp.Controllers
{
    [Authorize]
    public class FieldsController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly IFieldRepository _fieldRepository;
        private readonly IOperatorRepository _operatorRepository;

        public FieldsController(
            UserManager<User> userManager,
            IUserRepository userRepository,
            IFieldRepository fieldRepository,
            IOperatorRepository operatorRepository
            )
        {
            _userManager = userManager;
            _userRepository = userRepository;
            _fieldRepository = fieldRepository;
            _operatorRepository = operatorRepository;
        }

        public async Task<IActionResult> Index()
        {
            var managedYearPlan = await getManagedYearPlan();

            if (managedYearPlan == null)
            {
                return RedirectToAction("index", "yearplans");
            }

            var fields = await _fieldRepository.GetByYearPlan(managedYearPlan);

            var model = fields.Select(x => new FieldViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Number = x.Number,
                Area = x.GetFieldArea()/100f,
            });
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var managedYearPlan = await getManagedYearPlan();
            if (managedYearPlan == null)
            {
                return RedirectToAction("index", "yearplans");
            }
            var model = new FieldViewModel()
            {
                Number = await GetNumberForNewField(managedYearPlan),
            };
            var operatorSelectList = managedYearPlan.Operators.Select(x => new SelectListItem()
            {
                Text = x.GetName,
                Value = x.Id.ToString(),
            });
            ViewBag.OperatorSelectList = operatorSelectList;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FieldViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var managedYearPlan = await getManagedYearPlan();
            if (managedYearPlan == null)
            {
                return RedirectToAction("index", "yearplans");
            }

            Field field = new Field()
            {
                Name = model.Name,
                Number = model.Number,
                YearPlan = managedYearPlan,
                Parcels = model.Parcels.Select(x => new Parcel()
                {
                    Number = x.Number,
                    CultivatedArea = x.CultivatedArea,
                    FuelApplication = x.FuelApplication,
                    Operator = _operatorRepository.GetById(x.OperatorId).Result
                }).ToList()
            };
            await _fieldRepository.Add(field);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var field = await _fieldRepository.GetById(id);

            if (field == null)
            {
                return NotFound();
            }
            var managedYearPlan = await getManagedYearPlan();
            if (managedYearPlan == null)
            {
                return RedirectToAction("index", "yearplans");
            }

            var operators = managedYearPlan.Operators;

            var model = new FieldViewModel()
            {
                Id = field.Id,
                Name = field.Name,
                Number = field.Number,
                Parcels = field.Parcels.Select(x => new ParcelViewModel()
                {
                    Number = x.Number,
                    CultivatedArea = x.CultivatedArea,
                    FuelApplication = x.FuelApplication,
                    OperatorId = x.Operator != null ? x.Operator.Id : Guid.NewGuid(),
                }).ToList()
            };

            var operatorSelectList = managedYearPlan.Operators.Select(x => new SelectListItem()
            {
                Text = x.GetName,
                Value = x.Id.ToString(),
            });
            ViewBag.OperatorSelectList = operatorSelectList;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(FieldViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var field = await _fieldRepository.GetById(model.Id);
            field.Name = model.Name;
            field.Number = model.Number;
            field.Parcels = model.Parcels.Select(x => new Parcel()
            {
                CultivatedArea = x.CultivatedArea,
                Number = x.Number,
                FuelApplication = x.FuelApplication,
                Operator = _operatorRepository.GetById(x.OperatorId).Result
            }
            ).ToList();
            await _fieldRepository.Update(field);
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var field = await _fieldRepository.GetById(id);
            await _fieldRepository.Delete(field);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddParcel(FieldViewModel model)
        {
            var managedYearPlan = await getManagedYearPlan();
            if (managedYearPlan == null)
            {
                return RedirectToAction("index", "yearplans");
            }
            var operatorSelectList = managedYearPlan.Operators.Select(x => new SelectListItem()
            {
                Text = x.GetName,
                Value = x.Id.ToString(),
            });
            ViewBag.OperatorSelectList = operatorSelectList;
            model.Parcels.Add(new ParcelViewModel());
            return PartialView("Parcels", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveParcel(FieldViewModel model, int index)
        {
            var managedYearPlan = await getManagedYearPlan();
            if (managedYearPlan == null)
            {
                return RedirectToAction("index", "yearplans");
            }
            ModelState.Clear();
            model.Parcels.RemoveAt(index);
            var operatorSelectList = managedYearPlan.Operators.Select(x => new SelectListItem()
            {
                Text = x.GetName,
                Value = x.Id.ToString(),
            });
            ViewBag.OperatorSelectList = operatorSelectList;
            return PartialView("Parcels", model);
        }
        [HttpPost]
        public async Task<IActionResult> ChangeNumber(Guid fieldId, int position)
        {
            // position = 1 - change fieldId number to up
            // position = 2 - change fieldId number to down
            var mainField = await _fieldRepository.GetById(fieldId);
            var fieldToChangeNumberWith = position == 1 ? await _fieldRepository.GetPrevious(fieldId): await _fieldRepository.GetNext(fieldId);
            await SwapFieldNumbers(mainField, fieldToChangeNumberWith);
            return RedirectToAction("Index");
        }

        // Methods
        private async Task SwapFieldNumbers(Field a, Field b)
        {
            var temp = a.Number;
            a.Number = b.Number;
            b.Number = temp;
            await _fieldRepository.Update(a);
            await _fieldRepository.Update(b);
        }
        private async Task<YearPlan> getManagedYearPlan()
        {
            var loggedUserId = Guid.Parse(_userManager.GetUserId(User));
            var loggedUser = await _userRepository.GetById(loggedUserId);
            var managedYearPlan = loggedUser.ManagedYearPlan;
            return managedYearPlan;
        }

        private IEnumerable<Parcel> getParcelsFromModel(FieldViewModel model)
        {
            return model.Parcels.Select(x => new Parcel()
            {
                Number = x.Number,
                CultivatedArea = x.CultivatedArea,
                FuelApplication = x.FuelApplication,
                //Operator = x.OperatorI,
            });
        }
        private IEnumerable<ParcelViewModel> getParcelsModelFromDomain(Field domain)
        {
            return domain.Parcels.Select(x => new ParcelViewModel()
            {
                Number = x.Number,
                CultivatedArea = x.CultivatedArea,
                FuelApplication = x.FuelApplication,
                //Operator = x.Operator,
            });
        }
        private async Task<int> GetNumberForNewField(YearPlan yearPlan)
        {
            var yearPlanFields = await _fieldRepository.GetByYearPlan(yearPlan);
            if(yearPlanFields.Count()>0)
            {
                return yearPlanFields.Max(x => x.Number)+1;
            }
            return 1;
        }

        // Validation
        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> UniqueFieldNumber(FieldViewModel model)
        {
            var managedYearPlan = await getManagedYearPlan();
            // Get list of user's yearplans
            var userYearPlans = await _fieldRepository.GetByYearPlan(managedYearPlan);
            var list = userYearPlans.ToList();

            foreach (var item in list)
            {
                if (item.Number == model.Number)
                {
                    // If user have yearplan with startYear passed by user error
                    return Json($"Posiadasz już pole z tym numerem");
                }
            }
            return Json(true);
        }

    }
}
