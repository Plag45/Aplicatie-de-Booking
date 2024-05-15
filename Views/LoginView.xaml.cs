using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;


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
    }
}
