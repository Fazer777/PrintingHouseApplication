using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using PrintingHouseApplication.Models;
using PrintingHouseApplication.MyCommand;
using PrintingHouseApplication.Views;

namespace PrintingHouseApplication.ViewModels
{
    internal class UpdContractViewModel : BaseViewModel
    {
        private Employee _currentEmployee;
        private string _customer;
        private DateTime _registrDate;
        private DateTime _completionDate;
        private string _city;
        private string _street;
        private string _house;
        private List<Product> _products;
        private int _amount;
        private Product _selectedProduct;
        private List<ProdsInContracts> _prodsInContracts;


        private ObservableCollection<ProdsInContracts> _prods = new ObservableCollection<ProdsInContracts>();
        public ReadOnlyObservableCollection<ProdsInContracts> Prods { get; private set; }

        public Command AddProduct { get; private set; }
        public Command DelProduct { get; private set; }


        public Employee CurrentEmployee
        {
            get => _currentEmployee;
            set
            {
                _currentEmployee = value;
                OnPropertyChanged(nameof(CurrentEmployee));
            }
        }

        public string Customer
        {
            get => _customer;
            set
            {
                _customer = value;
                OnPropertyChanged(nameof(Customer));
            }
        }

        public DateTime RegistrDate
        {
            get => _registrDate.Date;
            set
            {
                _registrDate = value;
                OnPropertyChanged(nameof(RegistrDate));
            }
        }

        private string _textDate;
        public string TextDate
        {
            get => _textDate;
            set
            {
                _textDate = value;
                OnPropertyChanged(nameof(TextDate));
                if (_textDate != string.Empty)
                {
                    _completionDate = DateTime.Parse(_textDate);
                }

            }
        }
      
        public string City
        {
            get => _city; set
            {
                _city = value;
                OnPropertyChanged(nameof(City));
            }
        }
        public string Street
        {
            get => _street; set
            {
                _street = value;
                OnPropertyChanged(Street);
            }
        }
        public string House
        {
            get => _house; set
            {
                _house = value;
                OnPropertyChanged(nameof(House));
            }
        }

        public List<Product> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged(nameof(Products));
            }
        }
        
        public int Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                OnPropertyChanged(nameof(Amount));
            }
        }

        public Product SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));
            }
        }

        public Command Update { get; private set; }
        public Command Cancel { get; private set; }

        public ProdsInContracts SelectedInTable { get; set; }


        public UpdContractViewModel()
        {
            Prods = new ReadOnlyObservableCollection<ProdsInContracts>(_prods);
            _prodsInContracts = UpdContractModel.GetProductsInContract(ContractModel.SelectedContract.Id);
            _prodsInContracts.ForEach(x => _prods.Add(x));

            
            Products = AddContractModel.GetProducts();
            CurrentEmployee = ContractModel.SelectedContract.Employee;
            
            RegistrDate = ContractModel.SelectedContract.RegistrDate;
            TextDate = ContractModel.SelectedContract.CompletionDate.ToShortDateString();
            Customer = ContractModel.SelectedContract.CustomerName;
            City = ContractModel.SelectedContract.Address.City;
            Street = ContractModel.SelectedContract.Address.Street;
            House = ContractModel.SelectedContract.Address.House;
            Update = new Command((_) =>
            {
                Address address = new Address()
                {
                    City = this.City,
                    Street = this.Street,
                    House = this.House
                };

                if (UpdContractModel.UpdateContract(ContractModel.SelectedContract.Id, ContractModel.SelectedContract.Employee, Customer, address, RegistrDate, _completionDate))
                {
                    MessageBox.Show("Изменение успешно выполнено", "Изменение", MessageBoxButton.OK, MessageBoxImage.Information);
                    Application.Current.Windows.OfType<ActionView>().FirstOrDefault(x => x.IsActive).Close();
                }
                else
                {
                    MessageBox.Show("Ошибка изменения.\nИзменение отменено", "Изменение", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }, null);

            Cancel = new Command((_) =>
            {
                Application.Current.Windows.OfType<ActionView>().FirstOrDefault(x => x.IsActive).Close();
            }, null);
        }
    }
}
