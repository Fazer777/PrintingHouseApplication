using System;
using System.Windows;
using System.Linq;
using System.Windows.Threading;
using PrintingHouseApplication.Models;
using PrintingHouseApplication.MyCommand;
using PrintingHouseApplication.Views;

namespace PrintingHouseApplication.ViewModels
{
    class LoginViewModel : BaseViewModel
    {
        private string _login = string.Empty;
        private string _password = string.Empty;
        private bool _isAccess = false;
        private LoginModel _loginModel = new LoginModel();
        private bool _isError = false;
        private string _captchaInput = string.Empty;
        private Captcha _captcha;
        private DispatcherTimer dt = new DispatcherTimer();
        private int _timerValue = 0;
        private int _timeLock = 10;
        private bool _isLock = false;

        public bool ShowMaskPass { get; set; } = true;
        public bool ShowUnmaskPass { get; set; } = false;

        public string CaptchaInput
        {
            get => _captchaInput;
            set
            {
                _captchaInput = value;
                OnPropertyChanged(nameof(CaptchaInput));
            }
        }

        public string Captcha { get; set; }

        public bool IsError
        {
            get => _isError;
            set
            {
                _isError = value;
                OnPropertyChanged(nameof(IsError));
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
            private get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

       
        public Command SignIn { get; private set; }

        public int TimerValue
        {
            get => _timerValue;
            set
            {
                _timerValue = value;
                OnPropertyChanged(nameof(TimerValue));
            }
        }
        public LoginViewModel()
        {
            dt.Interval = TimeSpan.FromSeconds(1);
            dt.Tick += Dt_Tick;
            SignIn = new Command((_) =>
            {
                if (IsError)
                {
                    if (!_loginModel.CheckCaptcha(Captcha, CaptchaInput))
                    {
                        MessageBox.Show("Captcha введена неверно\n Вход в систему заблокирован на 10 секунд", "Ошибка входа", MessageBoxButton.OK, icon: MessageBoxImage.Error);
                        CaptchaInput = string.Empty;
                        IsError = true;
                        _isLock = true;
                        dt.Start();
                        return;
                    }
                }
                _isAccess = _loginModel.GetUser(Login, Password);
                GetAccess();
            }, (_) => (Login != string.Empty && Password != string.Empty && !(_isLock)));
        }

        private void Dt_Tick(object sender, EventArgs e)
        {
            TimerValue++;
            if (TimerValue > _timeLock)
            {
                dt.Stop();
                TimerValue = 0;
                _isLock = false;
                MessageBox.Show("Вход в систему доступен", "Уведомление", MessageBoxButton.OK, icon: MessageBoxImage.Information);
                return;
            }
        }

        private void GetAccess()
        {
            if (_isAccess)
            {
                IsError = false;
                MessageBox.Show($"Добро пожаловать!", "Успешный вход", button: MessageBoxButton.OK, icon: MessageBoxImage.Information);
                Application.Current.Windows.OfType<LoginView>().SingleOrDefault(x => x.IsVisible).Hide();
                _loginModel.OpenUserView();
            }
            else
            {
                MessageBox.Show("Логин или пароль введены неверно", "Ошибка входа", button: MessageBoxButton.OK, icon: MessageBoxImage.Error);
                IsError = true;
                _captcha = new Captcha();
                Captcha = _captcha.Generate(6);
                OnPropertyChanged(nameof(Captcha));
            }
        }

        public void VisiblePasswordOn()
        {
            ChangeVisiblePassword(true);
        }

        public void VisiblePasswordOff()
        {
            ChangeVisiblePassword(false);
        }

        private void ChangeVisiblePassword(bool state)
        {
            ShowMaskPass = !state;
            OnPropertyChanged(nameof(ShowMaskPass));
            ShowUnmaskPass = state;
            OnPropertyChanged(nameof(ShowUnmaskPass));
        }

        public void SetPassword(string password) => Password = password;
    }
}
