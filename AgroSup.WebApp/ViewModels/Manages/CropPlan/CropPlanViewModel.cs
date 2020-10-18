using AgroSup.Core.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgroSup.WebApp.ViewModels.Manages.CropPlan
{
    public class CropPlanViewModel
    {
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public IEnumerable<FieldViewModel> Fields2{ get; set; }
        public IEnumerable<FieldViewModel> Fields1{ get; set; }
        public IList<FieldViewModel> Fields{ get; set; }
        public IEnumerable<SelectListItem> Plants { get; set; }
    }
    public class FieldViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float Area { get; set; }
        public string PlantName2 { get; set; }
        public string PlantName1 { get; set; }
        public string PlantVariety { get; set; }
        public Guid PlantId { get; set; }

    }
}
