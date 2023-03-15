using System.Windows;
using System.Collections.Generic;
using System.Linq;
using PrintingHouseApplication.Models;
using PrintingHouseApplication.MyCommand;
using PrintingHouseApplication.Views;

namespace PrintingHouseApplication.ViewModels
{
    class UpdProductViewModel : BaseViewModel
    {
        private int _prodId;
        private string _prodName;
        private List<Department> _departs;
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


        public List<Department> Departs
        {
            get => _departs;
            set
            {
                _departs = value;
                OnPropertyChanged(nameof(Departs));
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

        public Command Update { get; private set; }
        public Command Cancel { get; private set; }

        public UpdProductViewModel()
        {
            Departs = ProductModel.GetDepartments();
            _prodId = ProductModel.SelectedProduct.Id;
            ProdName = ProductModel.SelectedProduct.Name;
            ProdPricePerPiece = ProductModel.SelectedProduct.PricePerPiece;
            SelectedDepartment = ProductModel.SelectedProduct.Department;

            Update = new Command((_)=> {
                if (UpdProductModel.UpdateProduct(_prodId, SelectedDepartment.Id, ProdName, ProdPricePerPiece))
                {
                    MessageBox.Show($"Изменение успешно выполнено", "Изменение", MessageBoxButton.OK, MessageBoxImage.Information);
                    Application.Current.Windows.OfType<ActionView>().SingleOrDefault(x => x.IsActive).Close();
                }
                else
                {
                    MessageBox.Show($"Ошибка изменения.\nИзменение отменено", "Изменение", MessageBoxButton.OK, MessageBoxImage.Error);
                }               
            }, null);

            Cancel = new Command((_)=> {
                Application.Current.Windows.OfType<ActionView>().SingleOrDefault(x => x.IsActive).Close();
            }, null);
        }
    }
}
