using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrintingHouseApplication.Models;
using PrintingHouseApplication.MyCommand;
using PrintingHouseApplication.Views;

namespace PrintingHouseApplication.ViewModels
{
    class UpdDepartmentViewModel : BaseViewModel
    {
        private int _depId;
        private string _depName;
        private int _depChief;
        private List<Employee> _employees;
        private string _depPhone;
        private UpdDepartmentModel _updDepartmentModel = new UpdDepartmentModel();
        private Employee _selectedEmployee;
     
        public Command Update { get; private set; }
        public Command Cancel { get; private set; }
  
        public string DepName
        {
            get => _depName;
            set
            {
                _depName = value;
                OnPropertyChanged(nameof(DepName));
            }
        }
        public int DepChief
        {
            get => _depChief;
            set
            {
                _depChief = value;
                OnPropertyChanged(nameof(DepChief));
            }
        }
        public string DepPhone
        {
            get => _depPhone;
            set
            {
                _depPhone = value;
                OnPropertyChanged(nameof(DepPhone));
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
                Name = SelectedEmployee.Name;
                OnPropertyChanged(nameof(Name));                
            }
        }

        public string Name { get; set; }
     
        public UpdDepartmentViewModel()
        {
            Employees = _updDepartmentModel.GetEmployees();
            SelectedEmployee = _updDepartmentModel.GetNameEmployee(DepartmentModel.SelectedDepartment.Chief);
           
            _depId = DepartmentModel.SelectedDepartment.Id;
            DepName = DepartmentModel.SelectedDepartment.Name;
            DepPhone = DepartmentModel.SelectedDepartment.Phone;

            Update = new Command((_) =>
            {
                

                Department depart = new Department()
                {
                    Id = _depId,
                    Name = DepName,
                    Chief = SelectedEmployee.Id,
                    Phone = DepPhone
                };
                if (UpdDepartmentModel.UpdateDepartment(depart))
                {
                    MessageBox.Show("Изменение успешно выполнено", "Изменение", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Изменение отмененно", "Изменение", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                Application.Current.Windows.OfType<ActionView>().SingleOrDefault(x => x.IsActive).Close();
               
            }, null);

            Cancel = new Command((_) =>
            {
                Application.Current.Windows.OfType<ActionView>().SingleOrDefault(x => x.IsActive).Close();
            }, null);
        }
    }
}
