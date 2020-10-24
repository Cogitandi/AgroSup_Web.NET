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
        [Required(ErrorMessage = "To pole jest wymagane")]
        public int N { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane")]
        public int P { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane")]
        public int K { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane")]
        public int Ca { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane")]
        public int Mg { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane")]
        public int S { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane")]
        public int Na { get; set; }
        public bool Delete { get; set; }
    }
}
