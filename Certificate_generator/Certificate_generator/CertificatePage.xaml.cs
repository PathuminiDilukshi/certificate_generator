using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Printing;
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
using System.IO;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using System.Windows.Documents.Serialization;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using System.Globalization;
using System.Threading;

namespace Certificate_generator
{
    /// <summary>
    /// Interaction logic for CertificatePage.xaml
    /// </summary>
    public partial class CertificatePage : Page
    {
        public SqlConnection con = null;
        public SqlCommand com = null;
        public SqlDataAdapter adapter = null;
        public DataSet ds = null;
        public int rate = 0;
        public static string new_val = "0";
        public static string cer_code = "0";
        public static string Scode;
        public static string cus_no;
        public static int paycode;
        public static string ToDate;
        public static string FromDate;
        public static string DeductionDate;
        public static string RemittanceDate;
        public static string AuthorizedDate;
        public static string created_date;



        private static string ConString = @"Data Source=PC-13;Initial Catalog=Certificate_Generating_System;User ID=sa;Password=abc123;";

        public CertificatePage()
        {
            InitializeComponent();
            BindComboBox(Certificate_type);

            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd.MM.yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

        }

        #region dateTimepicker

        private void DatePicker_OnMouseEnter(object sender, MouseEventArgs e)
        {
            ChangeCalendarButtonVisibility(Visibility.Visible);
        }

        private void DatePicker_OnMouseLeave(object sender, MouseEventArgs e)
        {
            ChangeCalendarButtonVisibility(Visibility.Collapsed);
        }

        private void DatePicker_OnLoaded(object sender, RoutedEventArgs e)
        {
            //We use loaded event to hide button in initial state
            ChangeCalendarButtonVisibility(Visibility.Collapsed);
        }

        private Button GetCalendarButton()
        {
            var template = datePicker.Template;
            var button = (Button)template.FindName("PART_Button", datePicker);
            return button;
        }

        private void ChangeCalendarButtonVisibility(Visibility visibility)
        {
            var button = GetCalendarButton();
            if (button != null)
            {
                button.Visibility = visibility;
            }
        }
        #endregion


        private void datePicker2_MouseEnter(object sender, MouseEventArgs e)
        {
            ChangeCalendarButtonVisibility2(Visibility.Visible);
        }

        private void datePicker2_MouseLeave(object sender, MouseEventArgs e)
        {
            ChangeCalendarButtonVisibility2(Visibility.Collapsed);
        }

        private void datePicker2_Loaded(object sender, RoutedEventArgs e)
        {
            ChangeCalendarButtonVisibility2(Visibility.Collapsed);
        }

        private Button GetCalendarButton2()
        {
            var template = datePicker2.Template;
            var button = (Button)template.FindName("PART_Button", datePicker2);
            return button;
        }
        private void ChangeCalendarButtonVisibility2(Visibility visibility)
        {
            var button = GetCalendarButton2();
            if (button != null)
            {
                button.Visibility = visibility;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Print_WPF_Preview(Grid_Main);
      
        }

        public static void Print_WPF_Preview(FrameworkElement wpf_Element)
        {
            string sPrintFilename = "print_preview.xps";
            if (File.Exists(sPrintFilename) == true)
                File.Delete(sPrintFilename);


            // - <create xps document> -

            XpsDocument doc = new XpsDocument(sPrintFilename, FileAccess.ReadWrite);
            XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(doc);

            SerializerWriterCollator preview_Document = writer.CreateVisualsCollator();
            preview_Document.BeginBatchWrite();
            preview_Document.Write(wpf_Element);  // * this or wpf xaml control
            preview_Document.EndBatchWrite();

            // - </ create xps document> -
            FixedDocumentSequence preview = doc.GetFixedDocumentSequence();

            // - </Open> -

            // - </Open preview window> -

            var window = new Window();
            window.Content = new DocumentViewer { Document = preview };
            window.ShowDialog();
            doc.Close();


        }

        private void datepicker3_MouseEnter(object sender, MouseEventArgs e)
        {
            ChangeCalendarButtonVisibility3(Visibility.Visible);
        }

        private void datepicker3_MouseLeave(object sender, MouseEventArgs e)
        {
            ChangeCalendarButtonVisibility3(Visibility.Collapsed);
        }

        private void datepicker3_Loaded(object sender, RoutedEventArgs e)
        {
            ChangeCalendarButtonVisibility3(Visibility.Collapsed);
        }


        private Button GetCalendarButton3()
        {
            var template = datepicker3.Template;
            var button = (Button)template.FindName("PART_Button", datepicker3);
            return button;
        }

        private void ChangeCalendarButtonVisibility3(Visibility visibility)
        {
            var button = GetCalendarButton3();
            if (button != null)
            {
                button.Visibility = visibility;
            }
        }


        private void datepicker4_MouseEnter(object sender, MouseEventArgs e)
        {
            ChangeCalendarButtonVisibility4(Visibility.Visible);
        }

        private void datepicker4_MouseLeave(object sender, MouseEventArgs e)
        {
            ChangeCalendarButtonVisibility4(Visibility.Collapsed);
        }

        private void datepicker4_Loaded(object sender, RoutedEventArgs e)
        {
            ChangeCalendarButtonVisibility4(Visibility.Collapsed);
        }

        private Button GetCalendarButton4()
        {
            var template = datepicker4.Template;
            var button = (Button)template.FindName("PART_Button", datepicker4);
            return button;
        }

        private void ChangeCalendarButtonVisibility4(Visibility visibility)
        {
            var button = GetCalendarButton4();
            if (button != null)
            {
                button.Visibility = visibility;
            }
        }
        public void BindComboBox(ComboBox comboBoxName)
        {

            try
            {
                con = new SqlConnection(ConString);
                con.Open();
                com = new SqlCommand("select Short_name from WH_agent ", con);
                com.CommandType = CommandType.Text;
                SqlDataReader dr = com.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        string sid = dr.GetString(0);
                        comboBoxName.Items.Add(sid);
                    }
                }
                con.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }



        }



        private void Certificate_type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Scode = Certificate_type.SelectedItem as string;

                con = new SqlConnection(ConString);
                con.Open();
                com = new SqlCommand("select top(1) id from Certificate_details where  Sname ='" + Scode + "' ORDER BY id DESC  ", con);
                com.CommandType = CommandType.Text;
                SqlDataReader dr = com.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        int id_val = dr.GetInt32(0);
                        new_val = (id_val + 1).ToString();
                        int no = new_val.Length;


                        if (no == 1)
                        {
                            certificate_int.Text = Scode.TrimEnd() + "/" +"000" + new_val;
                        }
                        else if (no == 2)
                        {
                            certificate_int.Text = Scode.TrimEnd() + "/" + "00" + new_val;
                        }
                        else if (no == 3)
                        {
                            certificate_int.Text = Scode.TrimEnd() + "/" + "0" + new_val;
                        }

                    }

                }
                else
                {
                    int new_code = 1;
                    cer_code = new_code.ToString();
                    certificate_int.Text = Scode + "/" + "000" + cer_code;

                }

                get_withholding_agent(Scode);

                con.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        public void get_withholding_agent(string shortID)
        {
            try
            {
                con = new SqlConnection(ConString);
                con.Open();
                com = new SqlCommand("select Full_name,AddressLine2,AddressLine1,AddressLine3  from WH_agent where Short_name ='" + shortID + "'", con);
                com.CommandType = CommandType.Text;
                SqlDataReader dr = com.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        string fullname = dr.GetString(0).Trim();
                        string Address_line1 = ((string)dr["Addressline1"]).Trim();
                        string Address_line2 = ((string)dr["AddressLine2"]).Trim();
                        string Addrss_line3 = ((string)dr["AddressLine3"]).Trim();

                        WH_name_addr.Text = fullname +' '+Address_line1 + ',' + Address_line2 + ',' + Addrss_line3;;
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void Search_WH_Click(object sender, RoutedEventArgs e)
        {
            string search_txt = WH_NameTxt.Text.ToString();
            if (search_txt.Length > 0)
            {
                string query = "SELECT c.Cus_no,c.Customer_Name,c.Addressline1,c.Addressline2,c.Addressline3 ,c.payment_code,p.Nature_of_payment,p.Rate FROM Cusomer_details c JOIN Payment_methods p ON c.payment_code=p.pay_code where Customer_Name like '%" + search_txt + "%'";
                get_Customer_details(search_txt, query);
            }

        }

        public void get_Customer_details(string search_txt, string query)
        {
            try
            {
                con = new SqlConnection(ConString);
                con.Open();
                com = new SqlCommand(query, con);
                com.CommandType = CommandType.Text;
                SqlDataReader dr = com.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cus_no = ((string)dr["Cus_no"]).Trim();
                        string cus_name = ((string)dr["Customer_Name"]).Trim();
                        string Address_line1 = ((string)dr["Addressline1"]).Trim();
                        string Address_line2 = ((string)dr["Addressline2"]).Trim();
                        string Addrss_line3 = ((string)dr["Addressline3"]).Trim();
                        string payment_method = ((string)dr["Nature_of_payment"]).Trim();
                        rate = ((Int32)dr["Rate"]);
                        paycode = ((Int32)dr["payment_code"]);

                        WHoldee_details.Text = cus_name + ' '+ Address_line1 + ',' + Address_line2 + ',' + Addrss_line3;
                        cus_id.Text = cus_no;
                        payment_type.Text = payment_method;
                        payment_rate.Text = Convert.ToString(rate) + '%';

                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void gross_amount_TextChanged(object sender, TextChangedEventArgs e)
        {
            string gross = gross_amount.Text.ToString();
            calculate(gross);

        }



        private void calculate(string gross)
        {
            string val=gross.Replace(",", "");
            decimal amount = Convert.ToDecimal(val);
            decimal tax_deduction = (amount * rate / 100);
            string a =String.Format("{0:n}", tax_deduction);
            tax_deducted.Text = a.ToString();
        }

        private void datepicker5_MouseEnter(object sender, MouseEventArgs e)
        {
            ChangeCalendarButtonVisibility5(Visibility.Visible);
        }

        private void datepicker5_MouseLeave(object sender, MouseEventArgs e)
        {
            ChangeCalendarButtonVisibility5(Visibility.Collapsed);
        }

        private void datepicker5_Loaded(object sender, RoutedEventArgs e)
        {
            ChangeCalendarButtonVisibility5(Visibility.Collapsed);
        }

        private Button GetCalendarButton5()
        {
            var template = datepicker5.Template;
            var button = (Button)template.FindName("PART_Button", datepicker5);
            return button;
        }
        private void ChangeCalendarButtonVisibility5(Visibility visibility)
        {
            var button = GetCalendarButton5();
            if (button != null)
            {
                button.Visibility = visibility;
            }
        }

    

        private void certi_create_Click(object sender, RoutedEventArgs e)
        {
            int id;
            int new1=Convert.ToInt32(new_val);
            int new2=Convert.ToInt32(cer_code);

            if (new1 != 0)
            { 
               id = new1;
            }
            if (new2 != 0)
            {
                id = new2;
            }
            string WHT_carNo = certificate_int.Text;
            decimal Gamount = Convert.ToDecimal(gross_amount.Text);
            decimal Damount = Convert.ToDecimal(tax_deducted.Text);
            created_date = DateTime.Now.ToString("dd.MM.yyyy");

            ValidationS();
            
        }

        private void ValidationS()
        {
            
        }

        public void datePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
           
            // ... Get DatePicker reference.
            var picker = sender as DatePicker;

            // ... Get nullable DateTime from SelectedDate.
            DateTime? date = picker.SelectedDate;
            if (date == null)
            {
                // ... A null object.
                Title = "No date";
            }
            else
            {
                // ... No need to display the time.
                //FromDate = datePicker.SelectedDate.Value.ToString("dd/MM/yyyy");
                FromDate = datePicker.SelectedDate.Value.ToString("dd.MM.yyyy");
            }
        
        }

        private void datePicker2_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            // ... Get DatePicker reference.
            var picker = sender as DatePicker;

            // ... Get nullable DateTime from SelectedDate.
            DateTime? date = picker.SelectedDate;
            if (date == null)
            {
                // ... A null object.
                Title = "No date";
            }
            else
            {
                ToDate=datePicker2.SelectedDate.Value.ToString("dd.MM.yyyy");
            }
        }

        private void datepicker3_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            // ... Get DatePicker reference.
            var picker = sender as DatePicker;

            // ... Get nullable DateTime from SelectedDate.
            DateTime? date = picker.SelectedDate;
            if (date == null)
            {
                // ... A null object.
                Title = "No date";
            }
            else
            {
                // ... No need to display the time.
                DeductionDate = datepicker3.SelectedDate.Value.ToString("dd.MM.yyyy");
            }
        }

        private void datepicker4_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            // ... Get DatePicker reference.
            var picker = sender as DatePicker;

            // ... Get nullable DateTime from SelectedDate.
            DateTime? date = picker.SelectedDate;
            if (date == null)
            {
                // ... A null object.
                Title = "No date";
            }
            else
            {
                // ... No need to display the time.
                RemittanceDate= datepicker4.SelectedDate.Value.ToString("dd.MM.yyyy");
            }
        }

        private void datepicker5_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            // ... Get DatePicker reference.
            var picker = sender as DatePicker;

            // ... Get nullable DateTime from SelectedDate.
            DateTime? date = picker.SelectedDate;
            if (date == null)
            {
                // ... A null object.
                Title = "No date";
            }
            else
            {
                // ... No need to display the time.
                AuthorizedDate= datepicker5.SelectedDate.Value.ToString("dd.MM.yyyy");
            }
        }

      
    }
}
