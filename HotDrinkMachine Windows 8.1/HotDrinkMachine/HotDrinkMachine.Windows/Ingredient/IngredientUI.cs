using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace HotDrinkMachine
{
    public class IngredientUI
    {
        private Canvas cnv;
        private List<Ingredient> ingList = new List<Ingredient>();
        private List<CheckBox> chBoxList = new List<CheckBox>();
        private List<TextBox> txBoxList = new List<TextBox>();
        private int count;

        public int Count
        {
            get { return count; }
        }

        public IngredientUI(Canvas cnv)
        {
            count = 0;
            this.cnv = cnv;
        }

        public int GetIngredientStock(int index) 
        {
            if (index >= count) throw new IndexOutOfRangeException();
            else return ingList[index].Stock;
        }
        public string GetIngredientName(int index)
        {
            if (index >= count) throw new IndexOutOfRangeException();
            else return ingList[index].ToString();
        }


        public void RefillIngredient( int index,int quantity)
        {
            if (index >= count) throw new IndexOutOfRangeException();
            else ingList[index].Refill(quantity);
        }


        // to be sure that the 3 list have the same number of element we let the User Add or Remove
        //only Ingredient, CheckBox, and TextBox together.
        //so we are sure they have the same index.
        public void Add(Ingredient addIngredient, CheckBox addCheckBox, TextBox addTextBox)
        {
            ingList.Add(addIngredient);
            chBoxList.Add(addCheckBox);
            txBoxList.Add(addTextBox);
            count++;
        }
        public void Remove(int index)
        {
            if (index >= count) throw new IndexOutOfRangeException();

            ingList.RemoveAt(index);
            chBoxList.RemoveAt(index);
            txBoxList.RemoveAt(index);
            count--;
        }

        public Ingredient GetIngredient(int index)
        {
            if (index > count) throw new IndexOutOfRangeException();
            else
            {
                return ingList[index];
            }
        }
        public CheckBox GetCheckBox(int index)
        {
            if (index > count) throw new IndexOutOfRangeException();
            else
            {
                return chBoxList[index];
            }
        }
        public TextBox GetTextBox(int index)
        {
            if (index > count) throw new IndexOutOfRangeException();
            else
            {
                return txBoxList[index];
            }
        }

        //Let Create UIElement in Canvas (Employee Part)
        private int LastTextBoxPosition = 444;
        private int DeltaLastTextkBoxPosition = -36;
        public void CreateTextBoxToUI(TextBox txBox)
        {
            LastTextBoxPosition += DeltaLastTextkBoxPosition;
            Canvas.SetTop(txBox, LastTextBoxPosition);
            Canvas.SetLeft(txBox, 10);
            txBox.Background = new SolidColorBrush(Colors.White);
            txBox.BorderBrush = new SolidColorBrush(Colors.White);
            txBox.Foreground = new SolidColorBrush(Colors.Black);
            txBox.Width = 147;
            txBox.Height = 32;
            txBox.FontSize = 14.667;
            txBox.FontFamily = new FontFamily("Segoe UI");
            cnv.Children.Add(txBox);
        }

        private int LastCheckBoxPosition = 170;
        private int DeltaLastCheckBoxPosition = 32;
        public void CreateCheckBoxToUI(CheckBox chBox)
        {
            LastCheckBoxPosition += DeltaLastCheckBoxPosition;
            Canvas.SetTop(chBox, LastCheckBoxPosition);
            Canvas.SetLeft(chBox, 7);
            chBox.Foreground = new SolidColorBrush(Colors.White);
            chBox.Width = 186;
            chBox.Height = 24;
            chBox.FontSize = 14.667;
            chBox.FontFamily = new FontFamily("Segoe UI");
            cnv.Children.Add(chBox);
        }





    }
}
