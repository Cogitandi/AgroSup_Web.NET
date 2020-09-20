using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgroSup.WebApp.ViewModels.Manages
{
    public class ParcelSummaryViewModel
    {
        public IEnumerable<SelectListItem> OperatorSelectList { get; set; }
        public IEnumerable<SelectListItem> PlantSelectList { get; set; }
        public IEnumerable<ParcelSummaryParcel> Parcels { get; set; }
    }
    public class ParcelSummaryParcel
    {
        public string FieldName { get; set; }
        public string Number { get; set; }
        public float CultivatedArea { get; set; }
        public string OperatorName { get; set; }
        public string PlantName { get; set; }
        public string FuelApplication { get; set; }
    }
}
