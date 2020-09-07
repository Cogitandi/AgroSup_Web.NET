using System;
using System.Collections.Generic;
using System.Text;

namespace AgroSup.Core.Domain
{
    class Field
    {

        private IList<Parcel> _parcels = new List<Parcel>();

        public Guid Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public string PlantVariety { get; set; }
        public Plant Plant { get; set; }
        public YearPlan YearPlan { get; set; }

        public IEnumerable<Parcel> Parcels => _parcels;
    }
}
