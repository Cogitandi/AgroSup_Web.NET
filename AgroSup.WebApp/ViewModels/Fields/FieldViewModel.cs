using AgroSup.Core.Domain;
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
        public int Number { get; set; }
        [Required(ErrorMessage = "Musisz wypełnić te pole")]
        [DisplayName("Nazwa")]
        public string Name { get; set; }
        [DisplayName("Powierzchnia [ha]")]
        public float Area { get; set; }

        public List<ParcelViewModel> Parcels { get; set; } = new List<ParcelViewModel>();
    }
}
