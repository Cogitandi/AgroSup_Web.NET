using AgroSup.Core.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgroSup.WebApp.ViewModels.YearPlans
{
    public class YearPlanViewModel
    {
        public Guid Id { get; set; }
        [Required]
        [DisplayName("Rok rozpoczęcia")]
        public int StartYear { get; set; }
        [DisplayName("Rok zakończenia")]
        public int EndYear { get; set; }
        public List<SelectListItem> UserYearPlans { get; set; }

        public void AddYearPlansToSelect(IEnumerable<YearPlan> yearplans)
        {
            UserYearPlans = yearplans.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.GetYearPlanName
            }).ToList();
        }


    }
}
