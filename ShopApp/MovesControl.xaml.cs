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
    /// Interaction logic for MovesControl.xaml
    /// </summary>
    public partial class MovesControl : UserControl
    {
        public MovesControl()
        {
            InitializeComponent();
        }
        DataSet ds;
        SqlDataAdapter adapter;
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            // Do not load your data at design time.
            // if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            // {
            // 	//Load your data here and assign the result to the CollectionViewSource.
            // 	System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["Resource Key for CollectionViewSource"];
            // 	myCollectionViewSource.Source = your data
            // }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddTransfer transfer = new AddTransfer();
            transfer.Show();
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadData();
        }
         
        public void LoadData()
        {
            this.ds = new DataSet();
            try
            {
                string data = dtpxdateSelected.SelectedDate.ToString();
                string query = string.Format(@"select 
                                            t.ID,
                                            t.DateCreated,
                                            r.Repart_Name as RepartTo,
                                            r2.Repart_Name as RepartFrom,
                                            d.Depot_Name as MagFrom,
                                            d1.Depot_Name as MagTo,
                                            p.Product_Name, 
                                            t.Quantity,
                                            t.CostPerUnit,
                                            t.ProductMoveCost,
                                            t.Comments,
                                            t.ProductCost,
                                            n.Njesia_Name
                                            from Transactions t 
                                            inner join Reparts r on r.Repart_Id = t.RepIdTo
                                            inner join Reparts r2 on r2.Repart_Id = t.RepIdFrom
                                            inner join Depots d on d.Depot_ID = t.DepotIdFrom
                                            inner join Depots d1 on d1.Depot_ID = t.DepotIdTo
                                            inner join Products p on p.Product_ID = t.ProductId
                                            inner join Njesite n on n.Njesia_ID =  t.NjesiaId
                                            where cast(DateCreated as date) = cast('{0}' as date)
                                            order by t.ID desc ", data);


                using (SqlConnection con = new SqlConnection(Utility.ConnStr))
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable("Transactions");
                    sda.Fill(dt);
                    transactionsDataGrid.ItemsSource = dt.DefaultView;
                    adapter = sda;
                }
                this.ds.AcceptChanges();
            }
            catch (Exception ec)
            {

                throw ec;
            }
            

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.ds = new DataSet();
            try
            {
                string data = txtTransferId.Text;
                string query = string.Format(@"select 
                                            t.ID,
                                            t.DateCreated,
                                            r.Repart_Name as RepartTo,
                                            r2.Repart_Name as RepartFrom,
                                            d.Depot_Name as MagFrom,
                                            d1.Depot_Name as MagTo,
                                            p.Product_Name, 
                                            t.Quantity,
                                            t.CostPerUnit,
                                            t.ProductMoveCost,
                                            t.Comments,
                                            t.ProductCost,
                                            n.Njesia_Name
                                            from Transactions t 
                                            inner join Reparts r on r.Repart_Id = t.RepIdTo
                                            inner join Reparts r2 on r2.Repart_Id = t.RepIdFrom
                                            inner join Depots d on d.Depot_ID = t.DepotIdFrom
                                            inner join Depots d1 on d1.Depot_ID = t.DepotIdTo
                                            inner join Products p on p.Product_ID = t.ProductId
                                            inner join Njesite n on n.Njesia_ID =  t.NjesiaId
                                            where t.id={0}
                                            ", data);


                using (SqlConnection con = new SqlConnection(Utility.ConnStr))
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable("Transactions");
                    sda.Fill(dt);
                    adapter = sda;
                    transactionsDataGrid.ItemsSource = dt.DefaultView;
                }
                this.ds.AcceptChanges();
            }
            catch (Exception ec)
            {

                throw ec;
            }

        }
       
    }
}
