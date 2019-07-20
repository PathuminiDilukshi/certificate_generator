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

namespace Certificate_generator
{
    /// <summary>
    /// Interaction logic for Create_Customer.xaml
    /// </summary>
    public partial class Create_Customer : Page
    {
        public SqlConnection con = null;
        public SqlCommand com = null;
        public SqlDataAdapter adapter = null;
        public DataSet ds = null;
        public int PayCode;
        public static string HL_val;
        public static string CLT_val;
        public static string CLW_val;
        public static string CPS_val;
        public static string ILS_val;

        private static string ConString = @"Data Source=PC-13;Initial Catalog=Certificate_Generating_System;User ID=sa;Password=abc123;";

        public Create_Customer()
        {
            InitializeComponent();
            BindPaymentType(Payemeny_method);
        }

       

        public void BindPaymentType(ComboBox payment_type)
        {
            try
            {
                con = new SqlConnection(ConString);
                con.Open();
                com = new SqlCommand("SELECT Nature_of_payment FROM Payment_methods", con);
                com.CommandType = CommandType.Text;
                SqlDataReader dr = com.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        string payment_method = dr["Nature_of_payment"].ToString().Trim();
                        payment_type.Items.Add(payment_method);

                    }
                }
                con.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            bool return_val = Validationmethod();

            if (return_val == true)
            {
                try
                {
                    string query = "INSERT INTO Cusomer_details(Cus_no,Customer_Name,Addressline1,Addressline2,Addressline3,payment_code,Contact_no,email)VALUES('" + this.cus_no.Text.ToString() + "','" + this.CustomerName.Text.ToString() + "','" + this.AddrLine1.Text.ToString() + "','" + this.AddrLine2.Text.ToString() + "','" + this.AddrLine3.Text.ToString() + "','" + this.PayCode + "','" + this.ContactNo.Text.ToString() + "','" + this.Email.Text.ToString() + "');";


                     con.Open();
                     com = new SqlCommand(query, con);
                     com.ExecuteNonQuery();
                     com.Dispose();
                     con.Close();

                     customer_agent();
                     MessageBox.Show("Data inserted Successfully");
                     this.Refresh();

                }
                catch (Exception ex)
                {
                     MessageBox.Show(ex.ToString());
                }
            }

            
        }

        private bool Validationmethod()
        {
         
                if (CustomerName.Text.Length == 0)
                {
                    errormessage1.Text = "Customer Name Cannot be Empty";
                    CustomerName.Focus();
                    return false;
                }

                else if (cus_no.Text.Length == 0)
                {
                    errormessage_cno.Text = "Enter Cusomer's NIC/TIN/Passport No";
                    cus_no.Focus();
                    return false;
                }

                else if (AddrLine1.Text.Length == 0)
                {
                    errormessage_addr1.Text = "Please fill Address Line1";
                    AddrLine1.Focus();
                    return false;
                }

                else if (AddrLine2.Text.Length == 0)
                {
                    errormessage_addr2.Text = "Please fill Address Line2";
                    AddrLine2.Focus();
                    return false;
                }

                else if (AddrLine3.Text.Length == 0)
                {
                    errormessage_addr3.Text = "Please fill City";
                    AddrLine3.Focus();
                    return false;
                }

                else if (Payemeny_method.Text.Length == 0)
                {
                    errormessage_payment.Text = "Select Payment Method";
                    Payemeny_method.Focus();
                    return false;
                }

                
                    if (CLT.IsChecked == false)
                    {
                        errormessage_checkbox.Text = "Select Atleast One CheckBox";
                        return false;
                    }

                    if (CLW.IsChecked == false)
                    {
                        errormessage_checkbox.Text = "Select Atleast One CheckBox";
                        return false;
                    }

                    if (CPS.IsChecked == false)
                    {
                        errormessage_checkbox.Text = "Select Atleast One CheckBox";
                        return false;
                    }

                    if (HL.IsChecked == false)
                    {
                        errormessage_checkbox.Text = "Select Atleast One CheckBox";
                        return false;
                    }

                    if (ILS.IsChecked == false)
                    {
                        errormessage_checkbox.Text = "Select Atleast One CheckBox";
                        return false;
                    }



                if ((CustomerName.Text.Length != 0) && (cus_no.Text.Length != 0) && (AddrLine1.Text.Length != 0) && (AddrLine2.Text.Length != 0) && (AddrLine3.Text.Length != 0) && (Payemeny_method.Text.Length != 0))
                {
                    errormessage1.Text = "";
                    CustomerName.Focus();

                    errormessage_cno.Text = "";
                    cus_no.Focus();

                    errormessage_addr1.Text = "";
                    AddrLine1.Focus();

                    errormessage_addr2.Text = "";
                    AddrLine2.Focus();

                    errormessage_addr3.Text = "";
                    AddrLine3.Focus();

                    errormessage_payment.Text = "";
                    Payemeny_method.Focus();

                    return true;
                }
                return true;
        }


        public void customer_agent()
        {
            if (HL.IsChecked == true)
            {
                HL_val = "HL";
                string query1 = "INSERT INTO Customers_agents(Customer_id,WH_agent)VALUES('" + this.cus_no.Text.ToString() + "','" + HL_val + "');";
                combo_insert(query1);
            }

            if (CLW.IsChecked == true)
            {
                 CLW_val = "CLW";
                 string query2 = "INSERT INTO Customers_agents(Customer_id,WH_agent)VALUES('" + this.cus_no.Text.ToString() + "','" + CLW_val + "');";
                 combo_insert(query2);
            }
            
            if (CLT.IsChecked == true)
            {
                CLT_val = "CLT";
                string query3 = "INSERT INTO Customers_agents(Customer_id,WH_agent)VALUES('" + this.cus_no.Text.ToString() + "','" + CLT_val + "');";
                combo_insert(query3);
            }
            if (CPS.IsChecked == true)
            {
                CPS_val = "CPS";
                string query4 = "INSERT INTO Customers_agents(Customer_id,WH_agent)VALUES('" + this.cus_no.Text.ToString() + "','" + CPS_val + "');";
                combo_insert(query4);
            }
            if (ILS.IsChecked == true)
            {
                ILS_val = "ILS";
                string query5 = "INSERT INTO Customers_agents(Customer_id,WH_agent)VALUES('" + this.cus_no.Text.ToString() + "','" + ILS_val + "');";
                combo_insert(query5);
            }

        }

        public void combo_insert(string query)
        {
            con.Open();
            com = new SqlCommand(query, con);
            com.ExecuteNonQuery();
            com.Dispose();
            con.Close();
        }

        private void Payemeny_method_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string payMethod = Payemeny_method.SelectedItem as string;

                con = new SqlConnection(ConString);
                con.Open();
                com = new SqlCommand("SELECT pay_code FROM Payment_methods where Nature_of_payment ='" + payMethod + "'", con);
                com.CommandType = CommandType.Text;
                SqlDataReader dr = com.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        PayCode = Convert.ToInt32(dr["pay_code"]);
                    }
                }
                con.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }


        private void Refresh()
        {
            cus_no.Text = null;
            CustomerName.Text = null;
            ContactNo.Text = null;
            Email.Text = null;
            AddrLine1.Text = null;
            AddrLine2.Text = null;
            AddrLine3.Text = null;
            Payemeny_method.Text = null;
            CLT.IsChecked = false;
            CLW.IsChecked = false;
            CPS.IsChecked = false;
            HL.IsChecked = false;
            ILS.IsChecked = false;

        }

 

       
    }
}
