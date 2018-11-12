using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace HotDrinkMachine
{
    public delegate void ShowMessage(string message);

    public class VendingMachine
    {
        private Beverage[] beveArr;
        private int index;

        private MainPage mainPage;
        private Canvas cnv;

        public Subsidiary cup;
        public Subsidiary sugar;



        ButtonManager btMan;

        //ctor
        public VendingMachine(MainPage mainPage, Canvas cnv, int beverageNumber)
        {
            this.mainPage = mainPage;
            this.cnv = cnv;
            beveArr = new Beverage[beverageNumber];
            index = -1;
        }

        //initialize Subsidiary
        public void SubsidiaryInitialisation(int sugarQuantity, int cupQuantity)
        {
            sugar = new Subsidiary(sugarQuantity, "sugar");
            cup = new Subsidiary(cupQuantity, "cup");

        }

        // intitialise after ctor other element.
        public void SetButtonManager(ButtonManager btnManager)
        {
            btMan = btnManager;
            btMan.BuildButton(beveArr.Length);
            btMan.OnButtonManagerClicked += BtMan_OnButtonManagerClicked;

        }

        //Click Handler
        private void BtMan_OnButtonManagerClicked(object sender, ButtonManagerEventArgs e)
        {

            try
            {
                Selector(e.ClickedButton, ShowOnMachineScreen);
            }
            catch (Exception)
            {
                ShowOnMachineScreen("Problem with the Machine, Call the Manager..");
            }
        }

        //quick Method to Show on screen problems
        public void ShowOnMachineScreen(string message)
        {
            mainPage.textBlock.Text = message;
        }



        
        public void Selector(Button btn, ShowMessage showMessage)
        {
            try
            {
                int btRef;
                // take the reference of Button
                if (int.TryParse(btn.Name, out btRef)) 
                {
                    // verify that the buton is affected
                    if (btRef > index) mainPage.textBlock.Text = "Beverage Non-avalable... Please Select another...";
                    else
                    {
                        // verify if the money introduced is enought
                        if (mainPage.totalIntroducedCoin < beveArr[btRef].price)
                        {
                            mainPage.textBlock.Text = string.Format("Please, insert {0} Shekel...", beveArr[btRef].price - mainPage.totalIntroducedCoin);
                        }
                        else
                        {   // verify if the drinker want sugar (to introduce it before prepare
                            if (mainPage.sugarNumber > 0)
                            {
                                mainPage.textBlock.Text = string.Format("{0}\n", sugar.Introduce(mainPage.sugarNumber));
                                mainPage.textBlock.Text += string.Format("{0}", beveArr[btRef].Prepare());
                            }
                            else
                            {
                                mainPage.textBlock.Text = string.Format("{0}", beveArr[btRef].Prepare());
                            }
                            // decrease to one cup stock
                            cup.Use(1);

                            //return to 0 sugar wanted, 
                            mainPage.sugarNumber = 0;

                            //give back the change
                            mainPage.tBReturnedMoney.Text = string.Format("You've got {0} Shekel Back", mainPage.totalIntroducedCoin - beveArr[btRef].price);


                            //return to 0 coin introduced
                            mainPage.totalIntroducedCoin = 0;

                            //Show the beverage
                            mainPage.image1.Opacity = 0;
                        }
                    }
                }

            }
            catch (RunOutException Ex)
            {
                showMessage(Ex.Message);
                mainPage.sugarNumber = 0;

            }
        }
        
        public void RemoveBeverage(int ind)
        {
            if (ind > index) throw new IndexOutOfRangeException();
            else
            {
                for (int i = ind; i < index; i++)
                {
                    beveArr[i] = beveArr[i + 1];
                }
                beveArr[index] = default(Beverage);
                index--;
            }
            btMan.RemoveButton(ind);
        }

        public void NewBeverage(Beverage bev)
        {
            if (index == beveArr.Length - 1)
                throw new IndexOutOfRangeException();
            else
            {
                index++;
                btMan.AddButton(bev.name);
                beveArr[index] = bev;

            }
        }

        //public void NewBeverage(string name, double price, int temperature, int stiringTime, Recipe recipe, int WaterQuantityInMl)
        //{
        //    if (index == beveArr.Length - 1)
        //        throw new IndexOutOfRangeException();
        //    else
        //    {
        //        index++;
        //        OtherBeverage newBev = new OtherBeverage(name, price, temperature, stiringTime, recipe, WaterQuantityInMl);
        //        btMan.AddButton(name);
        //        beveArr[index] = newBev;

        //    }
        //}

        


    }
}
