using AgroSup.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgroSup.WebApp.ViewModels.Fields
{
    public class ParcelViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Musisz wypełnić te pole")]
        [DisplayName("Numer")]
        public string Number { get; set; }
        [Required(ErrorMessage = "Musisz wypełnić te pole")]
        [DisplayName("Powierzchnia")]
        public int CultivatedArea { get; set; }
        [DisplayName("Paliwo")]
        public bool FuelApplication { get; set; }
        [DisplayName("Dopłaty")]
        public Operator Operator { get; set; }
    }
}
