using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace AgroSup.WebApp.ViewModels.Operators
{
    public class OperatorViewModel
    {
        public Guid Id { get; set; }
        [DisplayName("Imię")]
        public string FirstName { get; set; }
        [DisplayName("Nazwisko")]
        public string LastName { get; set; }
        [DisplayName("Numer ARIMR")]
        public string ArimrNumber { get; set; }
    }
}
