using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotDrinkMachine
{
    //Recipe is a List of RecipIngredient 
    //RecipIngredient is an association of Ingredient and quantity used for the beverage
    
         
    public class Recipe
    {
        private List<RecipeIngredient> RecipIngre = new List<RecipeIngredient>();
        public Recipe(params RecipeIngredient[] RecipIngre)
        {
            Add(RecipIngre);
        }



        public void Add(params RecipeIngredient[] recipindredient)
        {
            for (int i = 0; i < recipindredient.Length; i++)
            {
                RecipIngre.Add(recipindredient[i]);
            }
            
        }
        //is called when we want to prepare a beverage
        public string Compose()
        {
            StringBuilder composer = new StringBuilder();
            for (int i = 0; i < RecipIngre.Count; i++)
            {
                if (RecipIngre[i].GetComposition() != string.Empty)
                { 
                composer.Append(RecipIngre[i].GetComposition());
                composer.AppendLine();
                }
            }
            return composer.ToString();
        }

    }

    // To help to manage/create the different recip for beverage, 
    //we associate the ingredient with an (int) 
    //that correspond to the ingredient's quantity used
    public class RecipeIngredient
    {
        int quantity;
        Ingredient ingredient;

        public RecipeIngredient(int quantity, Ingredient ingredient)
        {
            this.quantity = quantity;
            this.ingredient = ingredient;
        }

        //is called when we want to prepare a beverage
        public string GetComposition()
        {
            if (ingredient.Stock < quantity)
            {
                string excep = string.Format("Not Enought Stock of {0}", ingredient.name);
                throw new RunOutException(excep);
            }
            else
            {
                if (quantity <= 0) return string.Empty;

                ingredient.Stock -= quantity;
                char s;
                if (quantity > 1) s = 's';
                else s = default(char);

                return string.Format("adding {0} time{1} {2}... ", quantity, s,ingredient.ToString());
            }

        }
    }
}
