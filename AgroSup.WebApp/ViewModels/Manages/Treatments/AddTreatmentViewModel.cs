using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgroSup.WebApp.ViewModels.Manages.Treatments
{
    public class AddTreatmentViewModel
    {
        [DisplayName("Nazwa")]
        public string Name { get; set; }
        [DisplayName("Data")]
        public DateTime Date { get; set; }
        [DisplayName("Pole")]
        public Guid FieldId { get; set; }
        [DisplayName("Uwagi")]
        public string Notes { get; set; }
        [DisplayName("Dawka na ha")]
        public int DosePerHa { get; set; }
        [DisplayName("Nawóz")]
        public Guid FertilizerId { get; set; }
        [DisplayName("Opryski")]
        public string SprayingAgents { get; set; }
    }
}
