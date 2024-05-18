using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.Data.SqlClient;


namespace Aplicatie_de_Booking.Views
{

    public partial class LoginView : Window
    {
        public LoginView()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        public void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void CreatOne_Click(object sender, RoutedEventArgs e)
        {
            RegistrationPage _registration = new RegistrationPage();
            _registration.ShowDialog();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            
            string connectionString = "Server = tcp:eazybooking.database.windows.net,1433; Initial Catalog = EazyBooking; Persist Security Info = False; User ID = AdminNita; Password = Costina123; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30; ";
            string username = txtUser.Text;  // Assuming you have a TextBox named txtUsername
            string password = txtPassword.Password;  // Assuming you have a TextBox named txtPassword

            // SQL query to check if the username and password exist
            string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username AND Password = @Password";  // Make sure to hash passwords in a real application

            // Create a connection and command
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);  

                 
                    connection.Open();

                    
                    int userCount = (int)command.ExecuteScalar();  

                    
                    if (userCount > 0)
                    {
                        
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Invalid username or password!", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    
                    connection.Close();
                }
            }
        }
    }
}
