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

    public class ButtonManager
    {
        public event EventHandler<ButtonManagerEventArgs> OnButtonManagerClicked;
        List<Button> listBt = new List<Button>();
        public Canvas cnv;
        int btnNumber;
        public ButtonManager(Canvas cnv)
        {
            this.cnv = cnv;
            btnNumber = -1; 
        }

        //We create all the button from the beginnig but we are assigning the button one by one in the same time that we create new beverage
        public void BuildButton(int numberButton)
        {
            int cnvLeft = 824;
            int cnvTop = 177;
            int cnvTopSpace = 43;
            for (int i = 0; i < numberButton; i++)
            {

                Button bt = new Button();
                Canvas.SetLeft(bt, cnvLeft);
                Canvas.SetTop(bt, cnvTop + i * cnvTopSpace);
                bt.Background = new SolidColorBrush(Color.FromArgb(255, 236, 235, 240)); 
                bt.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 116, 85, 80)); 
                bt.Foreground = new SolidColorBrush(Colors.Black);
                bt.Width = 117;
                bt.Height = 45;
                bt.FontSize = 14;
                bt.Click += Bt_Click;
                bt.Name = i.ToString();
                bt.FontFamily = new FontFamily("Segoe Print");
                listBt.Add(bt);
                cnv.Children.Add(bt);
            }
        }
        // Initialise event in case of click..
        private void Bt_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Button bt = sender as Button;
            if (bt != null)
            {
                OnButtonManagerClicked?.Invoke(this, new ButtonManagerEventArgs
                {
                    ClickedButton = bt
                });

            }
        }

        public void AddButton(string content) //pushMethode
        {
            if (btnNumber == listBt.Count - 1) throw new IndexOutOfRangeException();
            btnNumber++;
            listBt[btnNumber].Content = content;
        }

        public void RemoveButton(int index) // by index 0-based
        {
            if (index > btnNumber) throw new IndexOutOfRangeException();
            for (int i = index; i < btnNumber; i++)
            {
                listBt[i].Content = listBt[i + 1].Content;
            }
            listBt[btnNumber].Content = null;
            btnNumber--;   
        }
    }

    public class ButtonManagerEventArgs : EventArgs
    {
        public Button ClickedButton { get; set; }
    }
}
