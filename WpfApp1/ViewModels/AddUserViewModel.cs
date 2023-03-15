using System;
using System.Windows;
using WinForms = System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using PrintingHouseApplication.MyCommand;
using PrintingHouseApplication.Models;
using PrintingHouseApplication.Views;
using System.IO;

namespace PrintingHouseApplication.ViewModels
{
    internal class AddUserViewModel : BaseViewModel
    {
        private int _userId;
        private List<Role> _roles;
        private string _login;
        private string _password;
        private Role _selectedRole;
        private byte[] _image;
        private WinForms.OpenFileDialog openFileDialog = new WinForms.OpenFileDialog();

        public int UserId
        {
            get => _userId; 
            set
            {
                _userId = value;
                OnPropertyChanged(nameof(UserId));
            }
        }
        public List<Role> Roles
        {
            get => _roles; 
            set
            {
                _roles = value;
                OnPropertyChanged(nameof(Roles));
            }
        }
        public string Login
        {
            get => _login; 
            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
            }
        }
        public string Password
        {
            get => _password; 
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));    
            }
        }
        public Role SelectedRole
        {
            get => _selectedRole; 
            set
            {
                _selectedRole = value;
                OnPropertyChanged(nameof(SelectedRole));
            }
        }

        public byte[] Image
        {
            get => _image; 
            set
            {
                _image = value;
                OnPropertyChanged(nameof(Image));
            }
        }

        public Command Add { get; private set; }
        public Command Cancel { get; private set; }

        public Command ChangePicture { get; private set; }
 

        public AddUserViewModel()
        {
            Roles = UserModel.GetRoles();

            Add = new Command((_) =>
            {
                if (AddUserModel.CreateUser(UserId, SelectedRole.Id, Login, Password, Image))
                {
                    MessageBox.Show("Добавление успешно выполнено", "Добавление", button: MessageBoxButton.OK, MessageBoxImage.Information);
                    Application.Current.Windows.OfType<ActionView>().FirstOrDefault(x => x.IsActive).Close();
                }
                else
                {
                    MessageBox.Show("Ошибка добавления.\nДобавление отменено.", "Добавление", button: MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }, null);

            Cancel = new Command((_) =>
            {
                Application.Current.Windows.OfType<ActionView>().FirstOrDefault(x => x.IsActive).Close();
            }, null);

            ChangePicture = new Command((_) =>
            {
                string fileName = string.Empty;
                openFileDialog.Filter = "Image files (*.jpg)|*.jpg|Image files (*.png)|*.png";
                if (openFileDialog.ShowDialog() == WinForms.DialogResult.OK)
                {
                    fileName = openFileDialog.FileName;
                }
                try
                {
                    Image = File.ReadAllBytes(fileName);
                }
                catch (Exception ex)
                {
                    Image = null;
                    MessageBox.Show(ex.Message, "Ошибка", button: MessageBoxButton.OK, icon: MessageBoxImage.Error);
                    return;
                }              
            }, null);
        }
    }
}
