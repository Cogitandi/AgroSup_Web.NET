using System;
using System.Collections.Generic;
using System.Text;

namespace AgroSup.Core.Domain
{
    public class SprayingTreatment : Treatment
    {
        public string Composition { get; set; }
        public string ReasonForUse { get; set; }
        public string Comments { get; set; }
    }
}
