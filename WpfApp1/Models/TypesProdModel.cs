using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintingHouseApplication.Models
{
    internal class TypesProdModel
    {
        public static TypeProd SelectedTypeProd { get; set; }

        public static List<TypeProd> GetTypeProds()
        {
            List<TypeProd> typeProds = null;
            using (DataContext db = new DataContext())
            {
                typeProds = db.TypeProds.Where(t => !t.IsDeleted).Include(x => x.Department).ToList();
            }
            return typeProds;
        }

        public static bool DeleteTypeProd(int tId)
        {
            var id = new SqlParameter("@id", tId);
            using (DataContext db = new DataContext())
            {
                try
                {
                    db.Database.ExecuteSqlCommand("EXEC DeleteTypeProd @id", id);
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
