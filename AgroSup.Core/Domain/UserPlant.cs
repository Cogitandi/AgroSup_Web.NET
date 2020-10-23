using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AgroSup.Core.Domain
{
    public class UserPlant
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public Plant Plant { get;set; }
    }
}
