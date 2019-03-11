using Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace ShopApp
{
    /// <summary>
    /// Interaction logic for AddTransfer.xaml
    /// </summary>
    public partial class AddTransfer : Window
    {
        public AddTransfer()
        {
            InitializeComponent();
        }

        private IDbConnection dbcnn = new SqlConnection(Utility.ConnStr);

        private void BntSubmit_Click(object sender, RoutedEventArgs e)
        {

            DateTime dateCreated = dateTransfer.SelectedDate.Value;
            int repIdFrom = Convert.ToInt32(cmbRepartFrom.SelectedValue);
            int repIdTo = Convert.ToInt32 (cmbRepartTo.SelectedValue);
            int magFrom = Convert.ToInt32( cmbDepotFrom.SelectedValue);
            int magTo = Convert.ToInt32(cmbMagTo.SelectedValue);
            int prodId = Convert.ToInt32(cmbProduct.SelectedValue);
            int quantity = Convert.ToInt32(txtQuantity.Text);
            string comment = txtComments.Text;
            string sql = "SP_InsertIntoMoves";
            
            try
             {
                bool DoneOrNot = false;
                var p = new DynamicParameters();
                p.Add("@DateCreated", dateCreated);
                p.Add("@repIdFrom", repIdFrom);
                p.Add("@repIdTo", repIdTo);
                p.Add("@magIdFrom", magFrom);
                p.Add("@magIdTo", magTo);
                p.Add("@prodId", prodId);
                p.Add("@Quantity", quantity);
                p.Add("@comments", comment);
                var count = dbcnn.Execute(sql, p, commandType: CommandType.StoredProcedure);
                DoneOrNot = true;
                if(count > 0)
                {
                    MessageBox.Show("Product was succesfully Transfered ", "Success!");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            ShopApp.InventoryDataSet inventoryDataSet = ((ShopApp.InventoryDataSet)(this.FindResource("inventoryDataSet")));
            // Load data into the table Reparts. You can modify this code as needed.
            ShopApp.InventoryDataSetTableAdapters.RepartsTableAdapter inventoryDataSetRepartsTableAdapter = new ShopApp.InventoryDataSetTableAdapters.RepartsTableAdapter();
            inventoryDataSetRepartsTableAdapter.Fill(inventoryDataSet.Reparts);
            System.Windows.Data.CollectionViewSource repartsViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("repartsViewSource")));
            repartsViewSource.View.MoveCurrentToFirst();

            // Load data into the table Reparts. You can modify this code as needed.
            ShopApp.InventoryDataSetTableAdapters.RepartsTableAdapter inventoryDataSetRepartsTableAdapters = new ShopApp.InventoryDataSetTableAdapters.RepartsTableAdapter();
            inventoryDataSetRepartsTableAdapter.Fill(inventoryDataSet.Reparts);
            System.Windows.Data.CollectionViewSource repartsViewSources = ((System.Windows.Data.CollectionViewSource)(this.FindResource("repartsViewSource1")));
            repartsViewSource.View.MoveCurrentToFirst();
            // Load data into the table Depots. You can modify this code as needed.
            ShopApp.InventoryDataSetTableAdapters.DepotsTableAdapter inventoryDataSetDepotsTableAdapter = new ShopApp.InventoryDataSetTableAdapters.DepotsTableAdapter();
            inventoryDataSetDepotsTableAdapter.Fill(inventoryDataSet.Depots);
            System.Windows.Data.CollectionViewSource depotsViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("depotsViewSource")));
            depotsViewSource.View.MoveCurrentToFirst();

            // Load data into the table Depots. You can modify this code as needed.
            ShopApp.InventoryDataSetTableAdapters.DepotsTableAdapter inventoryDataSetDepotsTableAdapters = new ShopApp.InventoryDataSetTableAdapters.DepotsTableAdapter();
            inventoryDataSetDepotsTableAdapter.Fill(inventoryDataSet.Depots);
            System.Windows.Data.CollectionViewSource depotsViewSources = ((System.Windows.Data.CollectionViewSource)(this.FindResource("depotsViewSource1")));
            depotsViewSource.View.MoveCurrentToFirst();
            // Load data into the table Products. You can modify this code as needed.
            ShopApp.InventoryDataSetTableAdapters.ProductsTableAdapter inventoryDataSetProductsTableAdapter = new ShopApp.InventoryDataSetTableAdapters.ProductsTableAdapter();
            inventoryDataSetProductsTableAdapter.Fill(inventoryDataSet.Products);
            System.Windows.Data.CollectionViewSource productsViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("productsViewSource")));
            productsViewSource.View.MoveCurrentToFirst();
        }
    }   
}
