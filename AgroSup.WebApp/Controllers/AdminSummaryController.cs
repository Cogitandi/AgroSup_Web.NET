using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgroSup.Core.Domain;
using AgroSup.Core.Repositories;
using AgroSup.WebApp.ViewModels.AdminPanel.Summary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgroSup.WebApp.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminSummaryController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IYearPlanRepository _yearPlanRepository;
        private readonly ITreatmentRepository _treatmentRepository;

        public AdminSummaryController(
            IUserRepository userRepository,
            IYearPlanRepository yearPlanRepository,
            ITreatmentRepository treatmentRepository
            )
        {
            _userRepository = userRepository;
            _yearPlanRepository = yearPlanRepository;
            _treatmentRepository = treatmentRepository;
        }

        public async Task<IActionResult> Index()
        {
            var Users = await _userRepository.GetAll();
            var YearPlans = await _yearPlanRepository.GetAll();
            var Treatments = await _treatmentRepository.GetAll();

            var FieldsCount = YearPlan.GetTotalFieldsCount(YearPlans);
            var FieldsArea = YearPlan.GetTotalFieldsArea(YearPlans);
            var YearPlansCount = YearPlans.Count();
            var InActiveUsersCount = AgroSup.Core.Domain.User.GetInActiveUsers(Users).Count();
            var TreatmentsCount = Treatments.Count();

            var Model = new SummaryViewModel
            {
                TotalUsers = Users.Count(),
                AverageFieldArea =  Math.Round((double)FieldsArea / (YearPlansCount * 100), 2),
                AverageTreatmentCountPerField =  Math.Round((double)TreatmentsCount / FieldsCount, 2),
                AverageYearPlansCountPerUser = Math.Round((double)YearPlans.Count()/ Users.Count(),2),
                InActiveAccounts = InActiveUsersCount,
            };
            return View(Model);
        }
    }
}
