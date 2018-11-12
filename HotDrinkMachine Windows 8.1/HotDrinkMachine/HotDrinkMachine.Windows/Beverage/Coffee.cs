using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotDrinkMachine
{
     class Coffee : Beverage
    {
        

        // public Coffee(string name, double price, int temperature, int stiringTime, Recipe recipe, int WaterQuantityInMl) : base (name, price, temperature, stiringTime, recipe, WaterQuantityInMl)
        //{
        //}

        public Coffee(string name, double price, int temperature, int stiringTime, int WaterQuantityInMl, int coffeeQuantity,  int milkQuantity, Ingredient coffee, Ingredient milk) 
            : base(name, price, temperature, stiringTime, WaterQuantityInMl, new RecipeIngredient(coffeeQuantity, coffee), new RecipeIngredient(milkQuantity, milk))
        {
        }

      

        public override string Stirring()
        {
            if (stiringTime == 0) return string.Empty;

            StringBuilder stiring = new StringBuilder();
            stiring.Append(string.Format("stiring quicly {0} times the {1}...", stiringTime, name));
            stiring.AppendLine();
            return stiring.ToString();

        }

        public override string AddIngredients()
        {
            return recipe.Compose();
        }

        public override string AddHotWater()
        {
            StringBuilder hotWaterCompo = new StringBuilder();            
            hotWaterCompo.AppendFormat("Boiling water to {0}°C", temperature.ToString());
            hotWaterCompo.AppendLine();
            return hotWaterCompo.ToString();
        }





    }
}
