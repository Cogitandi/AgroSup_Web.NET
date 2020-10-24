using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace AgroSup.Core.Domain
{
    public class User : IdentityUser<Guid>
    {
        public DateTime LastLoginDate { get; set; }
        public DateTime CreateDate { get; set; }

        public YearPlan ManagedYearPlan { get; set; }
        public IList<UserPlant> ChoosedPlants { get; set; } = new List<UserPlant>();
        public IEnumerable<YearPlan> YearPlans { get; set; }

        public void AddPlantToChoosed(Plant plant)
        {
            foreach(var choosedPlant in ChoosedPlants)
            {
                if (choosedPlant.Plant == plant)
                    return;
            }
            var userPlant = new UserPlant
            {
                Plant = plant
            };
            this.ChoosedPlants.Add(userPlant);
        }
        public void RemovePlantFromChoosed(Plant plant)
        {
            var userPlant = ChoosedPlants.Where(x=>x.Plant==plant).FirstOrDefault();
            if(userPlant!=null)
            {
                this.ChoosedPlants.Remove(userPlant);
            }
        }
        public static IEnumerable<User> GetInActiveUsers(IEnumerable<User> users)
        {
            // stworzone i nigdy nie zalogowane przez 90 dni
            // lub nie logowane od 90dni i nie utworzono nic
            int DaysWithoutLogin = 90;
            var TodayDate = DateTime.Now;

            return users
                .Where(x => x.YearPlans.Count() == 0)
                .Where(x => TodayDate.Subtract(x.CreateDate).Days >= DaysWithoutLogin)
                .Where(x => x.LastLoginDate == null || TodayDate.Subtract(x.LastLoginDate).Days >= DaysWithoutLogin)
                .ToList();
        }
    }

}
    
