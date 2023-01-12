using BlApi;
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

namespace PL.Admin
{
    /// <summary>
    /// Interaction logic for Admin_Password.xaml
    /// </summary>
    public partial class Admin_Password : Page
    {
        IBl bl;
        public Admin_Password(IBl bl)
        {
            InitializeComponent();
            this.bl = bl;
        }

       

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EnterPassword();
        }

        private void EnterPassword()
        {
            if (PasswordBox.Password == "1")
            {
                adminMenu adminHome = new(bl);
                PasswordBox.Password = "";
                adminHome.Show();
            }
            else
            {
                PasswordBox.Password = "";
                errorPassword.Visibility = Visibility.Visible;
            }
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            errorPassword.Visibility = Visibility.Collapsed;
        }
    }
}

