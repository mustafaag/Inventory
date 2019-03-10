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
using System.Windows.Shapes;

namespace ShopApp
{
    /// <summary>
    /// Interaction logic for MWindow.xaml
    /// </summary>
    public partial class MWindow : Window
    {
        public MWindow()
        {
            InitializeComponent();
            txtUsername.Text = Utility.UserName;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void DataGrid_SelectionChanged_2(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_2(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged_3(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_4(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_5(object sender, TextChangedEventArgs e)
        {

        }
        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
           
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
            
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserControl usc = null;
            GridMain.Children.Clear();

            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "Reparte":
                    usc = new RepartControl();
                    GridMain.Children.Add(usc);
                    break;
                case "ItemCreate":
                    usc = new UserControlCreate();
                    GridMain.Children.Add(usc);
                    break;
                case "Groups":
                    usc = new GroupsControl();
                    GridMain.Children.Add(usc);
                    break;
                case "Depot":
                    usc = new DepotControl();
                    GridMain.Children.Add(usc);
                    break;
                case "Measures":
                    usc = new Measure();
                    GridMain.Children.Add(usc);
                    break;
                case "SubGroups":
                    usc = new SubGroupControl();
                    GridMain.Children.Add(usc);
                    break;
                case "Articles":
                    usc = new ArticlesControl();
                    GridMain.Children.Add(usc);
                    break;
                case "Workflow":
                    usc = new WorkFlowControl();
                    GridMain.Children.Add(usc);
                    break;
                case "Receptura":
                    usc = new RecepturaControl();
                    GridMain.Children.Add(usc);
                    break;
                case "Transfers":
                    usc = new MovesControl();
                    GridMain.Children.Add(usc);
                    break;
                default:
                    break;

                   
            }
            
        }
    }
}
