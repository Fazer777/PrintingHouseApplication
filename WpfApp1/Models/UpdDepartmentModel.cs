using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

namespace PrintingHouseApplication.Models
{
    class UpdDepartmentModel : BaseOperationModel
    {
        public static bool UpdateDepartment(Department dep)
        {
            var id = new SqlParameter("@id", dep.Id);
            var name = new SqlParameter("@name", dep.Name);
            var chief = new SqlParameter("@chief", dep.Chief);
            var phone = new SqlParameter("@phone", dep.Phone);
            using (DataContext db = new DataContext())
            {
                try
                {
                    db.Database.ExecuteSqlCommand("EXEC UpdDepartment @id, @name, @chief, @phone", id, name, chief, phone);
                }
                catch (Exception)
                {
                    return false;
                }

            }
            return true;
        }

        public Employee GetNameEmployee(int id)
        {
            Employee employee = new Employee();
            using (DataContext db = new DataContext())
            {
                employee = db.Employees.Where(e => e.Id == id && !e.IsDeleted && e.User == null).Include(x => x.User).SingleOrDefault();
            }
            return employee;
        }
    }
}
