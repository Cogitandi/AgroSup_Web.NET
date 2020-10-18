using AgroSup.Core.Domain;
using AgroSup.Core.Repositories;
using AgroSup.WebApp.ViewModels.Manages.Treatments;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgroSup.WebApp.Controllers
{
    public class TreatmentsController : BaseController
    {
        private readonly IFieldRepository _fieldRepository;
        private readonly ITreatmentRepository<SeedingTreatment> _seedingRepository;
        private readonly ITreatmentRepository<SprayingTreatment> _sprayingRepository;
        private readonly ITreatmentRepository<FertilizationTreatment> _fertilizationRepository;

        public TreatmentsController(
            UserManager<User> userManager,
            IUserRepository userRepository,
            IFieldRepository fieldRepository,
            ITreatmentRepository<SeedingTreatment> seedingRepository,
            ITreatmentRepository<SprayingTreatment> sprayingRepository,
            ITreatmentRepository<FertilizationTreatment> fertilizationRepository
            ) : base(userManager, userRepository)
        {
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
                StartDate = x.Start,
                EndDate = x.End,
                FieldName = x.Field.Name,
                //FertilizerName = x.Fertilizer.Name,
                DosePerHa = x.DosePerHa,
                Name = "Nawóz",
                Notes = "-",
                ReasonForUse = "-"

            });
            var SeedingTreatmentsToModel = seedingTreatments.Select(x => new TreatmentViewModel
            {
                Id = x.Id,
                StartDate = x.Start,
                EndDate = x.End,
                FieldName = x.Field.Name,
                FertilizerName = "-",
                DosePerHa = x.DosePerHa,
                Name = "Siew",
                Notes = "-",
                ReasonForUse = "-"
            });
            var SprayingTreatmentsToModel = sprayingTreatments.Select(x => new TreatmentViewModel
            {
                Id = x.Id,
                StartDate = x.Start,
                EndDate = x.End,
                FieldName = x.Field.Name,
                FertilizerName = "-",
                Name = "Oprysk",
                Notes = "-",
                ReasonForUse = "-"
            });
            var model = new List<TreatmentViewModel>();
            model.AddRange(FertilizationTreatmentsToModel);
            model.AddRange(SeedingTreatmentsToModel);
            model.AddRange(SprayingTreatmentsToModel);
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
        public async Task<IActionResult> Create(IList<AddTreatmentViewModel> model)
        {
            if (ManagedYearPlan == null)
            {
                return ActionIfNotChoosedManagedYearPlan();
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
                            DosePerHa = item.DosePerHa,
                            Field = await _fieldRepository.GetById(item.FieldId),
                        };
                        await _fertilizationRepository.Add(treatment);
                        break;
                    case TreatmentViewModel.NameSpraying:
                        var treatment2 = new SprayingTreatment
                        {
                            Field = await _fieldRepository.GetById(item.FieldId),
                        };
                        await _sprayingRepository.Add(treatment2);
                        break;
                    case TreatmentViewModel.NameSeeding:
                        {
                            var treatment3 = new SeedingTreatment
                            {
                                DosePerHa = item.DosePerHa,
                                Field = await _fieldRepository.GetById(item.FieldId),
                            };
                            await _seedingRepository.Add(treatment3);
                        }
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

            var fields = await _fieldRepository.GetByYearPlan(ManagedYearPlan);
            var FieldList = new SelectList(fields, "Id", "Name");
            var KindList = new List<SelectListItem>
            {
                new SelectListItem{Text="Nawóz",Value=TreatmentViewModel.NameFertilizer},
                new SelectListItem{Text="Oprysk",Value=TreatmentViewModel.NameSpraying},
                new SelectListItem{Text="Siew",Value=TreatmentViewModel.NameSeeding },
            };

            ViewBag.Kinds = KindList;
            ViewBag.Fields = FieldList;

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

            var fields = await _fieldRepository.GetByYearPlan(ManagedYearPlan);
            var FieldList = new SelectList(fields, "Id", "Name");
            var KindList = new List<SelectListItem>
            {
                new SelectListItem{Text="Nawóz",Value="fertilizer"},
                new SelectListItem{Text="Oprysk",Value="spraying"},
                new SelectListItem{Text="Siew",Value="seeding"},
            };

            ViewBag.Kinds = KindList;
            ViewBag.Fields = FieldList;

            return PartialView("Treatments1", model);
        }
    }
}
