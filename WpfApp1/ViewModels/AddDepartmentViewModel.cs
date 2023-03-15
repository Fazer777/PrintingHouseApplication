using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using PrintingHouseApplication.Models;
using PrintingHouseApplication.MyCommand;
using PrintingHouseApplication.Views;

namespace PrintingHouseApplication.ViewModels
{
    class AddDepartmentViewModel : BaseViewModel
    {
        private int _departId;
        private string _departName;
        private List<Employee> _employees;
        private string _departPhone;
        private AddDepartmentModel _addDepartModel = new AddDepartmentModel();
        private Employee _selectedEmployee;

        public Command Add { get; private set; }
        public Command Cancel { get; private set; }

        public AddDepartmentViewModel()
        {
            Employees = _addDepartModel.GetEmployees();

            Add = new Command((_) =>
            {
                Department department = new Department()
                {
                    Id = DepartId,
                    Name = DepartName,
                    Chief = SelectedEmployee.Id,
                    Phone = DepartPhone
                };

                if (_addDepartModel.CreateDepartment(department))
                {
                    MessageBox.Show("Добавление успешно выполнено", "Добавление", MessageBoxButton.OK, icon: MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Ошибка добавления записи", "Добавление", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                SelectedEmployee = null;
                Application.Current.Windows.OfType<ActionView>().SingleOrDefault(x => x.IsActive).Close();
            }, null);

            Cancel = new Command((_) =>
            {
                Application.Current.Windows.OfType<ActionView>().SingleOrDefault(x => x.IsActive).Close();
            }, null);
        }

        public int DepartId
        {
            get => _departId;
            set
            {
                _departId = value;
                OnPropertyChanged(nameof(DepartId));
            }
        }
        public string DepartName
        {
            get => _departName;
            set
            {
                _departName = value;
                OnPropertyChanged(nameof(DepartName));
            }
        }

        public string DepartPhone
        {
            get => _departPhone;
            set
            {
                _departPhone = value;
                OnPropertyChanged(nameof(DepartPhone));
            }
        }
        public List<Employee> Employees
        {
            get => _employees;
            set
            {
                _employees = value;
                OnPropertyChanged(nameof(Employees));
            }
        }

        public Employee SelectedEmployee
        {
            get => _selectedEmployee;
            set
            {
                _selectedEmployee = value;
                OnPropertyChanged(nameof(SelectedEmployee));
            }
        }
    }
}