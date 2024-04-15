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

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для Reg.xaml
    /// </summary>
    public partial class Reg : Window
    {
        char[] SpecialChars = "!@#$%^&*()".ToCharArray();
        MainWindow main;
        public Reg()
        {
            InitializeComponent();

            Error.Content = "";

            Login.BorderBrush = new SolidColorBrush(Colors.White);

            Email.BorderBrush = new SolidColorBrush(Colors.White);

            PasswordBox.BorderBrush = new SolidColorBrush(Colors.White);

            ShowBox.BorderBrush = new SolidColorBrush(Colors.White);

            PasswordBox1.BorderBrush = new SolidColorBrush(Colors.White);

            ShowBox1.BorderBrush = new SolidColorBrush(Colors.White);

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }
        private void ToRegButtonClick(object sender, RoutedEventArgs e)
        {

            Error.Content = "";

            Login.BorderBrush = new SolidColorBrush(Colors.White);

            Email.BorderBrush = new SolidColorBrush(Colors.White);

            PasswordBox.BorderBrush = new SolidColorBrush(Colors.White);

            ShowBox.BorderBrush = new SolidColorBrush(Colors.White);

            PasswordBox1.BorderBrush = new SolidColorBrush(Colors.White);

            ShowBox1.BorderBrush = new SolidColorBrush(Colors.White);

       

            var login = Login.Text;

            var email = Email.Text;

            if (!email.Contains('@') || !email.Split("@")[1].Contains('.'))
            {
                Error.Content = "Данная почта некорректна";

                Email.BorderBrush = new SolidColorBrush(Colors.Red);

                return; 
            }

            var context = new AppDbContext();

            var user_exists = context.Users.FirstOrDefault(x => x.Login == login);

            if (user_exists != null)
            {
                Error.Content = "Данное имя пользователя уже используется";

                Login.BorderBrush = new SolidColorBrush(Colors.Red);
                return;
            }

             user_exists = context.Users.FirstOrDefault(x => x.e_mail == email);
            if (user_exists != null)
            {
                Error.Content = "Данная почта уже зарегистрирована";

                Email.BorderBrush = new SolidColorBrush(Colors.Red);
                return;
            }
            if (!check)
            {
                Hide_Click(null, null);
            }
            if (!check1)
            {
                Hide1_Click(null, null);
            }
            var pas1 = PasswordBox.Password;
            var pas2 = PasswordBox1.Password;
            if(pas1 != pas2)
            {
                Error.Content = "Пароли должны быть одинаковыми";

                PasswordBox1.BorderBrush = new SolidColorBrush(Colors.Red);

                ShowBox1.BorderBrush = new SolidColorBrush(Colors.Red);
                return;
            }
            if(pas1.Length < 8)
            {
                Error.Content = "Пароль должен быть не менее восьми символов";

                PasswordBox.BorderBrush = new SolidColorBrush(Colors.Red);

                ShowBox.BorderBrush = new SolidColorBrush(Colors.Red);

                return;
            }
            bool small = false;

            bool big = false;

            bool numbers = false;

            bool special = false;

            foreach(char c in pas1)
            {
                if(SpecialChars.Contains(c))
                {
                    special = true;
                }
                else

                if (Char.IsDigit(c))
                {
                    numbers = true;
                }
                else 
                if(Char.IsLetter(c))
                {
                    if (Char.IsUpper(c))
                    {
                        big = true;
                    }
                    else
                    {
                        small = true;
                        
                    }
                }
            }
            if (!small)
            {
                Error.Content = "В пароле должны быть маленькие буквы";

                PasswordBox.BorderBrush = new SolidColorBrush(Colors.Red);

                ShowBox.BorderBrush = new SolidColorBrush(Colors.Red);

                return;
            }

            if (!big)
            {
                Error.Content = "В пароле должны быть большие буквы";

                PasswordBox.BorderBrush = new SolidColorBrush(Colors.Red);

                ShowBox.BorderBrush = new SolidColorBrush(Colors.Red);

                return;
            }

            if (!numbers)
            {
                Error.Content = "В пароле должны быть цифры";

                PasswordBox.BorderBrush = new SolidColorBrush(Colors.Red);

                ShowBox.BorderBrush = new SolidColorBrush(Colors.Red);

                return;
            }

            if (!special)
            {
                Error.Content = "В пароле должны быть специальные символы";

                PasswordBox.BorderBrush = new SolidColorBrush(Colors.Red);

                ShowBox.BorderBrush = new SolidColorBrush(Colors.Red);

                return;
            }
            var user = new User { Login = login, Password = pas1, e_mail = email };
            context.Users.Add(user);
            context.SaveChanges();
            main = new MainWindow();

            main.Show();

            this.Hide();
        }
        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.main.Show();
            MainWindow.reg.Hide();           
        }

        public bool check = true;
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
        public bool check1 = true;
        private void Hide1_Click(object sender, RoutedEventArgs e)
        {
            if (check1)
            {
                PasswordBox1.Visibility = Visibility.Collapsed;

                ShowBox1.Visibility = Visibility.Visible;

                ShowBox1.Text = PasswordBox1.Password;

                check1 = false;

                Hide1.Visibility = Visibility.Collapsed;

                Show1.Visibility = Visibility.Visible;

            }
            else
            {
                PasswordBox1.Visibility = Visibility.Visible;
                ShowBox1.Visibility = Visibility.Collapsed;
                PasswordBox1.Password = ShowBox1.Text;
                check1 = true;
                Hide1.Visibility = Visibility.Visible;
                Show1.Visibility = Visibility.Collapsed;
            }
        }
        private void OnLoginChange (object sender, EventArgs e)
        {
            if (sender is TextBox box)
            {
                if (string.IsNullOrEmpty(box.Text))
                    box.Background = (ImageBrush)FindResource("watermark");
                else
                    box.Background = Brushes.White;
            }
        }
        private void OnEmailChange(object sender, EventArgs e)
        {
            if (sender is TextBox box)
            {
                if (string.IsNullOrEmpty(box.Text))
                    box.Background = (ImageBrush)FindResource("email");
                else
                    box.Background = Brushes.White;
            }
        }
        private void OnPasswordChange(object sender, EventArgs e)
        {
            if (sender is TextBox box)
            {
                if (string.IsNullOrEmpty(box.Text))
                    box.Background = (ImageBrush)FindResource("password");
                else
                    box.Background = Brushes.White;
            }
            if (sender is PasswordBox box1)
            {
                if (string.IsNullOrEmpty(box1.Password))
                    box1.Background = (ImageBrush)FindResource("password");
                else
                    box1.Background = Brushes.White;
            }
        }
    }
}
