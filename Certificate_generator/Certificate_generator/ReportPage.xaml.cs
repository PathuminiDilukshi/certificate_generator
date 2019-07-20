using System;
using System.Collections.Generic;
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
using System.Data;
using System.Data.SqlClient;
using Microsoft.Reporting.WinForms;



namespace Certificate_generator
{
    /// <summary>
    /// Interaction logic for ReportPage.xaml
    /// </summary>
    public partial class ReportPage : Page
    {
        private static string ConString = @"Data Source=PC-13;Initial Catalog=Certificate_Generating_System;User ID=sa;Password=abc123;";




        public ReportPage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                reportViewerDemo1.Reset();
                DataTable dt = GetData();
                ReportDataSource ds = new ReportDataSource("Certificate_Generating_SystemDataSet", dt);
                reportViewerDemo1.LocalReport.DataSources.Add(ds);

                //reportViewerDemo.LocalReport.ReportEmbeddedResource = "Certificate_generator.Report1.rdlc";
                //reportViewerDemo.RefreshReport();

                var reportStream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Report1.rdlc");
                reportViewerDemo1.LocalReport.LoadReportDefinition(reportStream);
                reportViewerDemo1.RefreshReport();


                //reportViewerDemo.ProcessingMode = ProcessingMode.Local;
                //reportViewerDemo.LocalReport.ReportPath = Server.MapPath("~/Report1.rdlc");
                //reportViewerDemo.LocalReport.DataSources.Clear();
                //ReportDataSource datasource = new ReportDataSource("DataSet1", ds);
                //reportViewerDemo.LocalReport.Refresh();
                //reportViewerDemo.LocalReport.DataSources.Add(datasource);
                //reportViewerDemo.ShowPrintButton = true;





            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private DataTable GetData()
        {
            string query ="SELECT Cus_no,Customer_Name,Addressline1,Addressline2,Addressline3,payment_code,Contact_no FROM Cusomer_details where Cus_no = '"+customer_number.Text.ToString() +"'" ;
            
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConString))
            {
                con.Open();
                SqlCommand com = new SqlCommand(query, con);
                dt.Load(com.ExecuteReader());
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(dt);

            }
            return dt;

        }
    }
}
