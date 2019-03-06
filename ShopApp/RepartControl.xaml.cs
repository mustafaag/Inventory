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
    /// Interaction logic for RepartControl.xaml
    /// </summary>
    public partial class RepartControl : UserControl
    {
        public RepartControl()
        {
            InitializeComponent();
            LoadData();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            // Do not load your data at design time.
            // if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            // {
            // 	//Load your data here and assign the result to the CollectionViewSource.
            // 	System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["Resource Key for CollectionViewSource"];
            // 	myCollectionViewSource.Source = your data
            // }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddRepart addRepart = new AddRepart(this);
            addRepart.Show();
        }

        public void LoadData()
        {
            try
            {
                string query = @"Select Repart_Id, Repart_name, Repart_desc, r.Created_date , u.Username as Created_By, d.Depot_Name as Depot_Id
                                from Reparts r 
                                inner join Users u on u.User_ID = r.Created_By 
                                inner join Depots d on d.Depot_ID = r.depot_id";
                using (SqlConnection con = new SqlConnection(Utility.ConnStr))
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable("Reparts");
                    sda.Fill(dt);
                    repartsDataGrid.ItemsSource = dt.DefaultView;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
