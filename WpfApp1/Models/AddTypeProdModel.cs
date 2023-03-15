using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace PrintingHouseApplication.Models
{
    internal class AddTypeProdModel
    {
        public static bool CreateTypeProd(int tId, string tName)
        {
            var id = new SqlParameter("@id", tId);
            var name = new SqlParameter("@name", tName);
            using (DataContext db = new DataContext())
            {
                try
                {
                    db.Database.ExecuteSqlCommand("EXEC AddTypeProd @id, @name", id, name);
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
