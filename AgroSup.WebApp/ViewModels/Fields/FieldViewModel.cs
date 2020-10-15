using AgroSup.Core.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgroSup.WebApp.ViewModels.Fields
{
    public class FieldViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage ="Musisz wypełnić te pole")]
        [DisplayName("Numer")]
        [Remote(action: "UniqueFieldNumber", controller: "Fields")]
        public int Number { get; set; }
        [RegularExpression(@"^([a-żA-Ż0-9_\-()\s]+)$", ErrorMessage = "Niepoprawna nazwa")]
        [Required(ErrorMessage = "Musisz wypełnić te pole")]
        [DisplayName("Nazwa")]
        public string Name { get; set; }
        [DisplayName("Powierzchnia [ha]")]
        public float Area { get; set; }

        public IList<ParcelViewModel> Parcels { get; set; } = new List<ParcelViewModel>();

        public void SetParcels(IEnumerable<ParcelViewModel> domains)
        {
            Parcels = domains.ToList();
        }
    }
}
