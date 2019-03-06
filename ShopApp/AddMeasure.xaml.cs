using Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace ShopApp
{
    /// <summary>
    /// Interaction logic for AddMeasure.xaml
    /// </summary>
    public partial class AddMeasure : Window
    {
        public Measure _Measure;
        public AddMeasure( Measure measure)
        {

            InitializeComponent();
            _Measure = measure;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ///
          
        }
        private void BntSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string measureName = txtMeasureName.Text;
                string repartDesc = txtMeasureDesc.Text;
              
                string userId = Utility.UserId.ToString();
                IDbConnection dbcnn = new SqlConnection(Utility.ConnStr);
                string query = String.Format("Insert into Njesite (Njesia_Name,Njesia_Desc,Created_by) values('{0}','{1}','{2}')", measureName, repartDesc,  userId);
                int count = dbcnn.Execute(query, commandType: CommandType.Text);
                if (count > 0)
                {
                    MessageBox.Show("Measure was succesfully Added", "Success!");
                    Measure measure = new Measure();
                    _Measure.LoadData();

                }
                    
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
