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
    /// Interaction logic for ArticlesControl.xaml
    /// </summary>
    public partial class ArticlesControl : UserControl
    {
        public ArticlesControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            // Do not load your data at design time.
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                //Load your data here and assign the result to the CollectionViewSource.
                ShopApp.InventoryDataSet inventoryDataSet = ((ShopApp.InventoryDataSet)(this.FindResource("inventoryDataSet")));
                // Load data into the table Reparts. You can modify this code as needed.
                ShopApp.InventoryDataSetTableAdapters.GroupsTableAdapter inventoryDataSetRepartsTableAdapter = new ShopApp.InventoryDataSetTableAdapters.GroupsTableAdapter();
                inventoryDataSetRepartsTableAdapter.Fill(inventoryDataSet.Groups);
                System.Windows.Data.CollectionViewSource repartsViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("groupsViewSource")));
                repartsViewSource.View.MoveCurrentToFirst();
                ShopApp.InventoryDataSetTableAdapters.SubGroupsTableAdapter inventoryDataSetRepartsTableAdapters = new ShopApp.InventoryDataSetTableAdapters.SubGroupsTableAdapter();
                inventoryDataSetRepartsTableAdapters.Fill(inventoryDataSet.SubGroups);
                System.Windows.Data.CollectionViewSource repartsViewSources = ((System.Windows.Data.CollectionViewSource)(this.FindResource("subGroupsViewSource")));
                repartsViewSources.View.MoveCurrentToFirst();
                LoadData();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddArticles articles = new AddArticles(this);
            articles.Show();
        }


        public void LoadData( string subGroupId="0")
        {
            if (cmbxSubGroupId.SelectedValue == null) return; 
            subGroupId = subGroupId == "0" ? cmbxSubGroupId.SelectedValue.ToString() : subGroupId;
            string groupId = cmbxGroupId.SelectedValue.ToString();

            string query = string.Format(@"select Product_ID,Product_Name,Product_Desc,n.Njesia_Name as Njesia ,Price	 from Products p
            inner join Njesite n on n.Njesia_ID = p.Njesia_ID where group_Id = {0} and sub_group_id = {1}", groupId,subGroupId);

            using (SqlConnection con = new SqlConnection(Utility.ConnStr))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Products");
                sda.Fill(dt);
                productsDataGrid.ItemsSource = dt.DefaultView;
            }
        
        }

        private void CmbxSubGroupId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadData();
        }

        private void CmbxGroupId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string groupId = cmbxGroupId.SelectedValue.ToString();

            string query = string.Format(@"select SubGroup_Id,SubGroup_Name from SubGroups  where group_Id = {0} ", groupId);

            using (SqlConnection con = new SqlConnection(Utility.ConnStr))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("SubGroups");
                sda.Fill(dt);
                cmbxSubGroupId.ItemsSource = dt.DefaultView;
            }
        }
    }
}
