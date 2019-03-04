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
        public AddGroup()
        {
            InitializeComponent();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void BntSubmit_Click(object sender, RoutedEventArgs e)
        {
            string groupName = txtGroupName.Text;
            string groupDesc = txtGroupDesc.Text;
            try
            {
                IDbConnection dbcnn = new SqlConnection(Utility.ConnStr);
                string query = "Insert into Groups(Group_Name, Group_desc,Created_By) values( '" + groupName + "','" + groupDesc + "',"+Utility.UserId.ToString()+")";
                int count = dbcnn.Execute(query, commandType: CommandType.Text);
                if(count>0)
                    MessageBox.Show("Group was succesfully Added", "Success!");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
