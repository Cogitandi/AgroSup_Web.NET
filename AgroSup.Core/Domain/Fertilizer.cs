using System;
using System.Collections.Generic;
using System.Text;

namespace AgroSup.Core.Domain
{
    public class Fertilizer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string N { get; set; }
        public string P { get; set; }
        public string K { get; set; }
        public string Ca { get; set; }
        public string Mg { get; set; }
        public string S { get; set; }
        public string Na { get; set; }
    }
}
