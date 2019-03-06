using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace ShopApp
{
    /// <summary>
    /// Interaction logic for Measure.xaml
    /// </summary>
    public partial class Measure : UserControl
    {
        public Measure()
        {
            InitializeComponent();

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            // Do not load your data at design time.
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                //Load your data here and assign the result to the CollectionViewSource.
                LoadData();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddMeasure measure = new AddMeasure(this);
            measure.Show();
        }

        public void LoadData()
        {
           
            try
            {
                string query = @"select Njesia_ID,Njesia_Desc,Njesia_Name,u.Username as Created_By,Created_Date from Njesite n 
                inner join Users u on u.User_ID = n.Created_By";
                using (SqlConnection con = new SqlConnection(Utility.ConnStr))
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable("Njesite");
                    sda.Fill(dt);
                    njesiteDataGrid.ItemsSource = dt.DefaultView;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
