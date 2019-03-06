using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace ShopApp
{
    /// <summary>
    /// Interaction logic for DepotControl.xaml
    /// </summary>
    public partial class DepotControl : UserControl
    {
        IDbConnection dbcnn = new SqlConnection(Utility.ConnStr);
        public DepotControl()
        {
            InitializeComponent();

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            // Do not load your data at design time.
            LoadData();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddDepot addDepot = new AddDepot(this);
            addDepot.Show();
        }


        public void LoadData()
        {
            try
            {
                string query = @"select Depot_ID, Depot_Name,Depot_Desc,Address,Created_Date,u.Username as Created_By from Depots d 
                inner join Users u on u.User_ID = d.Created_By";
                using (SqlConnection con = new SqlConnection(Utility.ConnStr))
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable("Depots");
                    sda.Fill(dt);
                    depotsDataGrid.ItemsSource = dt.DefaultView;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
