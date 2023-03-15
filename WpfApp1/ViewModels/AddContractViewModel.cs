using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrintingHouseApplication.Models;
using PrintingHouseApplication.MyCommand;
using PrintingHouseApplication.Views;
using System.Collections.ObjectModel;
using System.Data;

namespace PrintingHouseApplication.ViewModels
{
    internal class AddContractViewModel : BaseViewModel
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

        public Command Add { get; private set; }
        public Command Cancel { get; private set; }
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

        public ProdsInContracts SelectedInTable { get; set; }

        public AddContractViewModel()
        {
            Prods = new ReadOnlyObservableCollection<ProdsInContracts>(_prods);
            Products = AddContractModel.GetProducts();
            CurrentEmployee = UserHelper.GetEmployee();
            RegistrDate = DateTime.Now;
            TextDate = DateTime.Now.ToShortDateString();

            AddProduct = new Command((_) =>
            {
                ProdsInContracts test = new ProdsInContracts() { Product = SelectedProduct, Amount = this.Amount };
                if (_prods.Any(x => x.Product.Name == test.Product.Name))
                {
                    MessageBox.Show("Данная продукция уже выбрана", "Добавление продукции", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                _prods.Add(test);
            }, (_) => SelectedProduct != null && Amount > 0);

            DelProduct = new Command((_) =>
            {
                _prods.Remove(SelectedInTable);
            }, (_)=> SelectedInTable != null) ;

            Add = new Command((_) =>
            {
                Contract contract = new Contract()
                {
                    EmployeeId = CurrentEmployee.Id,
                    CustomerName = Customer,
                    AddressId = 0,
                    RegistrDate = this.RegistrDate,
                    CompletionDate = this._completionDate
                };

                if (AddContractModel.CreareContract(contract, _prods.ToList(), Amount, City, Street, House))
                {
                    MessageBox.Show("Добавление успешно выполнено", "Добавление", MessageBoxButton.OK, MessageBoxImage.Information);
                    Application.Current.Windows.OfType<ActionView>().FirstOrDefault(x => x.IsActive).Close();
                }
                else
                {
                    MessageBox.Show("Ошибка добавления.\nДобавление отменено", "Добавление", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }, (_)=> _prods.Count!=0);

            Cancel = new Command((_) =>
            {
                Application.Current.Windows.OfType<ActionView>().FirstOrDefault(x => x.IsActive).Close();
            }, null);
        }

    }
}
