using System;
using System.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using PrintingHouseApplication.Views;
using PrintingHouseApplication.MyCommand;
using PrintingHouseApplication.Models;

namespace PrintingHouseApplication.ViewModels
{
    internal class ContractViewModel: BaseViewModel
    {
        private ObservableCollection<Contract> _contracts = new ObservableCollection<Contract>();
        private Contract _selectedContract;
        private ActionView _actionView; 
        public ReadOnlyObservableCollection<Contract> Contracts { get; private set; }

        public Command Add { get; private set; }
        public Command Change { get; private set; }
        public Command Delete { get; private set; }
        public Command LoadList { get; private set; }
        public Contract SelectedContract
        {
            get => _selectedContract; 
            set
            {
                _selectedContract = value;
                OnPropertyChanged(nameof(SelectedContract));
                ContractModel.SelectedContract = SelectedContract;
            }
        }

        public ContractViewModel()
        {
            Contracts = new ReadOnlyObservableCollection<Contract>(_contracts);
            LoadList = new Command((_) =>
            {
                _contracts.Clear();
                List<Contract> contracts = ContractModel.GetContracts();
                contracts.ForEach(x => _contracts.Add(x));
            }, null);

            Add = new Command((_) =>
            {
                _actionView = new ActionView(new AddContractViewModel());
                _actionView.ShowDialog();
            }, null);

            Change = new Command((_) => {
                _actionView = new ActionView(new UpdContractViewModel());
                _actionView.ShowDialog();
                SelectedContract = null;
            }, (_)=> SelectedContract!=null);

            Delete = new Command((_) => {
                MessageBoxResult result = MessageBox.Show($"Удалить договор с номером #{SelectedContract.Id}?",
                    "Удаление",
                    MessageBoxButton.OKCancel,
                    MessageBoxImage.Question);
                bool resDel = result == MessageBoxResult.OK ? ContractModel.DeleteContract(SelectedContract.Id): false;
                string title = "Удаление";
                if (resDel)
                {
                    MessageBox.Show("Удаление успешно выполнено", title, MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Удаление договора отменено", title, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                SelectedContract = null;
            }, null);
        }
        
    }
}
