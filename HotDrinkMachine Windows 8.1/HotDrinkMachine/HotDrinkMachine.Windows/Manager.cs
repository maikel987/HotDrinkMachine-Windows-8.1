using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace HotDrinkMachine
{
    class Manager
    {

        private MainPage mainPage;
        private VendingMachine VendingMan;
        private ButtonManager btManager;
        private Canvas cnv;
        private Canvas cnv2;
        
        private Ingredient tea;
        private Ingredient coffee;
        private Ingredient chocoPowder;
        private Ingredient milk;

        public IngredientUI ingUIList;




        public Manager(MainPage mainPage, Canvas cnv, Canvas secondCnv)
        {
            this.mainPage = mainPage;
            this.cnv = cnv;
            this.cnv2 = secondCnv;

            ingUIList = new IngredientUI(cnv2); // initialize the ingredient List and his UI
            VendingMan = new VendingMachine(mainPage, cnv, 6); //initialize vending Machine with 6 places for beverages 
            btManager = new ButtonManager(cnv); // initialise button Management
            VendingMan.SetButtonManager(btManager); // initialise button Management in the vending Machine
            VendingMan.SubsidiaryInitialisation(5, 5); // initialize Subsidiary (cup & sugar)
            IngredientInitialisation(); // initialize the Ingredient 



            //create the beverage
            Coffee coffeeBev = new Coffee("Milky Coffee", 10, 100, 3, 15, 1, 1, coffee, milk);
            Tea teaBev = new Tea("Tea", 11, 90, 1, 30, 1, tea);
            Chocolate chocoBev = new Chocolate("Chocolate", 15, 95, 5, 25, 1, 1, chocoPowder, milk);

            //add the beverage to the vending machine 
            VendingMan.NewBeverage(coffeeBev);
            VendingMan.NewBeverage(teaBev);
            VendingMan.NewBeverage(chocoBev);

            //Let's create an Expresso for an example
            Coffee expressoBev = new Coffee("Expresso", 5, 100, 1, 10, 1, 0, coffee, milk);
            VendingMan.NewBeverage(expressoBev);
        }

          

        

      


        //let's set up the principal ingredient and their stock:
        public void IngredientInitialisation()
        {

            tea = new Ingredient(10, "tea leaves");
            ingUIList.Add(tea, mainPage.RefillTea, mainPage.TbTeaQtity);

            coffee = new Ingredient(11, "coffee beans");
            ingUIList.Add(coffee, mainPage.RefillCoffee, mainPage.TbCoffeeQtity);

            chocoPowder = new Ingredient(12, "chocolate powder");
            ingUIList.Add(chocoPowder, mainPage.RefillChocolate, mainPage.TbChocolateQtity);

            milk = new Ingredient(13, "milk");
            ingUIList.Add(milk, mainPage.RefillMilk, mainPage.TbMilkQtity);
        }

        //Allow to Refill Cup
        public void AddCup(int quantity)
        {
            VendingMan.cup.Refill(quantity);
        }

        //Allow to Refill Sugar
        public void AddSugar(int quantity)
        {
            VendingMan.sugar.Refill(quantity);
        }

        //Allow to check Sugar Stock
        public string SugarStockInfo()
        {
            return string.Format("{0}{1} units", VendingMan.sugar.ToString(), VendingMan.sugar.Stock.ToString());
        }

        //Allow to check Cup Stock
        public string CupStockInfo()
        {
            return string.Format("{0}{1} units", VendingMan.cup.ToString(), VendingMan.cup.Stock.ToString());
        }

        //Allow to Add Beverage to the Vending Machine
        public void NewBeverage(Beverage bev)
        {
            try
            {
                VendingMan.NewBeverage(bev);
            }
            catch (IndexOutOfRangeException message)
            {
                string answ = string.Format("Too much type of beverage, error: \n {0}", message.ToString());
                mainPage.ErrorMessage(answ);
            }
        }
        // a simple trick to add 's' for plurial element 
         public string Plural(int data)
        {
            if (data > 1) return "s";
            else return null;
        }

        public void CreateIngredientToUI(int IngredientQtity, string IngredientName)
        {
            Ingredient newIngre = new Ingredient(IngredientQtity, IngredientName);

            //Create CheckBox
            CheckBox chBox = new CheckBox();
            chBox.Content = IngredientName;
            ingUIList.CreateCheckBoxToUI(chBox);

            //Create TextBox
            TextBox txBox = new TextBox();
            txBox.PlaceholderText = string.Format(IngredientName + " Quantity");
            ingUIList.CreateTextBoxToUI(txBox);


            //link Ingredient-CheckBox-TextBox
            ingUIList.Add(newIngre, chBox, txBox);

            //textBox return to Default
            mainPage.textBoxNewIngregientName.Text = string.Empty;
            mainPage.textBoxNewIngregientQuantity.Text = string.Empty;
        }

        //Allow from the UI to create a new beverage.. a bit long but there are a lot of variable to manage.
        public void CreateBeverageFromUI()
        {
            //Variable Declaration
            bool flag = true;
            int secondFlag = 0;
            string name = string.Empty;
            if (mainPage.TbName.Text == string.Empty) flag = false; // if name is empty it will give an Error later.
            else name = mainPage.TbName.Text;
            RecipeIngredient[] recipe;

            //Cout how much Ingredient in the recip
            for (int i = 0; i < ingUIList.Count; i++)
            {
                if (ingUIList.GetTextBox(i).Text != string.Empty) secondFlag++;  
            }

            //if no Ingredient... Error.
            if (secondFlag == 0)
            {
                mainPage.ErrorMessage("Invalid Input Data");
            }
            else
            {   // else it give the length of ingredient Array
                recipe = new RecipeIngredient[secondFlag];
                int tmpIndex = 0;

                // And affect each Ingredient to the Array.
                for (int i = 0; i < ingUIList.Count; i++)
                {
                    int Qtity;
                    if (int.TryParse(ingUIList.GetTextBox(i).Text, out Qtity))
                    {
                        if (Qtity > 0) recipe[tmpIndex++] = new RecipeIngredient(Qtity, ingUIList.GetIngredient(i));
                    }
                }

                // Now we verify the Other Variable
                double price = 0;
                int temperature = 0;
                int stiringTime = 0;
                int WaterQuantityInMl = 0;
                flag = flag 
                    && int.TryParse(mainPage.tbTemperature.Text, out temperature)
                    && int.TryParse(mainPage.tbStiring.Text, out stiringTime)
                    && int.TryParse(mainPage.tbWaterQtity.Text, out WaterQuantityInMl)
                    && double.TryParse(mainPage.tbPrice.Text, out price);

                //and if everything went correctly... 
                if (flag)
                {
                    //we create the beverage with all the variable
                    OtherBeverage newBev = new OtherBeverage(name, price, temperature, stiringTime, WaterQuantityInMl, recipe);
                    NewBeverage(newBev);

                    // and return to original value the UI
                    for (int i = 0; i < ingUIList.Count; i++)
                    {
                        ingUIList.GetTextBox(i).Text = string.Empty;
                    }
                    mainPage.TbName.Text = string.Empty;
                    mainPage.tbPrice.Text = string.Empty;
                    mainPage.tbTemperature.Text = string.Empty;
                    mainPage.tbStiring.Text = string.Empty;
                    mainPage.tbWaterQtity.Text = string.Empty;
                }
                //and if error: 
                else mainPage.ErrorMessage("Invalid Input Data");
            }
        }

    }
}
