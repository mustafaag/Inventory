using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using Dapper;

namespace ShopApp
{
    /// <summary>
    /// Interaction logic for AddGroup.xaml
    /// </summary>
    public partial class AddGroup : Window
    {
        GroupsControl _Group;
        public AddGroup( GroupsControl g)
        {
            InitializeComponent();
            _Group = g;

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void BntSubmit_Click(object sender, RoutedEventArgs e)
        {
            string groupName = txtGroupName.Text;
            string groupDesc = txtGroupDesc.Text;
            string repartId = cmbxRepartId.SelectedValue.ToString();
            try
            {
                IDbConnection dbcnn = new SqlConnection(Utility.ConnStr);
                string query = "Insert into Groups(Group_Name, Group_desc,Created_By,repart_Id) values( '" + groupName + "','" + groupDesc + "',"+Utility.UserId.ToString()+","+ repartId + ")";
                int count = dbcnn.Execute(query, commandType: CommandType.Text);
                if (count > 0)
                {
                    MessageBox.Show("Group was succesfully Added", "Success!");
                    _Group.LoadData();
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
        }
    }
}
