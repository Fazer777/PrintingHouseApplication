using System.Windows;
using PrintingHouseApplication.ViewModels;

namespace PrintingHouseApplication.Views
{
    /// <summary>
    /// Логика взаимодействия для ActionView.xaml
    /// </summary>
    public partial class ActionView : Window
    {
        public ActionView(object viewModel = null)
        {
            InitializeComponent();
            if (this.DataContext != null)
            {
                (this.DataContext as ActionViewModel).SelectedViewModel = viewModel;
            }
            
        }
    }
}
