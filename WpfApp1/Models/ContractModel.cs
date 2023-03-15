using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace PrintingHouseApplication.Models
{
    internal class ContractModel
    {
        public static Contract SelectedContract { get; set; }
        public static List<Contract> GetContracts()
        {
            List<Contract> contracts = null;
            using (DataContext db = new DataContext())
            {
                contracts = db.Contracts
                    .Where(c => !c.IsDeleted)
                    .Include(e => e.Employee)
                    .Include(a => a.Address)
                    .Include(o => o.Orders.Select(x => x.Product))
                    .Include(j => j.Orders.Select(x => x.Contract))                    
                    .ToList();
            }
            return contracts;
        }

        public static bool DeleteContract (int id)
        {
            var contractId = new SqlParameter("@id", id);
            try
            {
                using (DataContext db = new DataContext())
                {
                    db.Database.ExecuteSqlCommand("EXEC DeleteContract @id", contractId);
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
