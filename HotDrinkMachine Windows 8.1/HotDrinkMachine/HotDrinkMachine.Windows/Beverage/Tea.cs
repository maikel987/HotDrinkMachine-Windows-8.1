using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotDrinkMachine
{
    class Tea : Beverage
    {
        
        public Tea(string name, double price, int temperature, int stiringTime, int WaterQuantityInMl, int teaQuantity, Ingredient tea) 
            : base(name, price, temperature, stiringTime, WaterQuantityInMl, new RecipeIngredient(teaQuantity, tea))
        {
        }

        //public Tea(string name, double price, int temperature, int stiringTime, Recipe recipe, int WaterQuantityInMl) : base(name, price, temperature, stiringTime, recipe, WaterQuantityInMl)
        //{
        //}

        //public Tea(string name, double price, int temperature, int stiringTime, int WaterQuantityInMl, int teaQuantity, int sugarQuantity, int milkQuantity, Ingredient tea, Ingredient sugar, Ingredient milk) 
        //    : base(name, price, temperature, stiringTime, WaterQuantityInMl, new RecipeIngredient(teaQuantity, tea),new RecipeIngredient(sugarQuantity, sugar), new RecipeIngredient(milkQuantity, milk))
        //{
        //}

        public override string Stirring()
        {
            if (stiringTime == 0) return string.Empty;

            StringBuilder stiring = new StringBuilder();
            stiring.Append(string.Format("stiring delicately {0} the {1}...", stiringTime, name));
            stiring.AppendLine();
            return stiring.ToString();

        }

       

        public override string AddHotWater()
        {
            StringBuilder hotWaterCompo = new StringBuilder();
            string temp;
            if (temperature > 30) temp = "hot ";
            else temp = null;
            hotWaterCompo.AppendFormat("Pouring delicately {0} water: {1}°C", temp, temperature.ToString());
            hotWaterCompo.AppendLine();
            return hotWaterCompo.ToString();
        }
    }
}
