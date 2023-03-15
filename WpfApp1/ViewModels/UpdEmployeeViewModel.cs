using System;
using System.Windows;
using System.Linq;
using PrintingHouseApplication.MyCommand;
using PrintingHouseApplication.Models;
using PrintingHouseApplication.Views;

namespace PrintingHouseApplication.ViewModels
{
    class UpdEmployeeViewModel : BaseViewModel
    {
        private int _empId;
        private string _empName;
        private string _empSurname;
        private string _empPatronymic;
        private string _empPhone;
        private readonly UpdEmployeeModel _updEmployeeModel = new UpdEmployeeModel();

        public Command Update { get; private set; }
        public Command Cancel { get; private set; }

        public UpdEmployeeViewModel()
        {
            _empId = EmployeeModel.SelectedEmployee.Id;
            EmpName = EmployeeModel.SelectedEmployee.Name;
            EmpSurname = EmployeeModel.SelectedEmployee.Surname;
            EmpPatronymic = EmployeeModel.SelectedEmployee.Patronymic;
            EmpPhone = EmployeeModel.SelectedEmployee.Phone;

            Update = new Command((_) => {
                Employee emp = new Employee()
                {
                    Id = _empId,
                    Surname = EmpSurname,
                    Name = EmpName,
                    Patronymic = EmpPatronymic,
                    Phone = EmpPhone
                };
                if (_updEmployeeModel.UpdateEmployee(emp))
                {
                    MessageBox.Show("Изменение успешно выполнено", "Изменение", MessageBoxButton.OK, MessageBoxImage.Information);
                    Application.Current.Windows.OfType<ActionView>().SingleOrDefault(x => x.IsActive).Close();
                }
                else
                {
                    MessageBox.Show("Ошибка выполнения изменения", "Изменение", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                
            }, null);

            Cancel = new Command((_) => {
                Application.Current.Windows.OfType<ActionView>().SingleOrDefault(x => x.IsActive).Close();
            }, null);
        }

        public string EmpName
        {
            get => _empName;
            set
            {
                _empName = value;
                OnPropertyChanged(nameof(EmpName));
            }
        }
        public string EmpSurname
        {
            get => _empSurname;
            set
            {
                _empSurname = value;
                OnPropertyChanged(nameof(EmpSurname));
            }
        }
        public string EmpPatronymic
        {
            get => _empPatronymic;
            set
            {
                _empPatronymic = value;
                OnPropertyChanged(nameof(EmpPatronymic));
            }
        }
        public string EmpPhone
        {
            get => _empPhone;
            set
            {
                _empPhone = value;
                OnPropertyChanged(nameof(EmpPhone));
            }
        }
    }
}
