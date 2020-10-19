using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgroSup.WebApp.ViewModels.AdminPanel.Fertilizers
{
    public class FertilizerViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Nazwa jest wymagana")]
        [DisplayName("Nazwa")]
        public string Name { get; set; }
        public int N { get; set; }
        public int P { get; set; }
        public int K { get; set; }
        public int Ca { get; set; }
        public int Mg { get; set; }
        public int S { get; set; }
        public int Na { get; set; }
        public bool Delete { get; set; }
    }
}
