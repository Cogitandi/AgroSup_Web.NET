using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgroSup.Core.Domain
{
    public class Field
    {
        private IList<Parcel> _parcels = new List<Parcel>();

        public Guid Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public string PlantVariety { get; set; }
        public Plant Plant { get; set; }
        public YearPlan YearPlan { get; set; }

        public List<Parcel> Parcels { get; set; } = new List<Parcel>();

        public float GetArea()
        {
            float area = 0;
            foreach(var item in Parcels)
            {
                area += item.CultivatedArea;
            }
            return area/100;
        }
        public void SetParcels(IEnumerable<Parcel> parcels)
        {
            Parcels = parcels.ToList();
        }
    }
}
