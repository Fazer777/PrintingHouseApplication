using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintingHouseApplication.Models
{
    internal class UserModel
    {
        public static User SelectedUser { get; set; }

        public static List<User> GetUsers()
        {
            List<User> users = new List<User>();
            using (DataContext db = new DataContext())
            {
                users = db.Users.Where(u => !u.IsDeleted).Include(x => x.Employee).Include(x => x.Role).ToList();
            }
            return users;
        }

        public static List<Role> GetRoles()
        {
            List<Role> roles = new List<Role>();
            using (DataContext db = new DataContext())
            {
                roles = db.Roles.Where(x => !x.IsDeleted).Include(x => x.Users).ToList();
            }
            return roles;
        }

        public static bool DeleteUser(int id)
        {
            var userId = new SqlParameter("@id", id); 
            try
            {
                using (DataContext db = new DataContext())
                {
                    db.Database.ExecuteSqlCommand("EXEC DeleteUser @id", userId);
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
