using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HotDrinkMachine
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Manager manager;
        
        public MainPage()
        {
            this.InitializeComponent();
            try
            {

                manager = new Manager(this, cnv, cnv2);
            }
            catch (RunOutException e)
            {   textBlock.Text = string.Format(" Exception caught, type: {0}", e.GetType());
                textBlock.Text += string.Format("\n Run Out of {0}", e.Source);
                textBlock.Text += string.Format("\n {0}",e.ToString());
            }
            catch (IndexOutOfRangeException e)
            {
                textBlock.Text = string.Format("{0}", e.ToString());
            }
            catch (ArgumentException e)
            {
                textBlock.Text = string.Format("{0}", e.ToString());
            }
            catch (Exception e)
            {
                textBlock.Text = string.Format("{0}", e.ToString());
            }

            finally
            {

                textBlock.Text = string.Format("");
            }
        }
        

        private void image1_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            image1.Opacity = 1;

        }

        public int sugarNumber=0;
        private void button_Click(object sender, RoutedEventArgs e)
        {
            sugarNumber = (sugarNumber + 1) % 6;            
            textBlock.Text = string.Format("You selected to add {0} sugar{1}...", sugarNumber,manager.Plural(sugarNumber));
        }

        public int totalIntroducedCoin = 0;
        private void Coin_Click(object sender, RoutedEventArgs e)
        {
            Button tmp = sender as Button;
            switch (tmp.Name)
            {
                case "bt_10":
                    totalIntroducedCoin += 10;
                    break;
                case "bt_1":
                    totalIntroducedCoin += 1;
                    break;
                case "bt_2":
                    totalIntroducedCoin += 2;
                    break;
                case "bt_5":
                    totalIntroducedCoin += 5;
                    break;
            }
            textBlock.Text = string.Format("you introduce {0} Shekel{1}", totalIntroducedCoin, manager.Plural(totalIntroducedCoin));
        }

        private void btManager_Click(object sender, RoutedEventArgs e)
        {
            MessageDialogManager();
        }
        
        private void btAddIngredient_Click(object sender, RoutedEventArgs e)
        {
            string IngredientName = textBoxNewIngregientName.Text;
            int IngredientQtity;
            if (int.TryParse(textBoxNewIngregientQuantity.Text, out IngredientQtity) && IngredientName != string.Empty)
            {
                manager.CreateIngredientToUI(IngredientQtity, IngredientName);
            }
            else ErrorMessage("Invalid Input Data");
        }

        private void btNewBeverage_Click(object sender, RoutedEventArgs e)
        {
            manager.CreateBeverageFromUI();
        }

        private void buttonRefill_Click(object sender, RoutedEventArgs e)
        {
            int quantity;
            if (int.TryParse(textBoxRefill.Text, out quantity) && quantity < 101 && quantity > 0) 
            {
                if (RefillSugar.IsChecked == true) manager.AddSugar(quantity) ;
                if (RefillCup.IsChecked == true) manager.AddCup(quantity);

                for (int i = 0; i < manager.ingUIList.Count; i++)
                {
                    if (manager.ingUIList.GetCheckBox(i).IsChecked == true)
                    {
                        manager.ingUIList.RefillIngredient(i, quantity);
                        manager.ingUIList.GetCheckBox(i).IsChecked = false;                
                    }
                }
                textBoxRefill.Text = string.Empty;
            }else
            {
                ErrorMessage("Invalid Input Data");
            }
    }



        public async void ErrorMessage(string message)
        {           
            MessageDialog dlg = new MessageDialog(message, "Manager Interface, Error");
            await dlg.ShowAsync();
        }

        private async void MessageDialogManager()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Subsidiary : ");
            sb.AppendLine(manager.SugarStockInfo());
            sb.AppendLine(manager.CupStockInfo());
            sb.AppendLine();
            sb.AppendLine("Ingredient :");
            for (int i = 0; i < manager.ingUIList.Count; i++)
            {
                sb.Append(manager.ingUIList.GetIngredientName(i));
                sb.Append(manager.ingUIList.GetIngredientStock(i).ToString());
                if (manager.ingUIList.GetIngredientStock(i) > 1)
                    sb.AppendLine("units");
                else sb.AppendLine("unit");
            }
            MessageDialog dlg = new MessageDialog(sb.ToString(), "Manager Interface, this is your Stock: ");

            await dlg.ShowAsync();
        }

    }
}
