using IBalance.Domain.Abstract;
using IBalance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBalance.Domain.Concrete
{
    public class CounterpartyRepository : ICounterpartyRepository
    {
        public List<Counterparty> GetCounterparties()
        {
            using (EFDbContext context = new EFDbContext())
            {
                try
                {
                    return context.Counterparties.Include("Phones").ToList();
                }
                catch
                {
                    throw new Exception("Problems with connection to database");
                }
            }
        }
        public List<string> GetCounterpartiesNames()
        {
            using (EFDbContext context = new EFDbContext())
            {
                try
                {
                    return context.Counterparties.Select(c => c.Name).ToList();
                }
                catch
                {
                    throw new Exception("Problems with connection to database");
                }
            }
        }
        public void SaveCounterparty(Counterparty counterparty)
        {
            using (EFDbContext context = new EFDbContext())
            {
                if (counterparty.Id == 0)
                {
                    context.Counterparties.Add(counterparty);
                }
                else
                {
                    Counterparty dbEntry = context.Counterparties.Find(counterparty.Id);
                    if (dbEntry != null)
                    {
                        dbEntry.Name = counterparty.Name;
                        dbEntry.City = counterparty.City;
                        dbEntry.Email = counterparty.Email;
                        dbEntry.Phones = counterparty.Phones;
                        dbEntry.CounterpartyType = counterparty.CounterpartyType;
                    }
                }
                EFDbContext.SaveChanges(context);
            }
        }
        public void DeleteCounterparty(int counterpartyId)
        {
            using (EFDbContext context = new EFDbContext())
            {
                context.Counterparties.Remove(context.Counterparties.Single(cp => cp.Id == counterpartyId));
                EFDbContext.SaveChanges(context);
            }
        }

        public void DeletePhones(int counterpartyId)
        {
            using (EFDbContext context = new EFDbContext())
            {
                context.Phones.RemoveRange(context.Phones.Where(p => p.CounterpartyId == counterpartyId));
                EFDbContext.SaveChanges(context);
            }
        }
    }
}
