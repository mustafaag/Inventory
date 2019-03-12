using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace ShopApp
{
    /// <summary>
    /// Interaction logic for RecepturaControl.xaml
    /// </summary>
    public partial class RecepturaControl : UserControl
    {
        public RecepturaControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            // Do not load your data at design time.
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                ShopApp.InventoryDataSet inventoryDataSet = ((ShopApp.InventoryDataSet)(this.FindResource("inventoryDataSet")));
                // Load data into the table Reparts. You can modify this code as needed.
                ShopApp.InventoryDataSetTableAdapters.RepartsTableAdapter inventoryDataSetRepartsTableAdapter = new ShopApp.InventoryDataSetTableAdapters.RepartsTableAdapter();
                inventoryDataSetRepartsTableAdapter.Fill(inventoryDataSet.Reparts);
                System.Windows.Data.CollectionViewSource repartsViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("repartsViewSource")));
                repartsViewSource.View.MoveCurrentToFirst();
            }
        }

        private void CmbxRepartId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbxRepartId.SelectedValue == null) return;
            string repartid = cmbxRepartId.SelectedValue.ToString();
            string query = string.Format(@"select SubGroup_Id,SubGroup_Name from SubGroups  where group_Id in (
            SELECT Group_ID FROM Groups where Repart_ID={0}) ", repartid);

            using (SqlConnection con = new SqlConnection(Utility.ConnStr))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("SubGroups");
                sda.Fill(dt);
                cmbSubGroupId.ItemsSource = dt.DefaultView;
            }
        }

        private void CmbSubGroupId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbSubGroupId.SelectedIndex ==0) return;
            string subgroupId = cmbSubGroupId.SelectedValue.ToString();
            string query = string.Format(@"Select Product_ID,Product_Name from Products where Sub_Group_ID={0}", subgroupId);
            using (SqlConnection con = new SqlConnection(Utility.ConnStr))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Products");
                sda.Fill(dt);
                cmbxProductMaster.ItemsSource = dt.DefaultView;
            }
        }
    }
}
