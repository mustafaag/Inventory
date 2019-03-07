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
    /// Interaction logic for SubGroupControl.xaml
    /// </summary>
    public partial class SubGroupControl : UserControl
    {
        public SubGroupControl()
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
                ShopApp.InventoryDataSetTableAdapters.GroupsTableAdapter inventoryDataSetRepartsTableAdapter = new ShopApp.InventoryDataSetTableAdapters.GroupsTableAdapter();
                inventoryDataSetRepartsTableAdapter.Fill(inventoryDataSet.Groups);
                System.Windows.Data.CollectionViewSource repartsViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("groupsViewSource")));
                repartsViewSource.View.MoveCurrentToFirst();
                LoadData();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddSubGroup subGroup = new AddSubGroup(this);
            subGroup.Show();
        }

        private void CmbxGroupId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadData();
        }

        public void LoadData(string groupId = "0")
        {

             groupId = groupId=="0"? cmbxGroupId.SelectedValue.ToString() : groupId;
            string query = String.Format(@"select SubGroup_ID, SubGroup_Name,SubGroup_Desc,s.Date_Created, u.Username as Created_By  from SubGroups s
            inner join users u on u.User_ID = s.Created_By  where s.Group_ID = {0}", groupId);


            using (SqlConnection con = new SqlConnection(Utility.ConnStr))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("SubGroups");
                sda.Fill(dt);
                subGroupsDataGrid.ItemsSource = dt.DefaultView;
            }
        }
    }
}
