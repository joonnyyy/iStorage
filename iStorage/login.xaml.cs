using System.Windows;
using System.Collections.Generic;
using System;

namespace iStorage
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {

        BackendAPI API = new BackendAPI();

        public Login()
        {
            //InitializeComponent();

            int rows;

            //Erstelle eine Liste mit Inhalt Liste von String, Lade Daten aus der Datenbank und füge sie ein
            List<List<String>> data = API.AllArticlesLimitedInfo(out rows);

            //Gib aus wieviele Reihen geladen wurden
            Console.WriteLine("Starting output of {0} rows.", rows);

            //Gleiches vorgehen wie mit 2 Dimensionalen Arrays
            for(int row = 0; row < data.Count; row++)
            {
                for (int column = 0; column < data[row].Count; column++)
                    Console.Write("{0}\t",data[row][column]);
            }
        }

        private void Login_Button_Clicked(object sender, RoutedEventArgs e)
        {
            Login_Button.IsEnabled = false;
            Username_Input.IsEnabled = false;
            Password_Input.IsEnabled = false;
            string input_user = Username_Input.Text;
            string input_password = Password_Input.Password;
            Username_Input.Text = "";
            Password_Input.Password = "";

            string Answer_API_Login = API.Login(input_user, input_password);

            if(Answer_API_Login != "loggedin")
            {
                Login_Button.IsEnabled = true;
                Username_Input.IsEnabled = true;
                Password_Input.IsEnabled = true;
                Username_Input.Text = Answer_API_Login;
                Username_Input.Focus();
                Username_Input.SelectAll();
            }
        }
    }
}