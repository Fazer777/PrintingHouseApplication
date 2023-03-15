using System;
using System.Data.SqlClient;


namespace PrintingHouseApplication.Models
{
    internal class AddUserModel
    {
        

        public static bool CreateUser(int userId, int roleId, string login, string password, byte[] image)
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
                    db.Database.ExecuteSqlCommand("EXEC AddUser @id, @roleId, @login, @password, @image",
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
