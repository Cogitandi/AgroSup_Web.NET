using AgroSup.Core.Domain;
using AgroSup.Core.Repositories;
using AgroSup.WebApp.ViewModels.Manages.Treatments;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgroSup.WebApp.Controllers
{
    public class TreatmentsController : BaseController
    {
        private readonly IFieldRepository _fieldRepository;
        private readonly IFertilizerRepository _fertilizerRepository;
        private readonly ITreatmentKindRepository _treatmentKindRepository;
        private readonly ITreatmentRepository _treatmentRepository;

        public TreatmentsController(
            UserManager<User> userManager,
            IUserRepository userRepository,
            IFieldRepository fieldRepository,
            IFertilizerRepository fertilizerRepository,
            ITreatmentKindRepository treatmentKindRepository,
            ITreatmentRepository treatmentRepository
            ) : base(userManager, userRepository)
        {
            _fertilizerRepository = fertilizerRepository;
            _fieldRepository = fieldRepository;
            _treatmentKindRepository = treatmentKindRepository;
            _treatmentRepository = treatmentRepository;

        }
        public async Task<IActionResult> Index()
        {
            if (ManagedYearPlan == null)
            {
                return ActionIfNotChoosedManagedYearPlan();
            }
            var treatments = await _treatmentRepository.GetAllByYearPlan(ManagedYearPlan);
            var model = treatments.Select(x => new TreatmentViewModel
            {
                Id = x.Id,
                Name = x.TreatmentKind.Name,
                Date = x.Date,
                Notes = x.Notes,
                DosePerHa = x.DosePerHa.ToString(),
                SprayingAgents = x.Composition,
                ReasonForUse = x.ReasonForUse,
                FieldName = x.Field.Name,
                FertilizerName = x.Fertilizer?.Name,
            });
            return View(model.OrderByDescending(x => x.Date));

        }
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
        public async Task<IActionResult> Create(IList<AddTreatmentViewModel> model)
        {
            if (ManagedYearPlan == null)
            {
                return ActionIfNotChoosedManagedYearPlan();
            }
            if (!ModelState.IsValid)
            {
                var kinds = await _treatmentKindRepository.GetAll();
                var fields = await _fieldRepository.GetByYearPlan(ManagedYearPlan);
                var fertilizers = await _fertilizerRepository.GetAll();
                var KindList = new SelectList(kinds, "Id", "Name");
                var FieldList = new SelectList(fields, "Id", "Name");
                var FertilizerList = new SelectList(fertilizers, "Id", "Name");

                ViewBag.Kinds = KindList;
                ViewBag.Fields = FieldList;
                ViewBag.Fertilizers = FertilizerList;
                return View(model);
            }

            var treatments = model.Select((x) => new Treatment
            {
                Date = x.Date,
                Notes = x.Notes,
                DosePerHa = x.DosePerHa,
                Composition = x.SprayingAgents,
                ReasonForUse = x.ReasonForUse,
                Field = _fieldRepository.GetById(x.FieldId).Result,
                Fertilizer = _fertilizerRepository.GetById(x.FertilizerId).Result,
                TreatmentKind = _treatmentKindRepository.GetById(x.TreatmentKindId).Result
            });
            await _treatmentRepository.AddRange(treatments);
            TempData["message"] = "Pomyślnie dodano nowe zabiegi";
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTreatment(IList<AddTreatmentViewModel> model)
        {
            if (ManagedYearPlan == null)
            {
                return ActionIfNotChoosedManagedYearPlan();
            }

            var kinds = await _treatmentKindRepository.GetAll();
            var fields = await _fieldRepository.GetByYearPlan(ManagedYearPlan);
            var fertilizers = await _fertilizerRepository.GetAll();
            var KindList = new SelectList(kinds, "Id", "Name");
            var FieldList = new SelectList(fields, "Id", "Name");
            var FertilizerList = new SelectList(fertilizers, "Id", "Name");

            ViewBag.Kinds = KindList;
            ViewBag.Fields = FieldList;
            ViewBag.Fertilizers = FertilizerList;

            model.Add(new AddTreatmentViewModel());

            return PartialView("Treatments", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveTreatment(IList<AddTreatmentViewModel> model, int __index)
        {
            if (ManagedYearPlan == null)
            {
                return ActionIfNotChoosedManagedYearPlan();
            }
            ModelState.Clear();
            model.RemoveAt(__index);

            var kinds = await _treatmentKindRepository.GetAll();
            var fields = await _fieldRepository.GetByYearPlan(ManagedYearPlan);
            var fertilizers = await _fertilizerRepository.GetAll();
            var KindList = new SelectList(kinds, "Id", "Name");
            var FieldList = new SelectList(fields, "Id", "Name");
            var FertilizerList = new SelectList(fertilizers, "Id", "Name");

            ViewBag.Kinds = KindList;
            ViewBag.Fields = FieldList;
            ViewBag.Fertilizers = FertilizerList;

            return PartialView("Treatments", model);
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            if (ManagedYearPlan == null)
            {
                return ActionIfNotChoosedManagedYearPlan();
            }

            var @operator = await _treatmentRepository.GetById(id);
            await _treatmentRepository.Delete(@operator);
            return RedirectToAction("Index");
        }
    }
}
