using AgroSup.Core.Domain;
using AgroSup.Core.Repositories;
using AgroSup.WebApp.ViewModels.Fields;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AgroSup.WebApp.Controllers
{
    public class FieldsController : BaseController
    {
        private readonly IFieldRepository _fieldRepository;
        private readonly IOperatorRepository _operatorRepository;

        public FieldsController(
            UserManager<User> userManager,
            IUserRepository userRepository,
            IFieldRepository fieldRepository,
            IOperatorRepository operatorRepository
            ) : base(userManager, userRepository)
        {
            _fieldRepository = fieldRepository;
            _operatorRepository = operatorRepository;
        }

        public async Task<IActionResult> Index()
        {
            if (ManagedYearPlan == null)
            {
                return ActionIfNotChoosedManagedYearPlan();
            }

            var fields = await _fieldRepository.GetByYearPlan(ManagedYearPlan);

            var model = fields.Select(x => new FieldViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Number = x.Number,
                Area = x.GetFieldArea() / 100f,
            });
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            if (ManagedYearPlan == null)
            {
                return ActionIfNotChoosedManagedYearPlan();
            }
            var model = new FieldViewModel()
            {
                Number = await GetNumberForNewField(ManagedYearPlan),
            };
            var operatorSelectList = ManagedYearPlan.Operators.Select(x => new SelectListItem()
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
            if (ManagedYearPlan == null)
            {
                return ActionIfNotChoosedManagedYearPlan();
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // check number is unique

            Field field = new Field()
            {
                Name = model.Name,
                Number = model.Number,
                YearPlan = ManagedYearPlan,
                Parcels = model.Parcels.Select(x => new Parcel()
                {
                    Number = x.Number,
                    CultivatedArea = x.CultivatedArea,
                    FuelApplication = x.FuelApplication,
                    Operator = _operatorRepository.GetById(x.OperatorId).Result
                }).ToList()
            };
            await _fieldRepository.Add(field);
            TempData["message"] = "Dodano nowe pole!";
            ModelState.Clear();
            return RedirectToAction("Create");
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            if (ManagedYearPlan == null)
            {
                return ActionIfNotChoosedManagedYearPlan();
            }

            // check id passed is that same
            // check number is unique
            if (id == null)
            {
                return NotFound();
            }
            var field = await _fieldRepository.GetById(id);

            if (field == null)
            {
                return NotFound();
            }

            var operators = ManagedYearPlan.Operators;

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

            var operatorSelectList = ManagedYearPlan.Operators.Select(x => new SelectListItem()
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
            if (ManagedYearPlan == null)
            {
                return ActionIfNotChoosedManagedYearPlan();
            }
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
            TempData["message"] = "Zmiany zapisano pomyślnie!";
            return View(model);
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            if (ManagedYearPlan == null)
            {
                return ActionIfNotChoosedManagedYearPlan();
            }
            var field = await _fieldRepository.GetById(id);
            await _fieldRepository.Delete(field);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddParcel(FieldViewModel model)
        {
            if (ManagedYearPlan == null)
            {
                return ActionIfNotChoosedManagedYearPlan();
            }
            var operatorSelectList = ManagedYearPlan.Operators.Select(x => new SelectListItem()
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
        public IActionResult RemoveParcel(FieldViewModel model, int index)
        {
            if (ManagedYearPlan == null)
            {
                return ActionIfNotChoosedManagedYearPlan();
            }

            ModelState.Clear();
            model.Parcels.RemoveAt(index);
            var operatorSelectList = ManagedYearPlan.Operators.Select(x => new SelectListItem()
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
            if (ManagedYearPlan == null)
            {
                return ActionIfNotChoosedManagedYearPlan();
            }
            // position = 1 - change fieldId number to up
            // position = 2 - change fieldId number to down
            var mainField = await _fieldRepository.GetById(fieldId);
            var fieldToChangeNumberWith = position == 1 ? await _fieldRepository.GetPrevious(fieldId) : await _fieldRepository.GetNext(fieldId);
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

        private async Task<int> GetNumberForNewField(YearPlan yearPlan)
        {
            var yearPlanFields = await _fieldRepository.GetByYearPlan(yearPlan);
            if (yearPlanFields.Count() > 0)
            {
                return yearPlanFields.Max(x => x.Number) + 1;
            }
            return 1;
        }

        // Validation
        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> UniqueFieldNumber(int Number, Guid Id)
        {
            if (ManagedYearPlan == null)
            {
                return ActionIfNotChoosedManagedYearPlan();
            }

            // Get list of user's yearplans
            var fields = await _fieldRepository.GetByYearPlan(ManagedYearPlan);
            var list = fields.ToList();

            foreach (var item in list)
            {
                if (item.Number == Number && item.Id != Id)
                {
                    return Json($"Posiadasz już pole z tym numerem");
                }
            }
            return Json(true);
        }

    }
}
