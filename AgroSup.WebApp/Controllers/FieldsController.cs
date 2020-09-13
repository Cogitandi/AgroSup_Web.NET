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

        public FieldsController(
            UserManager<User> userManager,
            IUserRepository userRepository,
            IFieldRepository fieldRepository
            )
        {
            _userManager = userManager;
            _userRepository = userRepository;
            _fieldRepository = fieldRepository;
        }

        public async Task<IActionResult> Index()
        {
            var managedYearPlan = await getManagedYearPlan();

            if (managedYearPlan == null)
            {
                return RedirectToAction("Index", "YearPlan");
            }

            var fields = await _fieldRepository.GetByYearPlan(managedYearPlan);

            var model = fields.Select(x => new FieldViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Number = x.Number,
                Area = x.GetArea()
            });
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var managedYearPlan = await getManagedYearPlan();
            if (managedYearPlan == null)
            {
                return RedirectToAction("Index", "YearPlan");
            }
            var operators = managedYearPlan.Operators.Select(x => new SelectListItem()
            {
                Text = x.GetName,
                Value = x.Id.ToString(),
            });

            var model = new FieldViewModel()
            {
                Number = await GetNumberForNewField(managedYearPlan),
            };
            ViewBag.Operators = new SelectList(operators, "Id", "GetName");
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
                return RedirectToAction("Index", "YearPlan");
            }


            Field field = new Field()
            {
                Name = model.Name,
                Number = model.Number,
                YearPlan = managedYearPlan,
            };
            field.SetParcels(getParcelsFromModel(model));
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

            var model = new FieldViewModel()
            {
                Id = field.Id,
                Name = field.Name,
                Number = field.Number,
            };
            model.SetParcels(getParcelsModelFromDomain(field));

            var managedYearPlan = await getManagedYearPlan();
            if (managedYearPlan == null)
            {
                return RedirectToAction("Index", "YearPlan");
            }

            var operators = managedYearPlan.Operators;
            ViewBag.Operators = new SelectList(operators, "Id", "GetName");

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
            field.SetParcels(getParcelsFromModel(model));
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
                return RedirectToAction("Index", "YearPlan");
            }

            var operators = managedYearPlan.Operators;
            ViewBag.Operators = new SelectList(operators, "Id", "GetName");
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
                return RedirectToAction("Index", "YearPlan");
            }

            var operators = managedYearPlan.Operators;
            ViewBag.Operators = new SelectList(operators, "Id", "GetName");

            ModelState.Clear();
            model.Parcels.RemoveAt(index);
            return PartialView("Parcels", model);
        }
        [HttpPost]
        public async Task<IActionResult> ChangeNumber(Guid fieldId, int position)
        {
            // position = 1 - change fieldId number to up
            // position = 2 - change fieldId number to down

            if(position==1)
            {
                var mainField = await _fieldRepository.GetById(fieldId);
                var fieldWithHigherNumber = await _fieldRepository.GetPrevious(fieldId);
                await SwapFieldNumbers(mainField, fieldWithHigherNumber);
            } else
            {
                var mainField = await _fieldRepository.GetById(fieldId);
                var fieldWithLowerNumber = await _fieldRepository.GetNext(fieldId);
                await SwapFieldNumbers(mainField, fieldWithLowerNumber);
            }


            var field = await _fieldRepository.GetById(fieldId);
            //var neighbourField = position==1?await
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
                Operator = x.Operator,
            });
        }
        private IEnumerable<ParcelViewModel> getParcelsModelFromDomain(Field domain)
        {
            return domain.Parcels.Select(x => new ParcelViewModel()
            {
                Number = x.Number,
                CultivatedArea = x.CultivatedArea,
                FuelApplication = x.FuelApplication,
                Operator = x.Operator,
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

    }
}
