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
        string takeout;

        public MainWindow()
        {
            InitializeComponent();

            
            //新增飲料品項
            drinks.Add(new Drink() { Name = "咖啡",Size="大杯",Price = 60 });
            drinks.Add(new Drink() { Name = "咖啡",Size="中杯",Price = 50 });
            drinks.Add(new Drink() { Name = "紅茶",Size="大杯",Price = 40 });
            drinks.Add(new Drink() { Name = "紅茶",Size="中杯",Price = 30 });
            drinks.Add(new Drink() { Name = "綠茶",Size="大杯",Price = 40 });
            drinks.Add(new Drink() { Name = "綠茶",Size="中杯",Price = 30 });
            drinks.Add(new Drink() { Name = "礦泉水",Size="大杯",Price = 20 });
            drinks.Add(new Drink() { Name = "礦泉水",Size="小杯",Price = 10 });

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
            
        }

        private void Radiobutton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            takeout = rb.Content.ToString();

        }
    }
}
