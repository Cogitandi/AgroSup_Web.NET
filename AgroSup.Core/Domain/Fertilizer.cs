﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AgroSup.Core.Domain
{
    public class Fertilizer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int N { get; set; }
        public int P { get; set; }
        public int K { get; set; }
        public int Ca { get; set; }
        public int Mg { get; set; }
        public int S { get; set; }
        public int Na { get; set; }
    }
}
