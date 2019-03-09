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
    /// Interaction logic for AddSubGroup.xaml
    /// </summary>
    public partial class AddSubGroup : Window
    {
        SubGroupControl _SubGroup = new SubGroupControl();
        public AddSubGroup(SubGroupControl sg)
        {
            InitializeComponent();
            _SubGroup = sg;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            ShopApp.InventoryDataSet inventoryDataSet = ((ShopApp.InventoryDataSet)(this.FindResource("inventoryDataSet")));
            // Load data into the table Groups. You can modify this code as needed.
            ShopApp.InventoryDataSetTableAdapters.GroupsTableAdapter inventoryDataSetGroupsTableAdapter = new ShopApp.InventoryDataSetTableAdapters.GroupsTableAdapter();
            inventoryDataSetGroupsTableAdapter.Fill(inventoryDataSet.Groups);
            System.Windows.Data.CollectionViewSource groupsViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("groupsViewSource")));
            groupsViewSource.View.MoveCurrentToFirst();
        }

        private void BntSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string subName = txtSGroupName.Text;
                string subDesc = txtSGroupDesc.Text;
                string groupId = cmbxGroupId.SelectedValue.ToString();
                string userId = Utility.UserId.ToString();

                IDbConnection dbcnn = new SqlConnection(Utility.ConnStr);
                string query = String.Format("Insert into SubGroups(SubGroup_Name,SubGroup_Desc,Group_ID,Created_by) values('{0}','{1}','{2}',{3})", subName, subDesc, groupId, userId);
                int count = dbcnn.Execute(query, commandType: CommandType.Text);
                if (count > 0)
                {
                    MessageBox.Show("Sub Group was succesfully Added", "Success!");
                    _SubGroup.LoadData(groupId);
                }
            }
            catch (Exception)
            {

                throw;
            }
           

        }
    }
}
