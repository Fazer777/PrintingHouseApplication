using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using PrintingHouseApplication.Models;
using PrintingHouseApplication.MyCommand;
using PrintingHouseApplication.Views;

namespace PrintingHouseApplication.ViewModels
{
    class AddProductViewModel : BaseViewModel
    {
        private string _prodName;
        private List<Department> _prods;
        private decimal _prodPricePerPiece;
        private Department _selectedDepartment;

        public Department SelectedDepartment
        {
            get => _selectedDepartment;
            set
            {
                _selectedDepartment = value;
                OnPropertyChanged(nameof(SelectedDepartment));
            }
        }


        public decimal ProdPricePerPiece
        {
            get => _prodPricePerPiece;
            set
            {
                _prodPricePerPiece = value;
                OnPropertyChanged(nameof(ProdPricePerPiece));
            }
        }


        public List<Department> Prods
        {
            get => _prods;
            set
            {
                _prods = value;
                OnPropertyChanged(nameof(Prods));
            }
        }


        public string ProdName
        {
            get => _prodName;
            set
            {
                _prodName = value;
                OnPropertyChanged(nameof(ProdName));
            }
        }

        public Command Add { get; private set; }
        public Command Cancel { get; private set; }

        public AddProductViewModel()
        {
            Prods = ProductModel.GetDepartments();

            Add = new Command((_)=> {
                if (AddProductModel.CreateProduct(SelectedDepartment.Id, ProdName, ProdPricePerPiece))
                {
                    MessageBox.Show("Добавление успешно выполнено", "Добавление", MessageBoxButton.OK, MessageBoxImage.Information);
                    Application.Current.Windows.OfType<ActionView>().SingleOrDefault(x => x.IsActive).Close();
                }
                else
                {
                    MessageBox.Show("Ошибка добавления.\nДобавление отменено", "Добавление", MessageBoxButton.OK, MessageBoxImage.Error);
                }             
            }, null);

            Cancel = new Command((_) => {
                Application.Current.Windows.OfType<ActionView>().SingleOrDefault(x => x.IsActive).Close();
            }, null);
        }

    }
}
