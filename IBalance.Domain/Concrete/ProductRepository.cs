using IBalance.Domain.Abstract;
using IBalance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBalance.Domain.Concrete
{
    public class ProductRepository : IProductRepository
    {
        public List<Product> GetProducts()
        {
            using(EFDbContext context = new EFDbContext())
            {
                try
                {
                    return context.Products.ToList();
                }
                catch
                {
                    throw new Exception("Problems with connection to database");
                }
            }
        }

        public Product GetProduct(int id)
        {
            using (EFDbContext context = new EFDbContext())
            {
                try
                {
                    return context.Products.SingleOrDefault(p => p.Id == id);
                }
                catch
                {
                    throw new Exception("Problems with connection to database");
                }
            }
        }
        public void SaveProduct(Product product)
        {
            using (EFDbContext context = new EFDbContext())
            {
                if (product.Id == 0)
                {
                    context.Products.Add(product);
                }
                else
                {
                    Product dbEntry = context.Products.Find(product.Id);
                    if (dbEntry != null)
                    {
                        dbEntry.Model = product.Model;
                        dbEntry.ProductionDate = product.ProductionDate;
                        dbEntry.Colour = product.Colour;
                        dbEntry.WheelsDiameter = product.WheelsDiameter;
                        dbEntry.MotorPower = product.MotorPower;
                        dbEntry.BatteryManufacturer = product.BatteryManufacturer;
                        dbEntry.Amperes = product.Amperes;
                        dbEntry.Weight = product.Weight;
                        dbEntry.MaxSpeed = product.MaxSpeed;
                        dbEntry.PossibleMileage = product.PossibleMileage;
                        dbEntry.Motherboard = product.Motherboard;
                        dbEntry.Application = product.Application;
                        dbEntry.ImageUrl = product.ImageUrl;
                    }
                }
                EFDbContext.SaveChanges(context);
            }
        }
        public void DeleteProduct(int productId)
        {
            using (EFDbContext context = new EFDbContext())
            {
                context.Products.Remove(context.Products.Single(p => p.Id == productId));
                EFDbContext.SaveChanges(context);
            }
        }
    }
}
