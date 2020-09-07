using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;



namespace AgroSup.Core.Domain
{
    public class User : IdentityUser<Guid>
    {
        private IList<YearPlan> _yearPlans = new List<YearPlan>();

        public YearPlan ManagedYearPlan { get; set; }
        public IEnumerable<YearPlan> YearPlans => _yearPlans;
    }
}
