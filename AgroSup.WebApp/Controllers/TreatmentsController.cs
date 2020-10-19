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
        private readonly ITreatmentRepository<SeedingTreatment> _seedingRepository;
        private readonly ITreatmentRepository<SprayingTreatment> _sprayingRepository;
        private readonly ITreatmentRepository<FertilizationTreatment> _fertilizationRepository;

        public TreatmentsController(
            UserManager<User> userManager,
            IUserRepository userRepository,
            IFieldRepository fieldRepository,
            IFertilizerRepository fertilizerRepository,
            ITreatmentRepository<SeedingTreatment> seedingRepository,
            ITreatmentRepository<SprayingTreatment> sprayingRepository,
            ITreatmentRepository<FertilizationTreatment> fertilizationRepository
            ) : base(userManager, userRepository)
        {
            _fertilizerRepository = fertilizerRepository;
            _fieldRepository = fieldRepository;
            _seedingRepository = seedingRepository;
            _sprayingRepository = sprayingRepository;
            _fertilizationRepository = fertilizationRepository;

        }
        public async Task<IActionResult> Index()
        {
            if (ManagedYearPlan == null)
            {
                return ActionIfNotChoosedManagedYearPlan();
            }

            var fertilizationTreatments = await _fertilizationRepository.GetAllByYearPlan(ManagedYearPlan);
            var seedingTreatments = await _seedingRepository.GetAllByYearPlan(ManagedYearPlan);
            var sprayingTreatments = await _sprayingRepository.GetAllByYearPlan(ManagedYearPlan);

            var FertilizationTreatmentsToModel = fertilizationTreatments.Select(x => new TreatmentViewModel
            {
                Id = x.Id,
                Name = "Nawóz",
                Date = x.Date,
                FieldName = x.Field.Name,
                Notes = x.Notes,
                DosePerHa = x.DosePerHa.ToString(),
                FertilizerName = x.Fertilizer.Name,
                SprayingAgents = "-",
                ReasonForUse = "-",
            });
            var SeedingTreatmentsToModel = seedingTreatments.Select(x => new TreatmentViewModel
            {
                Id = x.Id,
                Name = "Siew",
                Date = x.Date,
                FieldName = x.Field.Name,
                Notes = x.Notes,
                DosePerHa = x.DosePerHa.ToString(),
                FertilizerName ="-",
                SprayingAgents = "-",
                ReasonForUse = "-",
            });
            var SprayingTreatmentsToModel = sprayingTreatments.Select(x => new TreatmentViewModel
            {
                Id = x.Id,
                Name = "Oprysk",
                Date = x.Date,
                FieldName = x.Field.Name,
                Notes = x.Notes,
                DosePerHa = "-",
                FertilizerName = "-",
                SprayingAgents = x.Composition,
                ReasonForUse = x.ReasonForUse,
            });
            var model = new List<TreatmentViewModel>();
            model.AddRange(FertilizationTreatmentsToModel);
            model.AddRange(SeedingTreatmentsToModel);
            model.AddRange(SprayingTreatmentsToModel);
            model = model.OrderByDescending(x => x.Date).ToList();
            return View(model);
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
                var KindList = new List<SelectListItem>
            {
                new SelectListItem{Text="Nawóz",Value=TreatmentViewModel.NameFertilizer},
                new SelectListItem{Text="Oprysk",Value=TreatmentViewModel.NameSpraying},
                new SelectListItem{Text="Siew",Value=TreatmentViewModel.NameSeeding },
            };
                var fields = await _fieldRepository.GetByYearPlan(ManagedYearPlan);
                var FieldList = new SelectList(fields, "Id", "Name");
                var fertilizers = await _fertilizerRepository.GetAll();
                var FertilizerList = new SelectList(fertilizers, "Id", "Name");
                ViewBag.Kinds = KindList;
                ViewBag.Fields = FieldList;
                ViewBag.Fertilizers = FertilizerList;
                return View(model);
            }
            foreach (var item in model)
            {
                switch (item.Name)
                {
                    case "none":
                        break;
                    case TreatmentViewModel.NameFertilizer:
                        var treatment = new FertilizationTreatment
                        {
                            Date = item.Date,
                            Field = await _fieldRepository.GetById(item.FieldId),
                            Notes = item.Notes,
                            DosePerHa = item.DosePerHa,
                            Fertilizer = await _fertilizerRepository.GetById(item.FertilizerId),
                            
                        };
                        await _fertilizationRepository.Add(treatment);
                        break;
                    case TreatmentViewModel.NameSeeding:
                        {
                            var treatment3 = new SeedingTreatment
                            {
                                Date = item.Date,
                                Field = await _fieldRepository.GetById(item.FieldId),
                                Notes = item.Notes,
                                DosePerHa = item.DosePerHa,
                            };
                            await _seedingRepository.Add(treatment3);
                        }
                        break;
                    case TreatmentViewModel.NameSpraying:
                        var treatment2 = new SprayingTreatment
                        {
                            Date = item.Date,
                            Field = await _fieldRepository.GetById(item.FieldId),
                            Notes = item.Notes,
                            Composition = item.SprayingAgents,
                            ReasonForUse = item.ReasonForUse,
                        };
                        await _sprayingRepository.Add(treatment2);
                        break;
                }
            }
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

            var KindList = new List<SelectListItem>
            {
                new SelectListItem{Text="Nawóz",Value=TreatmentViewModel.NameFertilizer},
                new SelectListItem{Text="Oprysk",Value=TreatmentViewModel.NameSpraying},
                new SelectListItem{Text="Siew",Value=TreatmentViewModel.NameSeeding },
            };

            var fields = await _fieldRepository.GetByYearPlan(ManagedYearPlan);
            var FieldList = new SelectList(fields, "Id", "Name");
            var fertilizers = await _fertilizerRepository.GetAll();
            var FertilizerList = new SelectList(fertilizers, "Id", "Name");
            ViewBag.Kinds = KindList;
            ViewBag.Fields = FieldList;
            ViewBag.Fertilizers = FertilizerList;

            model.Add(new AddTreatmentViewModel());

            return PartialView("Treatments", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveTreatment(IList<AddTreatmentViewModel> model, int index)
        {
            if (ManagedYearPlan == null)
            {
                return ActionIfNotChoosedManagedYearPlan();
            }
            ModelState.Clear();
            model.RemoveAt(index);

            var KindList = new List<SelectListItem>
            {
                new SelectListItem{Text="Nawóz",Value=TreatmentViewModel.NameFertilizer},
                new SelectListItem{Text="Oprysk",Value=TreatmentViewModel.NameSpraying},
                new SelectListItem{Text="Siew",Value=TreatmentViewModel.NameSeeding },
            };

            var fields = await _fieldRepository.GetByYearPlan(ManagedYearPlan);
            var FieldList = new SelectList(fields, "Id", "Name");
            var fertilizers = await _fertilizerRepository.GetAll();
            var FertilizerList = new SelectList(fertilizers, "Id", "Name");
            ViewBag.Kinds = KindList;
            ViewBag.Fields = FieldList;
            ViewBag.Fertilizers = FertilizerList;

            return PartialView("Treatments1", model);
        }
    }
}
