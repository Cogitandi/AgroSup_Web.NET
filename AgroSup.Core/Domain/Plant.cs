using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AgroSup.Core.Domain
{
    public class Plant
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int EfaNitrogenRate { get; set; }
    }
}
