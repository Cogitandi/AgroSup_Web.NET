using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgroSup.WebApp.ViewModels.AdminPanel.Summary
{
    public class SummaryViewModel
    {
        public int TotalUsers { get; set; }
        public double AverageFieldArea { get; set; } //ar
        public int InActiveAccounts { get; set; }
        public double AverageTreatmentCountPerField { get; set; }
        public double AverageYearPlansCountPerUser { get; set; }
    }
}
