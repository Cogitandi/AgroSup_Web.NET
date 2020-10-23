using System;
using System.Collections.Generic;
using System.Text;

namespace AgroSup.Core.Domain
{
    public class Treatment
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }

        // Fertilization,Seed
        public int DosePerHa { get; set; }

        //Spraying
        public string Composition { get; set; }
        public string ReasonForUse { get; set; }

        public Field Field { get; set; }
        public Fertilizer Fertilizer { get; set; }
        public TreatmentKind TreatmentKind { get; set; }
    }
}
