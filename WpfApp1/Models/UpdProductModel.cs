using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintingHouseApplication.Models
{
    class UpdProductModel
    {
        public static bool UpdateProduct(int prodId, int departId, string prodName, decimal prodPricePerPiece)
        {
            var id = new SqlParameter("@id", prodId);
            var depId = new SqlParameter("@departmentId", departId);
            var name = new SqlParameter("@name", prodName);
            var pricePerPiece = new SqlParameter("@pricePerPiece", prodPricePerPiece);
            using (DataContext db = new DataContext())
            {
                try
                {
                    db.Database.ExecuteSqlCommand("EXEC UpdateProduct @id, @departmentId, @name, @pricePerPiece", id, depId, name, pricePerPiece);
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
