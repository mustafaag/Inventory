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
    /// Interaction logic for AddWorkFlows.xaml
    /// </summary>
    public partial class AddWorkFlows : Window
    {
        public AddWorkFlows()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            ShopApp.InventoryDataSet inventoryDataSet = ((ShopApp.InventoryDataSet)(this.FindResource("inventoryDataSet")));
            // Load data into the table Products. You can modify this code as needed.
            ShopApp.InventoryDataSetTableAdapters.ProductsTableAdapter inventoryDataSetProductsTableAdapter = new ShopApp.InventoryDataSetTableAdapters.ProductsTableAdapter();
            inventoryDataSetProductsTableAdapter.Fill(inventoryDataSet.Products);
            System.Windows.Data.CollectionViewSource productsViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("productsViewSource")));
            productsViewSource.View.MoveCurrentToFirst();
            // Load data into the table Status. You can modify this code as needed.
            ShopApp.InventoryDataSetTableAdapters.StatusTableAdapter inventoryDataSetStatusTableAdapter = new ShopApp.InventoryDataSetTableAdapters.StatusTableAdapter();
            inventoryDataSetStatusTableAdapter.Fill(inventoryDataSet.Status);
            System.Windows.Data.CollectionViewSource statusViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("statusViewSource")));
            statusViewSource.View.MoveCurrentToFirst();
            // Load data into the table Reparts. You can modify this code as needed.
            ShopApp.InventoryDataSetTableAdapters.RepartsTableAdapter inventoryDataSetRepartsTableAdapter = new ShopApp.InventoryDataSetTableAdapters.RepartsTableAdapter();
            inventoryDataSetRepartsTableAdapter.Fill(inventoryDataSet.Reparts);
            System.Windows.Data.CollectionViewSource repartsViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("repartsViewSource")));
            repartsViewSource.View.MoveCurrentToFirst();
        }

        private void BntSubmit_Click(object sender, RoutedEventArgs e)
        {
            string productId = cmbxProductId.SelectedValue.ToString();
            string repartId = cmbxRepartId.SelectedValue.ToString();
            string statusId = cmbxStatusId.SelectedValue.ToString();
            string comments = txtComments.Text;
            try
            {
                string query = string.Format(@"Insert into WorkFlows(Article_Id, Repart_Id,Status_Id, comment) values({0},{1},{2},'{3}')"
                ,productId, repartId, statusId, comments);
                IDbConnection dbcnn = new SqlConnection(Utility.ConnStr);
                int count = dbcnn.Execute(query, commandType: CommandType.Text);
                if (count > 0)
                {
                    MessageBox.Show("Workflow was succesfully Added", "Success!");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
