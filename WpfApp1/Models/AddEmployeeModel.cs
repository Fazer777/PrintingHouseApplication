using System;
using System.Data.SqlClient;

namespace PrintingHouseApplication.Models
{
    class AddEmployeeModel
    {
        public bool CreateEmployee(string surname, string name, string patronymic, string phone)
        {
            using (DataContext db = new DataContext())
            {
                var empSurname = new SqlParameter("@surname", surname);
                var empName = new SqlParameter("@name", name);
                var empPatronymic = new SqlParameter("@patronymic", patronymic);
                var empPhone = new SqlParameter("@phone", phone);
                try
                {
                    db.Database.ExecuteSqlCommand("EXEC AddEmployee @surname, @name, @patronymic, @phone", empSurname, empName, empPatronymic, empPhone);
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
