using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgroSup.WebApp.ViewModels.Operators
{
    public class OperatorViewModel
    {
        public Guid Id { get; set; }
        [DisplayName("Imię")]
        [Required(ErrorMessage ="To pole jest wymagane")]
        public string FirstName { get; set; }
        [DisplayName("Nazwisko")]
        public string LastName { get; set; }
        [DisplayName("Numer ARIMR")]
        public string ArimrNumber { get; set; }
    }
}
