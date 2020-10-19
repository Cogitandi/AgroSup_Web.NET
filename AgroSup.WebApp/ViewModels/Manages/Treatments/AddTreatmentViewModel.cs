using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AgroSup.WebApp.ViewModels.Manages.Treatments
{
    public class AddTreatmentViewModel : IValidatableObject
    {
        [DisplayName("Nazwa")]
        public string Name { get; set; }
        [DisplayName("Data")]
        public DateTime Date { get; set; }
        [DisplayName("Pole")]
        public Guid FieldId { get; set; }
        [DisplayName("Notatki")]
        public string Notes { get; set; }
        [DisplayName("Dawka na ha")]
        public int DosePerHa { get; set; }
        [DisplayName("Nawóz")]
        public Guid FertilizerId { get; set; }
        [DisplayName("Opryski")]
        public string SprayingAgents { get; set; }
        [DisplayName("Przyczyna użycia")]
        public string ReasonForUse { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Name.Equals("none"))
            {
                yield return new ValidationResult(
                    $"Musisz wybrać zabieg", new string[] { nameof(Name) });
            }

            if (!Name.Equals("none") && FieldId == Guid.Empty)
            {
                yield return new ValidationResult(
                    $"Musisz wybrać pole", new string[] { nameof(FieldId) });
            }
            if (Name.Equals(TreatmentViewModel.NameFertilizer) && FertilizerId == Guid.Empty)
            {
                yield return new ValidationResult(
                    $"Musisz wybrać nawóz", new string[] { nameof(FertilizerId) });
            }
        }

    }
}
