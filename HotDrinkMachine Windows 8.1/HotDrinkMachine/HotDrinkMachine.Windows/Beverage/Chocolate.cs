using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotDrinkMachine
{
    class Chocolate : Beverage
    {

        public Chocolate(string name, double price, int temperature, int stiringTime, int WaterQuantityInMl, int chocolateQuantity, int milkQuantity, Ingredient chocolate, Ingredient milk) 
            : base(name, price, temperature, stiringTime, WaterQuantityInMl, new RecipeIngredient(chocolateQuantity, chocolate), new RecipeIngredient(milkQuantity, milk))
        {
        }

        //public Chocolate(string name, double price, int temperature, int stiringTime, Recipe recipe, int WaterQuantityInMl) : base(name, price, temperature, stiringTime, recipe, WaterQuantityInMl)
        //{
        //}

        //public Chocolate(string name, double price, int temperature, int stiringTime, int WaterQuantityInMl, int chocolateQuantity, int sugarQuantity, int milkQuantity, Ingredient chocolate, Ingredient sugar, Ingredient milk) 
        //    : base(name, price, temperature, stiringTime, WaterQuantityInMl, new RecipeIngredient(chocolateQuantity, chocolate),new RecipeIngredient(sugarQuantity, sugar), new RecipeIngredient(milkQuantity, milk))
        //{
        //}

        public override string Stirring()
        {
            if (stiringTime == 0) return string.Empty;

            StringBuilder stiring = new StringBuilder();
            stiring.Append(string.Format("stiring {0} the {1}...", stiringTime, name));
            stiring.AppendLine();
            return stiring.ToString();

        }

      

        public override string AddHotWater()
        {
            StringBuilder hotWaterCompo = new StringBuilder();
            string temp;
            if (temperature > 30) temp = "hot ";
            else temp = null;
            hotWaterCompo.AppendFormat("Pouring {0} water: {1}°C", temp, temperature.ToString());
            hotWaterCompo.AppendLine();
            return hotWaterCompo.ToString();
        }

    }
}
