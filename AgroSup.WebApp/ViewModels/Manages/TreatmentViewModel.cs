using System;
using System.ComponentModel;

namespace AgroSup.WebApp.ViewModels.Manages
{
    public class TreatmentViewModel
    {
        public Guid Id { get; set; }
        [DisplayName("Nazwa")]
        public string Name { get; set; }
        [DisplayName("Początek")]
        public DateTime StartDate { get; set; }
        [DisplayName("Koniec")]
        public DateTime EndDate { get; set; }
        [DisplayName("Pole")]
        public string FieldName { get; set; }
        [DisplayName("Nawóz")]
        public string FertilizerName { get; set; }
        [DisplayName("Dawka na ha")]
        public int DosePerHa { get; set; }
        [DisplayName("Przyczyna użycia")]
        public string ReasonForUse { get; set; }
        [DisplayName("Uwagi")]
        public string Notes { get; set; }
    }
    public class Treatment
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string FieldName { get; set; }
        public string FertilizerName { get; set; }
        public int DosePerHa { get; set; }
        public string ReasonForUse { get; set; }
        public string Notes { get; set; }
    }
}
