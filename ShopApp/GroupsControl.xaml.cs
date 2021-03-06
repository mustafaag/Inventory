﻿using System;
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
    /// Interaction logic for GroupsControl.xaml
    /// </summary>
    public partial class GroupsControl : UserControl
    {
        public GroupsControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            LoadData();
        }

        public void LoadData()
        {
            try
            {
                string query = @"select Group_ID,Group_Name,Group_desc, g.Date_Created, u.Username as Created_By, r.Repart_Name as Reparti from groups g
                    inner join Users u on u.User_ID = g.Created_By
                    inner join Reparts r on r.Repart_Id = g.Created_By";
                using (SqlConnection con = new SqlConnection(Utility.ConnStr))
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable("Groups");
                    sda.Fill(dt);
                    groupsDataGrid.ItemsSource = dt.DefaultView;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddGroup addGroup = new AddGroup(this);
            addGroup.Show();
        }
    }
}
