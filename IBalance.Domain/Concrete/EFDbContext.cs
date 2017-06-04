using IBalance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBalance.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        //public EFDbContext() : base("Data Source=sql6001.smarterasp.net;Persist Security Info=True;User ID=DB_A1EF76_ibalance_admin;Password=alikalik057") { }
        public DbSet<Consignment> Consignments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Counterparty> Counterparties { get; set; }
        public DbSet<CounterpartyToPhone> Phones { get; set; }

        public static void SaveChanges(EFDbContext context)
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateException saveChangesException)
            {
                throw new Exception("Save changes problems: ", saveChangesException);
            }
        }
    }
}
