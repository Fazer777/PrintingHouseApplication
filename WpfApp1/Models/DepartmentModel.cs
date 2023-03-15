using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintingHouseApplication.Models
{
    class DepartmentModel
    {
        public static Department SelectedDepartment { get; set; }
        public List<Department> GetDepartments()
        {
            List<Department> departments = new List<Department>();
            using (DataContext db = new DataContext())
            {
                departments = db.Departments.Where(x => !x.IsDeleted).Include(e => e.Employee).Include(t => t.TypeProd).ToList();
            }

            return departments;
        }

        public bool DeleteDepartment(int depId)
        {
            var id = new SqlParameter("@id", depId);
            using (DataContext db = new DataContext())
            {
                try
                {
                    db.Database.ExecuteSqlCommand("EXEC DelDepartment @id", id);

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
