using Microsoft.Data.SqlClient;
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

namespace Aplicatie_de_Booking.Views
{
    /// <summary>
    /// Interaction logic for RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Window
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnRegistration_Click(object sender, RoutedEventArgs e)
        {
           
            string username = txtUser.Text;
            string email = txtEmail.Text;
            string Name = txtName.Text;
            string Password = txtPassword.Password;


            
            string connectionString = "Server=tcp:eazybooking.database.windows.net,1433;Initial Catalog=EazyBooking;Persist Security Info=False;User ID=AdminNita;Password=Costina123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            // SQL query to insert data
            string query = "INSERT INTO Users (Username, Email, Name, Password) VALUES (@Username, @Email, @Name, @Password)";

            // Create and open a connection to SQL Server
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create a SqlCommand object
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Define the parameters and their values
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Name", Name);
                    command.Parameters.AddWithValue("@Password", Password);

                    // Open the connection to the database
                    connection.Open();

                    // Execute the command
                    command.ExecuteNonQuery();

                    // Close the connection
                    connection.Close();
                    MessageBox.Show("Te-ai înregistrat cu succes");
                    this.Close();
                }
            }

            
        }
    }
}
