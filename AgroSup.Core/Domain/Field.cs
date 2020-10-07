using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace AgroSup.Core.Domain
{
    public class Field
    {
        public Guid Id { get; set; }
        [Required]
        public int Number { get; set; }
        [Required]
        public string Name { get; set; }
        public string PlantVariety { get; set; }
        public Plant Plant { get; set; }
        [Required]
        public YearPlan YearPlan { get; set; }

        public IEnumerable<Parcel> Parcels { get; set; } = new List<Parcel>();

        public Field()
        {
                
        }
        public Field(Field field)
        {
            this.Number = field.Number;
            this.Name = field.Name;
        }
        public int GetFieldArea()
        {
            int area = 0;
            foreach(var parcel in Parcels)
            {
                area += parcel.CultivatedArea;
            }
            return area;
        }
        // Total
        public static int GetTotalCultivatedArea(IEnumerable<Field> fields)
        {
            int area = 0;
            foreach (var field in fields)
            {
                area += field.GetFieldArea();
            }
            return area;
        }
        public static int GetTotalFuelArea(IEnumerable<Field> fields)
        {
            int area = 0;
            foreach (var field in fields)
            {
                foreach (var parcel in field.Parcels)
                {
                    area += parcel.FuelApplication ? parcel.CultivatedArea :0;
                }
            }
            return area;
        }
        public static int GetTotalNotEstabilishedArea(IEnumerable<Field> fields)
        {
            int area = 0;
            foreach (var field in fields)
            {
                foreach (var parcel in field.Parcels)
                {
                    area += parcel.Field.Plant == null ? parcel.CultivatedArea : 0;
                }
            }
            return area;
        }
        public static IEnumerable<string> GetTotalPlantNameList(IEnumerable<Field> fields)
        {
            IList<string> PlantNameList = new List<string>();
            foreach (var field in fields)
            {
                foreach (var parcel in field.Parcels)
                {
                    if (!PlantNameList.Contains(parcel.GetPlantName()))
                    {
                        PlantNameList.Add(parcel.GetPlantName());
                    }
                }
            }
            return PlantNameList;
        }
        public static int GetTotalCultivatedAreaByPlantName(IEnumerable<Field> fields, string plantName)
        {
            int area = 0;
            foreach (var field in fields)
            {
                foreach (var parcel in field.Parcels)
                {
                    if (parcel.GetPlantName().Equals(plantName))
                    {
                        area += parcel.CultivatedArea;
                    }
                }
            }
            return area;
        }
        // WithoutOperator
        public static int GetCultivatedAreaWithoutOperator(IEnumerable<Field> fields)
        {
            int area = 0;
            foreach(var field in fields)
            {
                foreach(var parcel in field.Parcels)
                {
                    area += parcel.Operator == null ? parcel.CultivatedArea : 0;
                }
            }
            return area;
        }
        public static int GetFuelAreaWithoutOperator(IEnumerable<Field> fields)
        {
            int area = 0;
            foreach (var field in fields)
            {
                foreach (var parcel in field.Parcels)
                {
                    area += (parcel.Operator == null && parcel.FuelApplication) ? parcel.CultivatedArea : 0;
                }
            }
            return area;
        }
        public static IEnumerable<string> GetPlantNameListWithoutOperator(IEnumerable<Field> fields)
        {
            IList<string> PlantNameList = new List<string>();
            foreach (var field in fields)
            {
                foreach (var parcel in field.Parcels)
                {
                    if(parcel.Operator==null && !PlantNameList.Contains(parcel.GetPlantName()))
                    {
                        PlantNameList.Add(parcel.GetPlantName());
                    }
                }
            }
            return PlantNameList;
        }
        public static int GetCultivatedAreaByPlantNameWithoutOperator(IEnumerable<Field> fields, string plantName)
        {
            int area = 0;
            foreach (var field in fields)
            {
                foreach (var parcel in field.Parcels)
                {
                    if (parcel.Operator == null && parcel.GetPlantName().Equals(plantName))
                    {
                        area += parcel.CultivatedArea;
                    }
                }
            }
            return area;
        }
        // Get plant name sawn on field[arg1] from list of fields[arg0]
        public static string GetPlantName(IEnumerable<Field> fields, Field field)
        {
            var fieldName = field.Name;
            var plant = fields.FirstOrDefault(x => x.Name.Equals(fieldName))?.Plant;
            if(plant!= null)
            {
                return plant.Name;
            }
            return "Brak danych";
        }
    }
}
