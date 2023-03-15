using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintingHouseApplication.Models
{
    class UserHelper
    {
        private static User _user;

        public static bool SetUser(User user)
        {
            _user = user;
            return _user != null;
        }

        public static Employee GetEmployee()
        {
            Employee emp = null;

            using (DataContext db = new DataContext())
            {
                emp = db.Employees.SingleOrDefault(e => e.Id == _user.Id && !e.IsDeleted);
            }

            return emp;
        }

        public static string GetUserRole()
        {
            return _user != null ? _user.Role.Name : string.Empty;
        }

        public static byte[] GetImageData()
        {
            return _user != null ? _user.Image : null;
        }
    }
}
