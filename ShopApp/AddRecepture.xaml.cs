using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using Dapper;

namespace ShopApp
{
    /// <summary>
    /// Interaction logic for AddRecepture.xaml
    /// </summary>
    public partial class AddRecepture : Window
    {
        public AddRecepture()
        {
            InitializeComponent();
        }
        private IDbConnection dbcnn = new SqlConnection(Utility.ConnStr);
        private void BntSubmit_Click(object sender, RoutedEventArgs e)
        {
            int prodMasterId = Convert.ToInt32(cmbxProductId.SelectedValue);
            int prodAddId = Convert.ToInt32(cmbxAdditionalProductId.SelectedValue);
            string comments = txtComments.Text;
            int measureID = Convert.ToInt32(cmbxMeasureId.SelectedValue);
            int quantity = Convert.ToInt32(txtQuantity.Text);
            try
            {
                string sql = "SP_SaveToReceptura";
                var p = new DynamicParameters();
                p.Add("@masterId", prodMasterId);
                p.Add("@additional", prodAddId);
                p.Add("@comments", comments);
                p.Add("@measureid", measureID);
                p.Add("@quantity", quantity);

                var count = dbcnn.Execute(sql, p, commandType: CommandType.StoredProcedure);
                if (count > 0)
                {
                    MessageBox.Show("Recepture was succesfully added ", "Success!");
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
            // Load data into the table Products. You can modify this code as needed.
            ShopApp.InventoryDataSetTableAdapters.ProductsTableAdapter inventoryDataSetProductsTableAdapter = new ShopApp.InventoryDataSetTableAdapters.ProductsTableAdapter();
            inventoryDataSetProductsTableAdapter.Fill(inventoryDataSet.Products);
            System.Windows.Data.CollectionViewSource productsViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("productsViewSource")));
            productsViewSource.View.MoveCurrentToFirst();
            ShopApp.InventoryDataSetTableAdapters.ProductsTableAdapter inventoryDataSetProductsTableAdapter1 = new ShopApp.InventoryDataSetTableAdapters.ProductsTableAdapter();
            inventoryDataSetProductsTableAdapter.Fill(inventoryDataSet.Products);
            System.Windows.Data.CollectionViewSource productsViewSource1 = ((System.Windows.Data.CollectionViewSource)(this.FindResource("productsViewSource1")));
            productsViewSource.View.MoveCurrentToFirst();
            // Load data into the table Njesite. You can modify this code as needed.
            ShopApp.InventoryDataSetTableAdapters.NjesiteTableAdapter inventoryDataSetNjesiteTableAdapter = new ShopApp.InventoryDataSetTableAdapters.NjesiteTableAdapter();
            inventoryDataSetNjesiteTableAdapter.Fill(inventoryDataSet.Njesite);
            System.Windows.Data.CollectionViewSource njesiteViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("njesiteViewSource")));
            njesiteViewSource.View.MoveCurrentToFirst();
        }
    }
}
