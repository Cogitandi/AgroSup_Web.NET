using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace AgroSup.Core.Domain
{
    public class YearPlan
    {
        public Guid Id { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public User User { get; set; }
        public IList<Field> Fields { get; set; }
        public IList<Operator> Operators { get; set; }

        public string GetYearPlanName
        {
            get
            {
                return "Sezon " + StartYear + "/" + EndYear;
            }
        }
        public void GetDataToImport(YearPlan yearPlan)
        {
            this.Fields = yearPlan.Fields.Select(x => new Field(x)).ToList();
            this.Operators = yearPlan.Operators.Select(x => new Operator(x)).ToList();
            foreach(var @operator in this.Operators)
            {
                var oldId = @operator.Id;
                @operator.Id = Guid.NewGuid();
                foreach(var field in Fields)
                {
                    foreach(var parcel in field.Parcels)
                    {
                        var op = parcel.Operator;
                        if (op!=null && op.Id == oldId)
                        {
                            parcel.Operator = @operator;
                        }
                    }
                }
            }
        }
        public static int GetTotalFieldsArea(IEnumerable<YearPlan> yearPlans)
        {
            int totalArea = 0;
            foreach(var item in yearPlans)
            {
                foreach(var field in item.Fields)
                {
                    totalArea += field.GetFieldArea();
                }
            }
            return totalArea;
        }
        public static int GetTotalFieldsCount(IEnumerable<YearPlan> yearPlans)
        {
            int counter = 0;
            foreach (var item in yearPlans)
            {
                counter += item.Fields.Count();
            }
            return counter;
        }
    }
}
