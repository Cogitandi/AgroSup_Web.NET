using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgroSup.WebApp.ViewModels.Plants
{
    public class PlantViewModel
    {
        public IList<string> SelectedPlants { get; set; } = new List<string>();
        public IList<SelectListItem> AvailablePlants { get; set; } = new List<SelectListItem>();
    }
}
