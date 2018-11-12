using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotDrinkMachine
{
    class OtherBeverage : Beverage
    {
        public OtherBeverage(string name, double price, int temperature, int stiringTime, Recipe recipe, int WaterQuantityInMl) : base(name, price, temperature, stiringTime, recipe, WaterQuantityInMl)
        {
        }
        public OtherBeverage(string name, double price, int temperature, int stiringTime, int WaterQuantityInMl, params RecipeIngredient[] recip) 
            : base (name, price, temperature, stiringTime, new Recipe (recip), WaterQuantityInMl)
        {


        }

    }
}
