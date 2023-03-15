using System;
using System.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using PrintingHouseApplication.Models;
using PrintingHouseApplication.MyCommand;
using PrintingHouseApplication.Views;

namespace PrintingHouseApplication.ViewModels
{
    class ProductViewModel : BaseViewModel
    {
        private ObservableCollection<Product> _products = new ObservableCollection<Product>();
        public ReadOnlyObservableCollection<Product> Products { get; private set; }
        private Product _selectedProduct;
        private ActionView _actionView = null;


        public Command LoadList { get; private set; }
        public Command Add { get; private set; }
        public Command Change { get; private set; }
        public Command Delete { get; private set; }
        public Product SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));
                ProductModel.SelectedProduct = SelectedProduct;
            }
        }

        public ProductViewModel()
        {
            Products = new ReadOnlyObservableCollection<Product>(_products);

            LoadList = new Command((_)=> {
                _products.Clear();
                List<Product> prods = ProductModel.GetProducts();
                prods.ForEach(x=> _products.Add(x));
            }, null);

            Add = new Command((_)=> {
                _actionView = new ActionView(new AddProductViewModel());
                _actionView.ShowDialog();
            }, null);

            Change = new Command((_) =>
            {
                _actionView = new ActionView(new UpdProductViewModel());
                _actionView.ShowDialog();
                SelectedProduct = null;
            }, (_) => SelectedProduct != null);

            Delete = new Command((_) => {
                MessageBoxResult result = MessageBox.Show($"Удалить продукцию с номером #{SelectedProduct.Id}?",
                    "Удаление",
                    MessageBoxButton.OKCancel,
                    MessageBoxImage.Question);
                bool resDel = result == MessageBoxResult.OK ? ProductModel.DeleteProduct(SelectedProduct.Id) : false;
                string title = "Удаление";
                if (resDel)
                {
                    MessageBox.Show("Удаление успешно выполнено", title, MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Удаление цеха отменено", title, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                SelectedProduct = null;
            }, (_)=> SelectedProduct!=null );
        }
    }
}
