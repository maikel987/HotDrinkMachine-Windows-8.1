using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotDrinkMachine
{


    public class Ingredient
    {
        private int stock;
        public int Stock { get { return stock; } set { stock = value; } }

        internal string name;
        public Ingredient(int stock, string name) // We force the introduction of ingredient to associated with a defined stock.
        {
            this.name = name;
            this.stock = stock;
        }

        public Ingredient(string name) : this(100, name) // stock by default 100
        {

        }

        public override string ToString()
        {
            return string.Format("{0} ", name);
        }

        public void Refill (int refillQuantity)
        {
            stock += refillQuantity;
        }


    }
}
