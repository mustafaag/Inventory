using Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace ShopApp
{
    /// <summary>
    /// Interaction logic for AddDepot.xaml
    /// </summary>
    public partial class AddDepot : Window
    {
        DepotControl _Depot;
        public AddDepot(DepotControl d)
        {
            InitializeComponent();
            _Depot = d;
        }

        private void BntSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string depotName = txtDepotName.Text;
                string depotDesc = txtDepotDesc.Text;
                string depotAddr = txtDepotAddress.Text;
                string userId = Utility.UserId.ToString();
                IDbConnection dbcnn = new SqlConnection(Utility.ConnStr);
                string query = String.Format("Insert into Depots(Depot_Name,Depot_desc,Address,Created_by) values('{0}','{1}','{2}',{3})", depotName, depotDesc, depotAddr, userId);
                int count = dbcnn.Execute(query, commandType: CommandType.Text);
                if (count > 0)
                {
                    MessageBox.Show("Depot was succesfully Added", "Success!");
                    _Depot.LoadData();
                }
                    
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
