using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AgroSup.Core.Domain
{
    public class Operator
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ArimrNumber { get; set; }
        public YearPlan YearPlan { get; set; }
        public IList<Parcel> Parcels { get; set; } = new List<Parcel>();

        public string GetName
        {
            get
                {
                return FirstName + " " + LastName;
            }
        }
        public float GetTotalArea()
        {
            float totalArea = 0;
            foreach(var item in Parcels)
            {
                totalArea += item.CultivatedArea;
            }
            return totalArea/100;
        }
        public float GetFuelArea()
        {
            float fuelArea = 0;
            foreach (var item in Parcels)
            {
                fuelArea += item.FuelApplication ? item.CultivatedArea : 0;
            }
            return fuelArea/100;
        }
        public float GetNotStabilishedArea()
        {
            float area = 0;
            foreach (var item in Parcels)
            {
                area += item.Field.Plant==null ? item.CultivatedArea : 0;
            }
            return area/100;
        }
        public IEnumerable<Plant> GetPlantsList()
        {
            IList<Plant> plants = new List<Plant>();

            foreach (var item in Parcels)
            {
                var plant = item.Field.Plant;
                if(plant !=null && !plants.Contains(plant))
                {
                    plants.Add(plant);
                }
            }
            return plants;
        }
        public float GetAreaByPlant(Plant plant)
        {
            float area = 0;
            foreach (var item in Parcels)
            {
                var fieldPlant = item.Field.Plant;
                area += (fieldPlant != null && fieldPlant==plant) ? item.CultivatedArea : 0;
            }
            return area/100;
        }
        public float GetEfaPercent()
        {
            float efaArea = 0;
            foreach (var item in Parcels)
            {
                if (item.Field.Plant == null)
                    continue;
                if (item.Field.Plant.EfaNitrogenRate == 0)
                    continue;
                efaArea += item.CultivatedArea * item.Field.Plant.EfaNitrogenRate;
            }
            float totalArea = GetTotalArea()*100;
            float percent = totalArea / 100 * efaArea;
            return percent;
        }
    }
}
