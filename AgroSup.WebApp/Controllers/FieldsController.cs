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
        private readonly IFieldRepository _fieldRepository;
        private readonly IParcelRepository _parcelRepository;

        public FieldsController(
            UserManager<User> userManager,
            IFieldRepository fieldRepository,
            IParcelRepository parcelRepository
            )
        {
            _userManager = userManager;
            _fieldRepository = fieldRepository;
            _parcelRepository = parcelRepository;
        }

        public async Task<IActionResult> Index()
        {
            var loggedUser = await _userManager.GetUserAsync(User);
            var fields = await _fieldRepository.GetByUser(loggedUser);
            var model = fields.Select(x => new FieldViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Number = x.Number,
                Area = x.GetArea()
            }); ;
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FieldViewModel model)
        {
        
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var loggedUser = await _userManager.GetUserAsync(User);

            Field field = new Field()
            {
                Name = model.Name,
                Number = model.Number,

            };
            model.Parcels.ForEach(x =>
            {
                field.Parcels.Add(new Parcel()
                {
                    Number = x.Number,
                    CultivatedArea = x.CultivatedArea,
                    FuelApplication = x.FuelApplication
                });
            });
            await _fieldRepository.Create(field);
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
                Name = field.Name,
                Number = field.Number,
            };
            field.Parcels.ForEach(x =>
            {
                model.Parcels.Add(new ParcelViewModel()
                {
                    Number = x.Number,
                    CultivatedArea = x.CultivatedArea,
                    FuelApplication = x.FuelApplication
                });
            });


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

            model.Parcels.ForEach(x =>
            {
                field.Parcels.Add(new Parcel()
                {
                    Number = x.Number,
                    CultivatedArea = x.CultivatedArea
                });
            });
            await _fieldRepository.Update(field);
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var field = await _fieldRepository.GetById(id);
            await _fieldRepository.Remove(field);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddParcel(FieldViewModel model)
        {
            var operators = new List<Operator>()
            {
                new Operator(){FirstName="panwl",LastName="konik"},
                new Operator(){FirstName="drugi",LastName="jan"},
            };

            ViewBag.Operators = new SelectList(operators, "Id", "GetName");
            model.Parcels.Add(new ParcelViewModel());
            return PartialView("Parcels", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveParcel(FieldViewModel model, int index)
        {
            ModelState.Clear();
            model.Parcels.RemoveAt(index);
            return PartialView("Parcels", model);
        }

    }
}
