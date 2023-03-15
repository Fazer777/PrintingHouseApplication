using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

namespace PrintingHouseApplication.Models
{
    internal class AddContractModel
    {
        public static List<Product> GetProducts()
        {
            List<Product> products = null;
            using (DataContext db = new DataContext())
            {
                products = db.Products.Where(p => !p.IsDeleted).Include(x => x.Department).Include(x => x.Orders).ToList();
            }
            return products;
        }

        public static bool CreareContract(Contract contract, List<ProdsInContracts> prods, int amount, string city, string street, string house)
        {           
            using (DataContext db = new DataContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        Address address = new Address()
                        {
                            City = city,
                            Street = street,
                            House = house
                        };
                        db.Addresses.Add(address);
                        db.SaveChanges();
                      
                        contract.AddressId = address.Id;
                        db.Contracts.Add(contract);
                        db.SaveChanges();
                        int contractId = contract.Id;

                        foreach (var item in prods)
                        {
                            Order order = new Order()
                            {
                                ContractId = contractId,
                                ProductId = item.Product.Id,
                                ProductAmount = item.Amount
                            };
                            db.Orders.Add(order);
                            db.SaveChanges();
                        }
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
