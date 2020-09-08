using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public IEnumerable<Field> Fields => _fields;
        public IEnumerable<Operator> Operators => _operators;

        public string GetYearPlanName
        {
            get
            {
                return "Sezon " + StartYear + "/" + EndYear;
            }
        }
    }
}
