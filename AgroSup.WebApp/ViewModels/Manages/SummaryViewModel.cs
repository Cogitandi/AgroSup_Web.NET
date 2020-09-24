using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgroSup.WebApp.ViewModels.Manages
{
    public class SummaryViewModel
    {
        public IEnumerable<SummaryDisplayGroup> DisplayGroups { get; set; }

    }
    public class SummaryDisplayGroup
    {
        public bool ShowEfa { get; set; }
        public string Name { get; set; }
        public IEnumerable<SummaryPlant> Plants { get; set; }
        public float EfaPercent { get; set; }
        public float TotalArea { get; set; }
        public float FuelArea { get; set; }
    }
    public class SummaryPlant
    {
        public string Name { get; set; }
        public float Area { get; set; }
        public int AreaPercent { get; set; }
    }
}
