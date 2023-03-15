using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using PrintingHouseApplication.Models;
using PrintingHouseApplication.MyCommand;
using PrintingHouseApplication.Views;

namespace PrintingHouseApplication.ViewModels
{
    class AddEmployeeViewModel : BaseViewModel
    {
        private string _surname;
        private string _name;
        private string _patronymic;
        private string _phone;
        private AddEmployeeModel _addEmployeeModel = new AddEmployeeModel();
        public Command Add { get; private set; }
        public Command Cancel { get; private set; }
        public string Surname
        {
            get => _surname;
            set
            {
                _surname = value;
                OnPropertyChanged(nameof(Surname));
            }
        }
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string Patronymic
        {
            get => _patronymic;
            set
            {
                _patronymic = value;
                OnPropertyChanged(nameof(Patronymic));
            }
        }
        public string Phone
        {
            get => _phone;
            set
            {
                _phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }

        public AddEmployeeViewModel()
        {
            Add = new Command((_) => {
                if ((_addEmployeeModel.CreateEmployee(Surname, Name, Patronymic, Phone)))
                {
                    MessageBox.Show("Новая запись успешно добавлена", "Добавление", MessageBoxButton.OK, MessageBoxImage.Information);
                    Application.Current.Windows.OfType<ActionView>().SingleOrDefault(x => x.IsActive).Close();
                    Surname = string.Empty;
                    Name = string.Empty;
                    Patronymic = string.Empty;
                    Phone = string.Empty;
                }
                else
                {
                    MessageBox.Show("Ошибка добавлении записи", "Добавление", MessageBoxButton.OK, MessageBoxImage.Error);
                }                          
            }, null);

            Cancel = new Command((_) => {
                Application.Current.Windows.OfType<ActionView>().SingleOrDefault(x => x.IsActive).Close();
            }, null);
        }
    }
}
