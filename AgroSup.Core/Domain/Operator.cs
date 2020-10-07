using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace AgroSup.Core.Domain
{
    public class Operator
    {
        public Guid Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ArimrNumber { get; set; }
        [Required]
        public YearPlan YearPlan { get; set; }
        public IList<Parcel> Parcels { get; set; } = new List<Parcel>();

        public string GetName
        {
            get
                {
                return FirstName + " " + LastName;
            }
        }
        public Operator()
        {

        }
        public Operator(Operator @operator)
        {
            this.FirstName = @operator.FirstName;
            this.LastName = @operator.LastName;
            this.LastName = @operator.LastName;
            this.ArimrNumber = @operator.ArimrNumber;
        }
        public int GetTotalArea()
        {
            int totalArea = 0;
            foreach(var item in Parcels)
            {
                totalArea += item.CultivatedArea;
            }
            return totalArea;
        }
        public int GetFuelArea()
        {
            int fuelArea = 0;
            foreach (var item in Parcels)
            {
                fuelArea += item.FuelApplication ? item.CultivatedArea : 0;
            }
            return fuelArea;
        }
        public IEnumerable<string> GetPlantNameList()
        {
            IList<string> PlantNameList = new List<string>();

            foreach (var parcel in Parcels)
            {
                if(!PlantNameList.Contains(parcel.GetPlantName()))
                {
                    PlantNameList.Add(parcel.GetPlantName());
                }
            }
            return PlantNameList;
        }
        public int GetAreaByPlant(string plantName)
        {
            int area = 0;
            foreach (var parcel in Parcels)
            {
                if(parcel.GetPlantName().Equals(plantName))
                {
                    area += parcel.CultivatedArea;
                }
            }
            return area;
        }
        public float GetEfaPercent()
        {
            int totalArea = GetTotalArea();
            if (totalArea == 0)
            {
                return 0;
            }

            int efaArea = 0;
            foreach (var item in Parcels)
            {
                if (item.Field.Plant == null)
                    continue;
                if (item.Field.Plant.EfaNitrogenRate == 0)
                    continue;
                efaArea += item.CultivatedArea * item.Field.Plant.EfaNitrogenRate;
            }

            float percent = 100 * efaArea / totalArea;
            return percent;
        }
    }
}
