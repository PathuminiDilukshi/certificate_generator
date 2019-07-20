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
using System.Windows.Shapes;

namespace Certificate_generator
{
    /// <summary>
    /// Interaction logic for User_dashbord.xaml
    /// </summary>
    public partial class User_dashbord : Window
    {
        public User_dashbord()
        {
            InitializeComponent();
        }


        private void Create_Certificate_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new CertificatePage();
        }

        private void Add_customer_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Create_Customer();
        }

        private void Update_customer_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Update_Customer();
        }
    }
}
