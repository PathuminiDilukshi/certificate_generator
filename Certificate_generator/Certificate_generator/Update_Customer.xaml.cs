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
    /// Interaction logic for Update_Customer.xaml
    /// </summary>
    public partial class Update_Customer : Page
    {
        public static SqlConnection con = new SqlConnection();
        public static SqlCommand com = new SqlCommand();
        string cno;
        string cus_name;
        string addr1;
        string addr2 ;
        string addr3 ;
        string contact;
        string email;
        string payment;
        static int pay_code;
        string cus_no;
        public static string HL_val;
        public static string CLT_val;
        public static string CLW_val;
        public static string CPS_val;
        public static string ILS_val;


        private static string ConString = @"Data Source=PC-13;Initial Catalog=Certificate_Generating_System;User ID=sa;Password=abc123;";

        public Update_Customer()
        {
            InitializeComponent();

        }

        private void Refresh()
        {
            Customer_code.Text=null;
            Cus_name.Text=null;
            Cus_no.Text=null;
            Contact_number.Text=null;
            Email.Text=null;
            Address1.Text=null;
            Address2.Text=null;
            Address3.Text=null;
            Payement_method.Text=null;
            RadioBut_cusID.IsChecked=false;
            RadioBut_CusName.IsChecked = false;
            CLT.IsChecked = false;
            CLW.IsChecked = false;
            CPS.IsChecked = false;
            HL.IsChecked = false;
            ILS.IsChecked=false;
            resultStack.Children.Clear();
            
        }

        static public List<string> GetCustomerName()
        {

                string query = "SELECT Customer_Name from  Cusomer_details";
                con = new SqlConnection(ConString);
                con.Open();
                com = new SqlCommand(query, con);
                com.CommandType = CommandType.Text;
                SqlDataReader dr = com.ExecuteReader();

                List<string> data = new List<string>();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        data.Add((string)dr["Customer_Name"]);
                    }
                }
                con.Close();


            return data;
         }

        static public List<string> GetData()
        {

            string query = "SELECT Cus_no from  Cusomer_details";
            con = new SqlConnection(ConString);
            con.Open();
            com = new SqlCommand(query, con);
            com.CommandType = CommandType.Text;
            SqlDataReader dr = com.ExecuteReader();

            List<string> data = new List<string>();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    data.Add((string)dr["Cus_no"]);
                }
            }
            con.Close();


            return data;
        }  
 

        private void update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string cusname= Cus_name.Text;
                cus_no=Cus_no.Text ;
                string tel=Contact_number.Text;
                string email=Email.Text;
                string addt1=Address1.Text;
                string addr2=Address2.Text;
                string addr3=Address3.Text;
            

                con = new SqlConnection(ConString);
                con.Open();
                com = new SqlCommand("SP_UPDATECustomerDetails", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@Cus_no", SqlDbType.NChar, 15).Value = cus_no;
                com.Parameters.Add("@Customer_Name", SqlDbType.NChar, 35).Value = cusname;
                com.Parameters.Add("@Addressline1", SqlDbType.NChar, 35).Value = addt1;
                com.Parameters.Add("@Addressline2", SqlDbType.NChar, 35).Value = addr2;
                com.Parameters.Add("@Addressline3", SqlDbType.NChar, 35).Value = addr3;
                com.Parameters.Add("@payment_code", SqlDbType.Int).Value = pay_code;
                com.Parameters.Add("@Contact_no", SqlDbType.NChar, 20).Value = tel;
                com.Parameters.Add("@email", SqlDbType.NChar, 30).Value = email;


                com.ExecuteNonQuery();
                com.Dispose();
                con.Close();
                combo_delete();
                customer_agent();

                MessageBox.Show("Data Updated Successfully");

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void customer_agent()
        {

            if (HL.IsChecked == true)
            {
                HL_val = "HL";
                combo_insert(HL_val);
            }

            if (CLW.IsChecked == true)
            {
                CLW_val = "CLW";
                combo_insert(CLW_val);
            }

            if (CLT.IsChecked == true)
            {
                CLT_val = "CLT";
                combo_insert(CLT_val);
            }

            if (CPS.IsChecked == true)
            {
                CPS_val = "CPS";
                combo_insert(CPS_val);
            }
            if (ILS.IsChecked == true)
            {
                ILS_val = "ILS";
                combo_insert(ILS_val);
            }

        }

        public void combo_insert(string agent_val)
        {
            con.Open();
            com = new SqlCommand("SP_UPDATECustomerAgent", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@Cus_no", SqlDbType.NChar, 15).Value = cus_no;
            com.Parameters.Add("@agent", SqlDbType.NChar, 5).Value = agent_val;
            com.ExecuteNonQuery();
            com.Dispose();
            con.Close();
        }

        public void combo_delete()
        {
            con.Open();
            com = new SqlCommand("SP_DELETECustomerAgent", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@Cus_no", SqlDbType.NChar, 15).Value = cus_no;
            com.ExecuteNonQuery();
            com.Dispose();
            con.Close();
        }

        private void Search_Customer_Click(object sender, RoutedEventArgs e)
        {
            if (RadioBut_cusID.IsChecked == true)
            {
                Cusno_details();
            }
            else if (RadioBut_CusName.IsChecked == true)
            {
              
                cusName_details();
            }

        }

        private void Cusno_details()
        {
            try
            {
                string cus_val = Customer_code.Text.ToString();

                con = new SqlConnection(ConString);
                con.Open();
                com = new SqlCommand("SP_GETCustomerDetails", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@CUSID", SqlDbType.NChar, 15).Value = cus_val;
                SqlDataReader dr = com.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                         cno = ((string)dr["Cus_no"]);
                         cus_name = ((string)dr["Customer_Name"]);
                         addr1 = ((string)dr["Addressline1"]);
                         addr2 = ((string)dr["Addressline2"]);
                         addr3 = ((string)dr["Addressline3"]);
                         contact = ((string)dr["Contact_no"]);
                         email = ((string)dr["email"]);
                         payment = ((string)dr["Nature_of_payment"]);
                         pay_code = Convert.ToInt32(dr["pay_code"]);

                    }
                }

                Cus_name.Text=cus_name;
                Cus_no.Text = cno;
                Contact_number.Text=contact;
                Email.Text=email;
                Address1.Text=addr1;
                Address2.Text=addr2;
                Address3.Text=addr3;
                Payement_method.Text = payment;
                checked_WH_agent();
            
                con.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void checked_WH_agent()
        {
            try
            {
                string cus_val = Customer_code.Text.ToString();

                con = new SqlConnection(ConString);
                con.Open();
                com = new SqlCommand("SP_GETCustomer_agents", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@CUSID", SqlDbType.NChar, 15).Value = cus_val;
                SqlDataReader dr = com.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        string agent = ((string)dr["WH_agent"]).Trim();
                        agent_Setval(agent);
                        
                    }
                }
                     
                 
                con.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void agent_Setval(string agent)
        {
           
            if (agent == "CLT")
            {
                CLT.IsChecked = true;
            }
            if (agent == "HL")
            {
                HL.IsChecked = true;
            }
            if (agent == "ILS")
            {
                ILS.IsChecked = true;
            }
            if (agent == "CLW")
            {
                CLW.IsChecked = true;
            }
            if (agent == "CPS")
            {
                CPS.IsChecked = true;
            }


        }

        private void cusName_details()
        {
            try
            {
                string cus_val = Customer_code.Text.ToString();

                con = new SqlConnection(ConString);
                con.Open();
                com = new SqlCommand("SP_GETCustomerDetails2", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@CUSName", SqlDbType.NChar, 35).Value = cus_val;
                SqlDataReader dr = com.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cno = ((string)dr["Cus_no"]);
                        cus_name = ((string)dr["Customer_Name"]);
                        addr1 = ((string)dr["Addressline1"]);
                        addr2 = ((string)dr["Addressline2"]);
                        addr3 = ((string)dr["Addressline3"]);
                        contact = ((string)dr["Contact_no"]);
                        email = ((string)dr["email"]);
                        payment = ((string)dr["Nature_of_payment"]);
                        pay_code = Convert.ToInt32(dr["pay_code"]);
                    }
                }

                Cus_name.Text = cus_name;
                Cus_no.Text = cno;
                Contact_number.Text = contact;
                Email.Text = email;
                Address1.Text = addr1;
                Address2.Text = addr2;
                Address3.Text = addr3;
                Payement_method.Text = payment;
                checked_WH_agent();

                con.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

     

        private void Customer_code_KeyUp(object sender, KeyEventArgs e)
        {
            if (RadioBut_cusID.IsChecked == true)
            {

                bool found = false;
                var border = (resultStack.Parent as ScrollViewer).Parent as Border;
                var data = Update_Customer.GetData();

                string query = (sender as TextBox).Text;

                if (query.Length == 0)
                {
                    // Clear   
                    resultStack.Children.Clear();
                    border.Visibility = System.Windows.Visibility.Collapsed;
                }
                else
                {
                    border.Visibility = System.Windows.Visibility.Visible;
                }

                // Clear the list   
                resultStack.Children.Clear();

                // Add the result   
                foreach (var obj in data)
                {
                    if (obj.ToLower().StartsWith(query.ToLower()))
                    {
                        // The word starts with this... Autocomplete must work   
                        addItem(obj);
                        found = true;
                    }  
                }
                

                if (!found)
                {
                    resultStack.Children.Add(new TextBlock() { Text = "No results found." });
                }
                

            }

            else if (RadioBut_CusName.IsChecked == true)
            {
                bool found = false;
                var border = (resultStack.Parent as ScrollViewer).Parent as Border;
                var data = Update_Customer.GetCustomerName();

                string query = (sender as TextBox).Text;

                if (query.Length == 0)
                {
                    // Clear   
                    resultStack.Children.Clear();
                    border.Visibility = System.Windows.Visibility.Collapsed;
                }
                else
                {
                    border.Visibility = System.Windows.Visibility.Visible;
                }

                // Clear the list   
                resultStack.Children.Clear();

                // Add the result   
                foreach (var obj in data)
                {
                    if (obj.ToLower().StartsWith(query.ToLower()))
                    {
                        // The word starts with this... Autocomplete must work   
                        addItem(obj);
                        found = true;
                    }
                }

                if (!found)
                {
                    resultStack.Children.Add(new TextBlock() { Text = "No results found." });
                }
            }
        }

        private void addItem(string text)
        {
            TextBlock block = new TextBlock();

            // Add the text   
            block.Text = text;

            // A little style...   
            block.Margin = new Thickness(2, 3, 2, 3);
            block.Cursor = Cursors.Hand;

            // Mouse events   
            block.MouseLeftButtonUp += (sender, e) =>
            {
                Customer_code.Text = (sender as TextBlock).Text;
            };

            block.MouseEnter += (sender, e) =>
            {
                TextBlock b = sender as TextBlock;
                b.Background = Brushes.PeachPuff;
            };

            block.MouseLeave += (sender, e) =>
            {
                TextBlock b = sender as TextBlock;
                b.Background = Brushes.Transparent;
            };

            // Add to the panel   
            resultStack.Children.Add(block);
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            this.Refresh();
        }  
    }

    
}
