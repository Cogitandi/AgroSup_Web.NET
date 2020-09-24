using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace AgroSup.Core.Domain
{
    public class YearPlan
    {
        private IList<Field> _fields = new List<Field>();
        private IList<Operator> _operators = new List<Operator>();

        public Guid Id { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public User User { get; set; }
        public IEnumerable<Field> Fields { get; set; }
        public IEnumerable<Operator> Operators { get; set; }

        public string GetYearPlanName
        {
            get
            {
                return "Sezon " + StartYear + "/" + EndYear;
            }
        }
        public void GetDataToImport(YearPlan yearPlan)
        {
            foreach(var field in yearPlan.Fields)
            {
                field.Id = Guid.Empty;
                foreach(var parcel in field.Parcels)
                {
                    parcel.Id = Guid.Empty;
                }
            }
            foreach (var @operator in yearPlan.Operators)
            {
                @operator.Id = Guid.Empty;
                
            }

            this.Fields = yearPlan.Fields.ToList();
            this.Operators = yearPlan.Operators.ToList();
        }
    }
}
