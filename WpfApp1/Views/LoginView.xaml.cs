using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PrintingHouseApplication.ViewModels;

namespace PrintingHouseApplication.Views
{
    /// <summary>
    /// Логика взаимодействия для LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void TextBlock_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (this.DataContext != null)
            {
                (this.DataContext as LoginViewModel).VisiblePasswordOff();
            }

        }

        private void TextBlock_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.DataContext != null)
            {
                (this.DataContext as LoginViewModel).VisiblePasswordOn();
            }
        }

        private void PassMask_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null && sender != null)
            {
                PasswordBox pb = sender as PasswordBox;
                (this.DataContext as LoginViewModel).SetPassword(pb.Password);
            }
        }
    }
}
