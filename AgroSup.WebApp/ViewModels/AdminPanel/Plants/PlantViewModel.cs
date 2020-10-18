using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AgroSup.WebApp.ViewModels.AdminPanel.Plants
{
    public class PlantViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Nazwa jest wymagana")]
        [DisplayName("Nazwa")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Musisz podać współczynnik")]
        [Range(0,2,ErrorMessage ="Współczynnik musi być z przedziału [0,2]")]
        [DisplayName("Współczynnik EFA")]
        public int EfaNitrogenRate { get; set; }
        public bool Delete { get; set; }
    }
}
