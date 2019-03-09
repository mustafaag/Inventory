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
    /// Interaction logic for AddArticles.xaml
    /// </summary>
    public partial class AddArticles : Window
    {
        ArticlesControl _Articles = new ArticlesControl();
        public AddArticles(ArticlesControl a)
        {
            InitializeComponent();
            _Articles = a;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            ShopApp.InventoryDataSet inventoryDataSet = ((ShopApp.InventoryDataSet)(this.FindResource("inventoryDataSet")));
            // Load data into the table Njesite. You can modify this code as needed.
            ShopApp.InventoryDataSetTableAdapters.NjesiteTableAdapter inventoryDataSetNjesiteTableAdapter = new ShopApp.InventoryDataSetTableAdapters.NjesiteTableAdapter();
            inventoryDataSetNjesiteTableAdapter.Fill(inventoryDataSet.Njesite);
            System.Windows.Data.CollectionViewSource njesiteViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("njesiteViewSource")));
            njesiteViewSource.View.MoveCurrentToFirst();
            // Load data into the table Groups. You can modify this code as needed.
            ShopApp.InventoryDataSetTableAdapters.GroupsTableAdapter inventoryDataSetGroupsTableAdapter = new ShopApp.InventoryDataSetTableAdapters.GroupsTableAdapter();
            inventoryDataSetGroupsTableAdapter.Fill(inventoryDataSet.Groups);
            System.Windows.Data.CollectionViewSource groupsViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("groupsViewSource")));
            groupsViewSource.View.MoveCurrentToFirst();
            // Load data into the table SubGroups. You can modify this code as needed.
            ShopApp.InventoryDataSetTableAdapters.SubGroupsTableAdapter inventoryDataSetSubGroupsTableAdapter = new ShopApp.InventoryDataSetTableAdapters.SubGroupsTableAdapter();
            inventoryDataSetSubGroupsTableAdapter.Fill(inventoryDataSet.SubGroups);
            System.Windows.Data.CollectionViewSource subGroupsViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("subGroupsViewSource")));
            subGroupsViewSource.View.MoveCurrentToFirst();
        }

        private void BntSubmit_Click(object sender, RoutedEventArgs e)
        {
            string articleName = txtArticleName.Text;
            string articleDesc = txtArticleDesc.Text;
            string cost = txtCost.Text;
            string groupId = cmbxGroupId.SelectedValue.ToString();
            string subGroupId = cmbxSubGroupId.SelectedValue.ToString();
            string measureId = cmbxNjesiId.SelectedValue.ToString();

            string userId = Utility.UserId.ToString();
            try
            {
                IDbConnection dbcnn = new SqlConnection(Utility.ConnStr);
                string query = String.Format(@"Insert into Products ( Product_Name,Product_Desc,Created_By,Sub_Group_ID,Price ,Njesia_ID,Group_Id) values('{0}' ,'{1}',{2},{3},{4},{5},{6})"
                ,articleName,articleDesc,userId,subGroupId,cost,measureId,groupId);

                int count = dbcnn.Execute(query, commandType: CommandType.Text);
                if (count > 0)
                {
                    MessageBox.Show("Article was succesfully Added", "Success!");
                    _Articles.LoadData(subGroupId);
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
