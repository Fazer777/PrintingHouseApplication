using System;
using System.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using PrintingHouseApplication.Models;
using PrintingHouseApplication.Views;
using PrintingHouseApplication.MyCommand;

namespace PrintingHouseApplication.ViewModels
{
    class DepartmentViewModel : BaseViewModel
    {
        private ObservableCollection<Department> _departments = new ObservableCollection<Department>();
        public ReadOnlyObservableCollection<Department> Departments { get; private set; }
        private DepartmentModel _departmentModel = new DepartmentModel();
        private ActionView _actionView = null;
        private Department _selectedDepartment;

        public Command Add { get; private set; }
        public Command Change { get; private set; }
        public Command Delete { get; private set; }
        public Command LoadList { get; private set; }
        public Department SelectedDepartment
        {
            get => _selectedDepartment;
            set
            {
                _selectedDepartment = value;
                OnPropertyChanged(nameof(SelectedDepartment));
                DepartmentModel.SelectedDepartment = SelectedDepartment;
            }
        }

        public DepartmentViewModel()
        {
            Departments = new ReadOnlyObservableCollection<Department>(_departments);

            LoadList = new Command((_) => {
                _departments.Clear();
                List<Department> temp = _departmentModel.GetDepartments();
                temp.ForEach(x => _departments.Add(x));

            }, null);

            Add = new Command((_) =>
            {
                _actionView = new ActionView(new AddDepartmentViewModel());
                _actionView.ShowDialog();
            }, null);

            Change = new Command(_ =>
            {
                _actionView = new ActionView(new UpdDepartmentViewModel());
                _actionView.ShowDialog();
                SelectedDepartment = null;
            }, _ => SelectedDepartment != null);

            Delete = new Command((_) =>
            {
                MessageBoxResult result = MessageBox.Show($"Удалить цех с номером #{SelectedDepartment.Id}?",
                    "Удаление",
                    MessageBoxButton.OKCancel,
                    MessageBoxImage.Question);
                bool resDel = result == MessageBoxResult.OK ? _departmentModel.DeleteDepartment(SelectedDepartment.Id) : false;
                string title = "Удаление";
                if (resDel)
                {
                    MessageBox.Show("Удаление успешно выполнено", title, MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Удаление цеха отменено", title, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                SelectedDepartment = null;
            }, (_) => SelectedDepartment != null);
        }
    }
}
