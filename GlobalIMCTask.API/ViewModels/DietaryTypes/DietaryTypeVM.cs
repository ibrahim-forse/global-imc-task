using GlobalIMCTask.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalIMCTask.API.ViewModels.DietaryTypes
{
    public class DietaryTypeVM
    {
        public int Id { set; get; }
        public string Name { set; get; }
    }

    public static class DietaryTypesConverters
    {
        public static DietaryTypeVM ConvertDietaryTypeToVM(this DietaryType dietaryType)
        {
            return new DietaryTypeVM()
            {
                Name = dietaryType.Name,
                Id = dietaryType.Id
            };
        }

        public static List<DietaryTypeVM> ConvertDietaryTypesToVMs(this List<DietaryType> dietaryTypes)
        {
            List<DietaryTypeVM> vms = new List<DietaryTypeVM>();
            foreach(var dietaryType in dietaryTypes)
            {
                vms.Add(dietaryType.ConvertDietaryTypeToVM());
            }
            return vms;
        }
    }
}
