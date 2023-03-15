using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintingHouseApplication.Models
{
    internal class UpdContractModel
    {
        public static List<ProdsInContracts> GetProductsInContract(int contractId)
        {
            List<ProdsInContracts> prods = new List<ProdsInContracts>();
            List<Order> orders = new List<Order>();
            using (DataContext db = new DataContext())
            {
                orders = db.Orders.Where(o => o.ContractId == contractId).Include(x => x.Product).Include(x => x.Contract).ToList();
            }
            orders.ForEach(x => prods.Add(new ProdsInContracts()
            {
                Product = x.Product,
                Amount = x.ProductAmount
            }));
            return prods;
        }

        public static bool UpdateContract(int contractId, Employee emp, string customer, Address address, DateTime regDate, DateTime compDate)
        {
            using (DataContext db = new DataContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        Contract contract = db.Contracts.Where(x => x.Id == contractId && !x.IsDeleted)
                                                        .Include(x => x.Address)
                                                        .Include(x => x.Employee)
                                                        .SingleOrDefault();
                        contract.EmployeeId = emp.Id;
                        contract.CustomerName = customer;
                        contract.Address.City = address.City;
                        contract.Address.Street = address.Street;
                        contract.Address.House = address.House;
                        contract.RegistrDate = regDate;
                        contract.CompletionDate = compDate;
                        db.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }

            return true;

        }
    }
}
