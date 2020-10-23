using System;
using System.ComponentModel;

namespace AgroSup.WebApp.ViewModels.Manages.Treatments
{
    public class TreatmentViewModel
    {
        public Guid Id { get; set; }
        [DisplayName("Nazwa")]
        public string Name { get; set; }
        [DisplayName("Data")]
        public DateTime Date { get; set; }
        [DisplayName("Pole")]
        public string FieldName { get; set; }
        [DisplayName("Dodatkowe")]
        public string Notes { get; set; }
        [DisplayName("Dawka na ha")]
        public string DosePerHa { get; set; }
        [DisplayName("Nawóz")]
        public string FertilizerName { get; set; }
        [DisplayName("Opryski")]
        public string SprayingAgents { get; set; }
        [DisplayName("Przyczyna użycia")]
        public string ReasonForUse { get; set; }
    }
}
