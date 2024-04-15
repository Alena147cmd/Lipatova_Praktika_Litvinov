using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow main;

        public static Reg reg;

        int fails = 0;

        long Block = 0;

        public MainWindow()
        {
            InitializeComponent();


            Error.Content = "";

            LoginBox.BorderBrush = new SolidColorBrush(Colors.White);

            PasswordBox.BorderBrush = new SolidColorBrush(Colors.White);
        }
        private void Loginn_Click(object sender, RoutedEventArgs e)
        {

            Error.Content = "";

            LoginBox.BorderBrush = new SolidColorBrush(Colors.White);

            PasswordBox.BorderBrush = new SolidColorBrush(Colors.White);

            long diff = Block - DateTimeOffset.Now.ToUnixTimeMilliseconds() ;

            if(diff > 0)
            {
                Error.Content = "Вход заблокирован на " + (diff / 1000) + " секунд";
                return;
            }
            var login = LoginBox.Text;

            if (!check)
            {
                Hide_Click(null, null);
            }

            var password = PasswordBox.Password;

            var context = new AppDbContext();

            var user = context.Users.SingleOrDefault(x => (x.Login == login || x.e_mail == login) && x.Password == password);
         
            if (user == null || user.Password != password)
            {
                Error.Content = "Неправильный логин или пароль";

                fails++;

                if( fails >= 3)
                {
                    Block = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 15000;

                    fails = 0;

                    Error.Content = "Неправильный логин или пароль, вход заблокирован на 15 секунд";

                    LoginBox.IsEnabled = false;

                    PasswordBox.IsEnabled = false;

                   var timer = new DispatcherTimer(DispatcherPriority.Render);

                    timer.Interval = TimeSpan.FromSeconds(15);

                    timer.Tick += (Sender, args) =>
                    {
                        LoginBox.IsEnabled = true;

                        PasswordBox.IsEnabled = true;

                        timer.Stop();
                    };

                    timer.Start();
                }

                LoginBox.BorderBrush = new SolidColorBrush(Colors.Red);

                PasswordBox.BorderBrush = new SolidColorBrush(Colors.Red);
                
                return;
            }

           

            this.Hide();
        }

        private void ComeToReg(object sender, RoutedEventArgs e)
        {
            reg = new Reg();

            main = this;

            reg.Show();

            this.Hide();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }

        private bool check = true;

        private void Hide_Click(object sender, RoutedEventArgs e)
        {
            if (check) 
            {
                PasswordBox.Visibility = Visibility.Collapsed;
                ShowBox.Visibility = Visibility.Visible;
                ShowBox.Text = PasswordBox.Password;
                check = false;
                Hide.Visibility = Visibility.Collapsed;
                Show.Visibility = Visibility.Visible;
            }
            else 
            {
                PasswordBox.Visibility = Visibility.Visible;
                ShowBox.Visibility = Visibility.Collapsed;
                PasswordBox.Password = ShowBox.Text;
                check = true;
                Hide.Visibility = Visibility.Visible;
                Show.Visibility = Visibility.Collapsed;
            }
        }
    }
}
