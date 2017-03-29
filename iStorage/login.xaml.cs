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

namespace iStorage
{
    /// <summary>
    /// Interaction logic for login.xaml
    /// </summary>
    /// 
    public partial class login : Window
    {

        string Pw, Bn;
        ForMembers Staff = new ForMembers();
        Error Bad = new Error();

        public login()
        {
            InitializeComponent();
        }


        private void Text_UserName_TextChanged(object sender, TextChangedEventArgs e)
        {
            Bn = Text_UserName.Text;
        }

        private void Text_Password_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void Button_Login_Click(object sender, RoutedEventArgs e)
        {
            if(Bn == "admin")
            {
                this.Close();
                Staff.Show();
            }

            else
            {
                Error Bad = new Error();
                Bad.Show();
            }


        }


    }
}
