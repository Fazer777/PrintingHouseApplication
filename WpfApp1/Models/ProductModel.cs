using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintingHouseApplication.Models
{
    class ProductModel
    {
        public static Product SelectedProduct { get; set; }
        public static List<Product> GetProducts()
        {
            List<Product> products = null;
            using (DataContext db = new DataContext())
            {
               products =  db.Products.Where(p => !p.IsDeleted).Include(x => x.Department).ToList();
            }
            return products;
        }

        public static List<Department> GetDepartments()
        {
            List<Department> departments = null;
            using (DataContext db = new DataContext())
            {
                departments = db.Departments.Where(d => !d.IsDeleted).Include(x => x.TypeProd).ToList();
            }
            return departments;
        }

        public static bool DeleteProduct(int prodId)
        {
            var id = new SqlParameter("@id", prodId);

            try
            {
                using (DataContext db = new DataContext())
                {
                    db.Database.ExecuteSqlCommand("EXEC DeleteProduct @id", id);
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
