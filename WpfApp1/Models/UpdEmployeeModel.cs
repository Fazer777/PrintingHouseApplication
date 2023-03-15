using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintingHouseApplication.Models
{
    class UpdEmployeeModel
    {
        public bool UpdateEmployee(Employee emp)
        {
            using (DataContext db = new DataContext())
            {
                var id = new SqlParameter("@id", emp.Id);
                var surname = new SqlParameter("@surname", emp.Surname);
                var name = new SqlParameter("@name", emp.Name);
                var patronymic = new SqlParameter("@patronymic", emp.Patronymic);
                var phone = new SqlParameter("@phone", emp.Phone);

                try
                {
                    db.Database.ExecuteSqlCommand("EXEC UpdateEmployee @id, @surname, @name, @patronymic, @phone",
                        id, surname, name, patronymic, phone);
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
