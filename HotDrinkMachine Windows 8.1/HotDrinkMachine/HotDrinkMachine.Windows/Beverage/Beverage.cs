using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotDrinkMachine
{
    public abstract class Beverage
    {


        public string name {  get; private set; }
        public double price { get; private set; }
        internal Recipe recipe {  get; private set; }
        private int waterQtityMl;
        internal int temperature { get; private set; }
        internal int stiringTime { get; private set; }

        public Beverage(string name, double price, int temperature, int stiringTime, Recipe recipe,int WaterQuantityInMl)
        {
            this.name = name;
            this.price = price;
            this.recipe = recipe;
            this.temperature = temperature;
            this.stiringTime = stiringTime;
            waterQtityMl = WaterQuantityInMl;
        }

        // We let the ctor to build it self the Recip also,
        public Beverage(string name, double price, int temperature, int stiringTime, int WaterQuantityInMl, params RecipeIngredient[] recip) 
            : this (name, price, temperature, stiringTime, new Recipe (recip), WaterQuantityInMl)
        {
        }




    public string Prepare()
        {
            StringBuilder preparation = new StringBuilder();
            preparation.Append(AddIngredients());
            preparation.Append(AddHotWater());
            preparation.Append(Stirring());
            return preparation.ToString();
        }

        public virtual string Stirring()
        {
            if (stiringTime == 0) return string.Empty;
            
            StringBuilder stiring = new StringBuilder();
            stiring.Append(string.Format("stiring {0} time the {1}...", stiringTime, name));
            stiring.AppendLine();
            return stiring.ToString();

        }

        public virtual string AddIngredients()
        {
            return recipe.Compose();
        }

        public virtual string AddHotWater()
        {
            StringBuilder hotWaterCompo = new StringBuilder();
            string temp;
            if (temperature > 30) temp = "hot ";
            else temp = null;
            hotWaterCompo.AppendFormat("Pouring {0} water: {1}°C",temp,temperature.ToString());
            hotWaterCompo.AppendLine();
            return hotWaterCompo.ToString();
        }



    }
}
