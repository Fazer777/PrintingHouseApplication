using System;
using System.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using PrintingHouseApplication.Models;
using PrintingHouseApplication.MyCommand;
using PrintingHouseApplication.Views;

namespace PrintingHouseApplication.ViewModels
{
    class EmployeeViewModel : BaseViewModel
    {
        private ObservableCollection<Employee> _employees = new ObservableCollection<Employee>();
        public ReadOnlyObservableCollection<Employee> Employees { get; private set; }
        private EmployeeModel _empModel = new EmployeeModel();
        private ActionView _actionView = null;
        private Employee _selectedEmployee = null;

        public Command Add { get; private set; }
        public Command Change { get; private set; }
        public Command Delete { get; private set; }
        public Command LoadList { get; private set; }
        public Employee SelectedEmployee
        {
            get => _selectedEmployee;
            set
            {
                _selectedEmployee = value;
                OnPropertyChanged(nameof(SelectedEmployee));
                EmployeeModel.SelectedEmployee = SelectedEmployee;
            }

        }

        public EmployeeViewModel()
        {
            Employees = new ReadOnlyObservableCollection<Employee>(_employees);

            LoadList = new Command((_) => {
                _employees.Clear();
                List<Employee> temp = _empModel.GetEmployees();
                temp.ForEach(x => _employees.Add(x));

            }, null);

            Add = new Command((_) =>
            {
                _actionView = new ActionView(new AddEmployeeViewModel());
                _actionView.ShowDialog();
            }, null);

            Change = new Command((_) =>
            {
                _actionView = new ActionView(new UpdEmployeeViewModel());
                _actionView.ShowDialog();
                SelectedEmployee = null;
            }, (_) => SelectedEmployee != null);

            Delete = new Command((_) =>
            {
                MessageBoxResult result = MessageBox.Show($"Удалить работника с номером #{SelectedEmployee.Id}?",
                    "Удаление",
                    MessageBoxButton.OKCancel,
                    MessageBoxImage.Question);
                bool resDel = result == MessageBoxResult.OK ? _empModel.DeleteEmployee(SelectedEmployee.Id) : false;
                string title = "Удаление";
                if (resDel)
                {
                    MessageBox.Show("Удаление успешно выполнено", title, MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Удаление работника отменено", title, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                SelectedEmployee = null;

            }, (_) => SelectedEmployee != null);

        }
    }
}
