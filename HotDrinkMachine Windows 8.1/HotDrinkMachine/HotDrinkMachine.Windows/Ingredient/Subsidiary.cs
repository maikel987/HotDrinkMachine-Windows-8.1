using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotDrinkMachine
{
    // as beverage drinker not always want sugar.. and as we don't show any message for cups
    //we created a different Class for them.
    // that allowed:  - to call cup without showing any message -> just to reduce the cup'stock
    //                - to put sugar only if the drinker want.
    public class Subsidiary : Ingredient
    {
        public Subsidiary(int quantity, string name):base (quantity,name)
        {

        }

        public string Introduce(int quantity)
        {
            if (Stock < quantity)
            {
                string excep = string.Format("Not Enought Stock of {0}", name);
                throw new RunOutException(excep);
            }
            else
            {
                Stock -= quantity;
                char s;
                if (quantity > 1) s = 's';
                else s = default(char);

                return string.Format("adding {0} time{1} {2}... ", quantity,s, name.ToString());
            }
            
        }

        public void Use(int quantity)
        {
            if (Stock < quantity)
            {
                string excep = string.Format("Not Enought Stock of {0}", name);
                throw new RunOutException(excep);
            }
            else
            {
                Stock -= quantity;
            }
        }
    }
}
