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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Utility.ConnStr = ConfigSettings.ReadSetting("DB");
        }

        private void bntSubmit_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = new DataTable();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@uname", this.txtUsername.Text);
            param[1] = new SqlParameter("@upass", this.txtPassword.Password);

            MsSqlDataAccess msAcc = new MsSqlDataAccess(Utility.ConnStr);
            dt = MsSqlDataAccess.GetDataTable("select User_Id,username,First_Name,Last_Name,Password,User_Type, Deleted, Active, Date_Created, Created_By from users where Username=@uname and Password=@upass", CommandType.Text, param);

            if (dt.Rows.Count > 0)
            {
                Utility.UserId = dt.Rows[0][0].ToString();
                Utility.UserName = dt.Rows[0][1].ToString();
                Utility.UserIdAccess = Convert.ToBoolean(dt.Rows[0][7].ToString());

            }
            else
            {
                Utility.UserIdAccess = false;
                MessageBox.Show("Wrong Password", "Info");
            }

            if (Utility.UserIdAccess)
            {
                MWindow mWindow = new MWindow();
                mWindow.Show();
                this.Close();
            }

        }
    }
}
