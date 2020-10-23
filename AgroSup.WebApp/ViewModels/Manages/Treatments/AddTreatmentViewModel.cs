using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AgroSup.WebApp.ViewModels.Manages.Treatments
{
    public class AddTreatmentViewModel : IValidatableObject
    {
        public static Guid FertilizingTreatmentID = Guid.Parse("407d4311-9b2f-4ee1-a532-1a1317e574f0");
        public static Guid SeedingTreatmentID = Guid.Parse("a56940ba-6de0-4803-8c90-09d17e143ac2");
        public static Guid SprayingTreatmentID = Guid.Parse("98595141-8270-413a-b175-d02a35eb48bd");

        [DisplayName("Nazwa")]
        public Guid TreatmentKindId { get; set; }
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
            if (TreatmentKindId == Guid.Empty)
            {
                yield return new ValidationResult(
                    $"Musisz wybrać zabieg", new string[] { nameof(TreatmentKindId) });
            }

            if (!(TreatmentKindId == Guid.Empty) && FieldId == Guid.Empty)
            {
                yield return new ValidationResult(
                    $"Musisz wybrać pole", new string[] { nameof(FieldId) });
            }
            if (TreatmentKindId==FertilizingTreatmentID && FertilizerId == Guid.Empty)
            {
                yield return new ValidationResult(
                    $"Musisz wybrać nawóz", new string[] { nameof(FertilizerId) });
            }
        }

    }
}
