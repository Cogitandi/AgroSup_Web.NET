using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgroSup.WebApp.ViewModels.AdminPanel
{
    public class PlantViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane")]
        [DisplayName("Nazwa")]
        public string Name { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane")]
        [DisplayName("Współczynnik EFA")]
        public int EfaNitrogenRate { get; set; }
        public bool Delete { get; set; }
    }
}
