using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintingHouseApplication.Models
{
    internal class UpdTypeProdModel
    {
        public static bool UpdateTypeProd(int tId, string tName)
        {
            var id = new SqlParameter("@id", tId);
            var name = new SqlParameter("@name", tName);
            using (DataContext db = new DataContext())
            {
                try
                {
                    db.Database.ExecuteSqlCommand("EXEC UpdateTypeProd @id, @name", id, name);
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
