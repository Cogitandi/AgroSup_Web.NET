using System;
using System.Collections.Generic;
using System.Text;

namespace AgroSup.Core.Domain
{
    class Parcel
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public int CultivatedArea { get; set; }
        public bool FuelApplication { get; set; }
        public Operator Operator { get; set; }
    }
}
