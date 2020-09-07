using System;
using System.Collections.Generic;
using System.Text;

namespace AgroSup.Core.Domain
{
    class Plant
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int EfaNitrogenRate { get; set; }
    }
}
