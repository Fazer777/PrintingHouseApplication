using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using PrintingHouseApplication.Models;
using PrintingHouseApplication.MyCommand;
using PrintingHouseApplication.Views;

namespace PrintingHouseApplication.ViewModels
{
    internal class TypeProdViewModel: BaseViewModel
    {
        private ObservableCollection<TypeProd> _typesProd = new ObservableCollection<TypeProd>();
        private TypeProd _selectedType;
        private ActionView _actionView;

        public ReadOnlyObservableCollection<TypeProd> TypesProd { get; private set; }

        public Command LoadList { get; private set; }
        public Command Add { get; private set; }
        public Command Change { get; private set; }
        public Command Delete { get; private set; }
        public TypeProd SelectedType
        {
            get => _selectedType; 
            set
            {
                _selectedType = value;
                OnPropertyChanged(nameof(SelectedType));
                TypesProdModel.SelectedTypeProd = SelectedType;
            }
        }

        public TypeProdViewModel()
        {
            TypesProd = new ReadOnlyObservableCollection<TypeProd>(_typesProd);

            LoadList = new Command((_) =>
            {
                _typesProd.Clear();
                List<TypeProd> types = TypesProdModel.GetTypeProds();
                types.ForEach(x => _typesProd.Add(x));
            }, null);

            Add = new Command((_) =>
            {
                _actionView = new ActionView(new AddTypeProdViewModel());
                _actionView.ShowDialog();
            }, null);

            Change = new Command((_) =>
            {
                _actionView = new ActionView(new UpdTypeProdViewModel());
                _actionView.ShowDialog();
                SelectedType = null;
            }, (_) => SelectedType != null);

            Delete = new Command((_) =>
            {
                MessageBoxResult result = MessageBox.Show($"Удалить вид продукции с номером #{SelectedType.Id}?",
                    "Удаление",
                    MessageBoxButton.OKCancel,
                    MessageBoxImage.Question);
                bool resDel = result == MessageBoxResult.OK ? TypesProdModel.DeleteTypeProd(SelectedType.Id) : false;
                string title = "Удаление";
                if (resDel)
                {
                    MessageBox.Show("Удаление успешно выполнено", title, MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {  }
                SelectedType = null;
            }, (_) => SelectedType != null);
        }
    }
}
