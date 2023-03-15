using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintingHouseApplication.Models
{
    class AddProductModel
    {
        public static bool CreateProduct(int depId, string prodName, decimal prodPricePerPiece)
        {
            var departId = new SqlParameter("@departmentId", depId);
            var name = new SqlParameter("@name", prodName);
            var pricePerPiece = new SqlParameter("@pricePerPiece", prodPricePerPiece);
            using (DataContext db = new DataContext())
            {
                try
                {
                    db.Database.ExecuteSqlCommand("EXEC AddProduct @departmentId, @name, @pricePerPiece", departId, name, pricePerPiece);
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
