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
        [RegularExpression(@"^[a-zA-ZąćęłńóśżźĄĆĘŁŃÓŚŻŹ]{2,20}$",ErrorMessage ="Niepoprawne imię")]
        public string FirstName { get; set; }
        [RegularExpression(@"^[a-zA-ZąćęłńóśżźĄĆĘŁŃÓŚŻŹ]{2,40}$", ErrorMessage = "Niepoprawne nazwisko")]
        [DisplayName("Nazwisko")]
        public string LastName { get; set; }
        [RegularExpression(@"^[0-9]{11}$", ErrorMessage = "Niepoprawny numer")]
        [DisplayName("Numer ARIMR")]
        public string ArimrNumber { get; set; }
    }
}
