using System.Windows.Media;
using PrintingHouseApplication.Models;

namespace PrintingHouseApplication.ViewModels
{
    class ProfileViewModel : BaseViewModel
    {
        private string _employeeRole;
        private Employee _currentEmployee;
        private ImageSource _imgSource;

        public ImageSource ImgSource
        {
            get => _imgSource;
            set
            {
                _imgSource = value;
                OnPropertyChanged(nameof(ImgSource));
            }
        }

        public Employee CurrentEmployee
        {
            get => _currentEmployee;
            set
            {
                _currentEmployee = value;
                OnPropertyChanged(nameof(CurrentEmployee));
            }
        }

        
        public string EmployeeRole
        {
            get => "Должность: " + _employeeRole;
            private set
            {
                _employeeRole = value;
                OnPropertyChanged(nameof(EmployeeRole));
            }
        }

        public ProfileViewModel()
        {
            CurrentEmployee = UserHelper.GetEmployee();           
            EmployeeRole = UserHelper.GetUserRole();
            ImgSource = ProfileModel.GetImage();
        }
    }
}
