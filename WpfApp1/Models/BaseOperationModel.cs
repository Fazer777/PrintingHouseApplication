using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintingHouseApplication.Models
{
    class BaseOperationModel
    {
        public List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();
            using (DataContext db = new DataContext())
            {
                employees = db.Employees.Where(x => !x.IsDeleted && x.User == null).Include(e => e.User).ToList();
            }
            return employees;
        }
    }
}
