using Dapper;
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
using System.Windows.Shapes;

namespace ShopApp
{
    /// <summary>
    /// Interaction logic for AddRepart.xaml
    /// </summary>
    public partial class AddRepart : Window
    {
        public AddRepart()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            ShopApp.InventoryDataSet inventoryDataSet = ((ShopApp.InventoryDataSet)(this.FindResource("inventoryDataSet")));
            // Load data into the table Depots. You can modify this code as needed.
            ShopApp.InventoryDataSetTableAdapters.DepotsTableAdapter inventoryDataSetDepotsTableAdapter = new ShopApp.InventoryDataSetTableAdapters.DepotsTableAdapter();
            inventoryDataSetDepotsTableAdapter.Fill(inventoryDataSet.Depots);
            System.Windows.Data.CollectionViewSource depotsViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("depotsViewSource")));
            depotsViewSource.View.MoveCurrentToFirst();
        }

        private void BntSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string repartName = txtRepartName.Text;
                string repartDesc = txtRepartDesc.Text;
                string depotId = cmbxDepotId.SelectedValue.ToString();
                string userId = Utility.UserId.ToString();
                IDbConnection dbcnn = new SqlConnection(Utility.ConnStr);
                string query = String.Format("Insert into reparts(Repart_Name,Repart_Desc,Depot_Id,Created_by) values('{0}','{1}','{2}',{3})", repartName, repartDesc, depotId, userId);
                int count = dbcnn.Execute(query, commandType: CommandType.Text);
                if (count > 0)
                    MessageBox.Show("Repart was succesfully Added", "Success!");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
