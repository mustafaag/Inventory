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
    /// Interaction logic for WorkFlowControl.xaml
    /// </summary>
    public partial class WorkFlowControl : UserControl
    {
        public WorkFlowControl()
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
                ShopApp.InventoryDataSetTableAdapters.RepartsTableAdapter inventoryDataSetRepartsTableAdapter = new ShopApp.InventoryDataSetTableAdapters.RepartsTableAdapter();
                inventoryDataSetRepartsTableAdapter.Fill(inventoryDataSet.Reparts);
                System.Windows.Data.CollectionViewSource repartsViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("repartsViewSource")));
                repartsViewSource.View.MoveCurrentToFirst();
                LoadData();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddWorkFlows workFlows = new AddWorkFlows();
            workFlows.Show();
        }

        public void LoadData()
        {
            try
            {
                string repartid = cmbxRepartId.SelectedValue.ToString();
                string query = string.Format(@"select w.id,  p.Product_Name, s.Status_Name,Comment
                            from workflows w
                            inner join Products p on p.Product_ID = w.Article_Id
                            inner  join Status s on s.Status_Id = w.Status_Id
                            where w.Repart_Id = {0}", repartid) ;
                using (SqlConnection con = new SqlConnection(Utility.ConnStr))
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable("Workflows");
                    sda.Fill(dt);
                    workFlowsDataGrid.ItemsSource = dt.DefaultView;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void CmbxRepartId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadData();
        }
    }
}
