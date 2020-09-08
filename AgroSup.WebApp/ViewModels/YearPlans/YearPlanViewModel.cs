using AgroSup.Core.Domain;
using Microsoft.AspNetCore.Mvc;
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
        [Required(ErrorMessage ="To pole jest wymagane")]
        [DisplayName("Rok rozpoczęcia")]
        [Remote(action: "UniqueYearPlan", controller: "YearPlans")]
        [Range(2000, 2100, ErrorMessage = "Niepoprawny rok")]
        public int StartYear { get; set; }
        [DisplayName("Rok zakończenia")]
        public int EndYear { get; set; }
    }
}
