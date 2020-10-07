using AgroSup.Core.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        [RegularExpression(@"^[0-9|/]+$", ErrorMessage = "Niepoprawny numer")]
        [DisplayName("Numer")]
        public string Number { get; set; }
        [Required(ErrorMessage = "Musisz wypełnić te pole")]
        [DisplayName("Powierzchnia [ar]")]
        [Range(0,10000,ErrorMessage ="Powierzchnia nie może być ujemna")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Niepoprawna powierzchnia")]
        public int CultivatedArea { get; set; }
        [DisplayName("Paliwo")]
        public bool FuelApplication { get; set; }
        [DisplayName("Dopłaty")]
        public Guid OperatorId { get; set; }
    }
}
