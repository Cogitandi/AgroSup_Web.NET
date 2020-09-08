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
    }
}
