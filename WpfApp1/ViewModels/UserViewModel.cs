using System.Collections.Generic;
using System.Collections.ObjectModel;
using PrintingHouseApplication.MyCommand;
using PrintingHouseApplication.Models;
using PrintingHouseApplication.Views;
using System.Windows;

namespace PrintingHouseApplication.ViewModels
{
    internal class UserViewModel : BaseViewModel
    {
        private ObservableCollection<User> _users = new ObservableCollection<User>();
        private User _selectedUser;
        private ActionView _actionView;
        public ReadOnlyObservableCollection<User> Users { get; private set; }

        public Command LoadList { get; private set; }
        public Command Add { get; private set; }
        public Command Change { get; private set; }
        public Command Delete { get; private set; }

        public User SelectedUser
        {
            get => _selectedUser; 
            set
            {
                _selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
                UserModel.SelectedUser = SelectedUser;
            }
        }

        public UserViewModel()
        {
            Users = new ReadOnlyObservableCollection<User>(_users);

            LoadList = new Command((_) =>
            {
                _users.Clear();
                List<User> users = UserModel.GetUsers();
                users.ForEach(x => _users.Add(x));
            }, null);

            Add = new Command((_) =>
            {
                _actionView = new ActionView(new AddUserViewModel());
                _actionView.ShowDialog();
            },null);

            Change = new Command((_) =>
            {
                _actionView = new ActionView(new UpdUserViewModel());
                _actionView.ShowDialog();
                SelectedUser = null;
            }, (_)=> SelectedUser !=null);

            Delete = new Command((_) => {
                MessageBoxResult result = MessageBox.Show($"Удалить пользователя с номером #{SelectedUser.Id}?",
                    "Удаление",
                    MessageBoxButton.OKCancel,
                    MessageBoxImage.Question);
                bool resDel = result == MessageBoxResult.OK ? UserModel.DeleteUser(SelectedUser.Id) : false;
                string title = "Удаление";
                if (resDel)
                {
                    MessageBox.Show("Удаление успешно выполнено", title, MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Удаление пользователя отменено", title, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                SelectedUser = null;
            }, (_) => SelectedUser != null);
        }
    }
}
