using System;
using System.Collections.Generic;
using System.Text;

namespace AgroSup.Core.Domain
{
    public class FertilizationTreatment : Treatment
    {
        public int DosePerHa { get; set; }
        public Fertilizer Fertilizer { get; set; }
    }
}
