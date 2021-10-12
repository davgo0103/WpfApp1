using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {

        List<Drink> drinks = new List<Drink>();
        List<Orderitem> order = new List<Orderitem>();
        string takeout;

        public MainWindow()
        {
            InitializeComponent();

            
            //新增飲料品項
            drinks.Add(new Drink() { Name = "咖啡",Size="大杯",Price = 60 });
            drinks.Add(new Drink() { Name = "咖啡",Size="中杯",Price = 50 });
            drinks.Add(new Drink() { Name = "紅茶",Size="大杯",Price = 30 });
            drinks.Add(new Drink() { Name = "紅茶",Size="中杯",Price = 20 });
            drinks.Add(new Drink() { Name = "綠茶",Size="大杯",Price = 25 });
            drinks.Add(new Drink() { Name = "綠茶",Size="中杯",Price = 20 });

            //顯示所有飲料品項
            DisplayDrink(drinks);

        }

        private void DisplayDrink(List<Drink> myDrink)
        {
            foreach (Drink d in myDrink)
            {
                CheckBox cb = new CheckBox();
                StackPanel sp = new StackPanel();
                Slider sl = new Slider();
                Label lb = new Label();

                sp.Orientation = Orientation.Horizontal;
                cb.Content = d.Name + d.Size + d.Price;

                sl.Width = 150;
                lb.Width = 100;
                lb.Content = "0";
                sl.Maximum = 10;
                sl.Minimum = 0;
                sl.IsSnapToTickEnabled = true;

                sp.Children.Add(cb);
                sp.Children.Add(sl);
                sp.Children.Add(lb);

                Binding myBinding = new Binding("Value");
                myBinding.Source = sl;
                lb.SetBinding(ContentProperty,myBinding);

                DrinkMenu.Children.Add(sp);

            }


        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            //新增飲料訂購品項至order內
            PlaceOrder(order,drinks);

            //計算訂單
            CompletOrder(order,drinks);
        }

        private void CompletOrder(List<Orderitem> myorder,List<Drink> mydrink)
        {
            int total=0;
            int sellPrice = 0;
            string message = "";
            Textblock1.Text = $"您要{takeout}飲料，訂單如下:\n";
            for(int i=0;i< myorder.Count;i++)
            {
                Textblock1.Text += $"第{i+1}項:{mydrink[myorder[i].Index].Name}{mydrink[myorder[i].Index].Size}，每杯{mydrink[myorder[i].Index].Price}元，總共{myorder[i].Subtotal}元\n";
                total += myorder[i].Subtotal;
            }
            if(total >= 500)
            {
                message = "超過500元以上打8折";
                sellPrice = Convert.ToInt32(Math.Round(Convert.ToDouble(total * 0.8)));
            }
            else if (total >= 300)
            {
                message = "超過300元以上打85折";
                sellPrice = Convert.ToInt32(Math.Round(Convert.ToDouble(total * 0.85)));
            }
            else if (total >= 200)
            {
                message = "超過200元以上打9折";
                sellPrice = Convert.ToInt32(Math.Round(Convert.ToDouble(total * 0.9)));
            }
            Textblock1.Text += $"總共{sellPrice}元，{message}";
            
        }

        private void PlaceOrder(List<Orderitem> myorder,List<Drink> mydrink) 
        {
            myorder.Clear();
            for (int i = 0; i < DrinkMenu.Children.Count; i++)
            {
                StackPanel sp = DrinkMenu.Children[i] as StackPanel;
                CheckBox cb = sp.Children[0] as CheckBox;
                Slider sl = sp.Children[1] as Slider;
                int quantity = Convert.ToInt32(sl.Value);

                if (cb.IsChecked == true && quantity != 0)
                {
                    order.Add(new Orderitem() { Index = i, Quantity = quantity, Subtotal = drinks[i].Price * quantity });
                }

            }
        }

        private void Radiobutton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            takeout = rb.Content.ToString();

        }
    }
}
