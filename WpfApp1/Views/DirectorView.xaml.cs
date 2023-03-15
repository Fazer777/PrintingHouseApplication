using System;
using System.Linq;
using System.Windows;

namespace PrintingHouseApplication.Views
{
    /// <summary>
    /// Логика взаимодействия для DirectorView.xaml
    /// </summary>
    public partial class DirectorView : Window
    {
        public DirectorView()
        {
            InitializeComponent();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Windows.OfType<LoginView>().SingleOrDefault(x => !x.IsVisible).ShowDialog();
        }   
    }
}
