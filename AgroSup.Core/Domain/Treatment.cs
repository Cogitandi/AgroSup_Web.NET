using System;
using System.Collections.Generic;
using System.Text;

namespace AgroSup.Core.Domain
{
    public class Treatment
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public Field Field { get; set; }
        public string Notes { get; set; }
    }
}
