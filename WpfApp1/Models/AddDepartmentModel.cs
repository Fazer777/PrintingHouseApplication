using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintingHouseApplication.Models
{
    class AddDepartmentModel : BaseOperationModel
    {
        public bool CreateDepartment(Department dep)
        {
            var id = new SqlParameter("@id", dep.Id);
            var name = new SqlParameter("@name", dep.Name);
            var chief = new SqlParameter("@chief", dep.Chief);
            var phone = new SqlParameter("@phone", dep.Phone);
            using (DataContext db = new DataContext())
            {
                try
                {
                    db.Database.ExecuteSqlCommand("EXEC AddDepartment @id, @name, @chief, @phone",
                                                                   id, name, chief, phone);
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
