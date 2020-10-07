using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AgroSup.Core.Domain
{
    public class UserPlant
    {
        public Guid Id { get; set; }
        [Required]
        public User User { get; set; }
        [Required]
        public Plant Plant { get;set; }
    }
}
