using System;
using System.Collections.Generic;
using System.Text;

namespace AgroSup.Core.Domain
{
    public class Operator
    {
        private IList<Parcel> _parcels = new List<Parcel>();

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ArimrNumber { get; set; }
        public YearPlan YearPlan { get; set; }
        public IEnumerable<Parcel> Parcels => _parcels;
    }
}
