using System;
using System.Data.Entity;
using System.Linq;
using PrintingHouseApplication.Views;

namespace PrintingHouseApplication.Models
{
    class LoginModel
    {
        public bool GetUser(string login, string password)
        {
            if (login != string.Empty && password != string.Empty)
            {
                login = login.Trim(' ');
                password = password.Trim(' ');
                try
                {
                    using (DataContext db = new DataContext())
                    {
                        User user = db.Users.Where(u => u.Login == login && u.Password == password && !u.IsDeleted).Include(u => u.Role).FirstOrDefault();
                        return UserHelper.SetUser(user);
                    }
                }
                catch (Exception)
                {
                    return false;
                }
               
            }
            else
            {
                return false;
            }
        }

        public bool CheckCaptcha(string captcha, string userInput)
        {
            return userInput == captcha;
        }

        public void OpenUserView()
        {

            switch (UserHelper.GetUserRole())
            {
                case "Директор":
                    {
                        DirectorView directorView = new DirectorView();
                        directorView.Show();
                    }
                    break;
                case "Администратор":
                    {
                        AdminView adminView = new AdminView();
                        adminView.Show();
                    }
                    break;
                case "Менеджер":
                    {
                        ManagerView managerView = new ManagerView();
                        managerView.Show();
                    }
                    break;
                case "Бухгалтер":
                    {
                        AccountantView accountantView = new AccountantView();
                        accountantView.Show();
                    }
                    break;
                default:
                    break;
            }

        }
    }
}
