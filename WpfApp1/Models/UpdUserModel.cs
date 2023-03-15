using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintingHouseApplication.Models
{
    internal class UpdUserModel
    {
        public static bool UpdateUser(int userId, int roleId, string login, string password, byte[] image)
        {
            var pUserId = new SqlParameter("@id", userId);
            var pRoleId = new SqlParameter("@roleId", roleId);
            var pLogin = new SqlParameter("@login", login);
            var pPassword = new SqlParameter("@password", password);
            var pImage = new SqlParameter("@image", image);
            try
            {
                using (DataContext db = new DataContext())
                {
                    db.Database.ExecuteSqlCommand("EXEC UpdateUser @id, @roleId, @login, @password, @image",
                                                                pUserId, pRoleId, pLogin, pPassword, pImage);
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
