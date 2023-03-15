using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;

namespace PrintingHouseApplication.Models
{
    class EmployeeModel
    {
        public static Employee SelectedEmployee { get; set; }

        public List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();
            using (DataContext db = new DataContext())
            {
                employees = db.Employees.Where(e => !e.IsDeleted).ToList();
            }
            return employees;
        }

        public bool DeleteEmployee(int empId)
        {
            using (DataContext db = new DataContext())
            {
                var id = new SqlParameter("@id", empId);
                try
                {
                    db.Database.ExecuteSqlCommand("EXEC DeleteEmployee @id", id);
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
