using IBalance.Domain.Abstract;
using System;
using System.Linq;
using IBalance.Domain.Entities;
using System.Collections.Generic;
using IBalance.Domain.ViewModels;

namespace IBalance.Domain.Concrete
{
    public class ConsignmentRepository : IConsignmentRepository
    {
        public List<Consignment> GetConsignments()
        {
            using(EFDbContext context = new EFDbContext())
            {
                try
                {
                    return context.Consignments.Include("Counterparty").Include("Product").OrderByDescending(c => c.ConsignmentDate).ToList();
                }
                catch
                {
                    throw new Exception("Problems with connection to database");
                }
            }
        }

        public List<Consignment> GetConsignmentsByCounterpartyName(string counterpartyName)
        {
            using (EFDbContext context = new EFDbContext())
            {
                try
                {
                    return context.Consignments.Include("Counterparty").Include("Product").Where(c => c.Counterparty.Name == counterpartyName)
                                               .OrderByDescending(c => c.ConsignmentDate).ToList();
                }
                catch
                {
                    throw new Exception("Problems with connection to database");
                }
            }
        }

        public List<Consignment> GetConsignmentsByDate(DateTime date)
        {
            using (EFDbContext context = new EFDbContext())
            {
                try
                {
                    return context.Consignments.Include("Counterparty").Include("Product").Where(c => c.ConsignmentDate == date).ToList();
                }
                catch
                {
                    throw new Exception("Problems with connection to database");
                }
            }
        }

        public List<Consignment> GetConsignmentsByConsignmentNumber(string consignmentNumber)
        {
            using (EFDbContext context = new EFDbContext())
            {
                try
                {
                    return context.Consignments.Include("Counterparty").Include("Product").Where(c => c.ConsignmentNumber == consignmentNumber).ToList();
                }
                catch
                {
                    throw new Exception("Problems with connection to database");
                }
            }
        }

        public List<Consignment> GetConsignmentsWithId(List<int> idList)
        {
            using (EFDbContext context = new EFDbContext())
            {
                try
                {
                    return context.Consignments.Include("Counterparty").Include("Product").Where(c => idList.Contains(c.Id)).ToList();
                }
                catch
                {
                    throw new Exception("Problems with connection to database");
                }
            }
        }

        public List<string> GetConsignmentNumbers()
        {
            using (EFDbContext context = new EFDbContext())
            {
                try
                {
                    return context.Consignments.Select(c => c.ConsignmentNumber).Distinct().ToList();
                }
                catch
                {
                    throw new Exception("Problems with connection to database");
                }
            }
        }

        public Consignment GetConsignmentBySerialKey(string serialKey)
        {
            using (EFDbContext context = new EFDbContext())
            {
                try
                {
                    return context.Consignments.Include("Product").SingleOrDefault(c => c.SerialKey == serialKey);
                }
                catch
                {
                    throw new Exception("Problems with connection to database");
                }
            }
        }

        public void SaveConsignment(Consignment consignment)
        {
            using (EFDbContext context = new EFDbContext())
            {
                context.Consignments.Add(consignment);
                EFDbContext.SaveChanges(context);
            }
        }

        public void DeleteConsignment(int consignmentId)
        {
            using (EFDbContext context = new EFDbContext())
            {
                context.Consignments.Remove(context.Consignments.Single(c => c.Id == consignmentId));
                EFDbContext.SaveChanges(context);
            }
        }

        public void DeleteConsignments(List<int> idList)
        {
            using (EFDbContext context = new EFDbContext())
            {
                context.Consignments.RemoveRange(context.Consignments.Where(c => idList.Contains(c.Id)));
                EFDbContext.SaveChanges(context);
            }
        }

        public void DeleteAllConsignments()
        {
            using (EFDbContext context = new EFDbContext())
            {
                context.Consignments.RemoveRange(context.Consignments);
                EFDbContext.SaveChanges(context);
            }
        }

        public List<Consignment> GetConsignmentByConsignmentNumber(string consignmentNumber)
        {
            using (EFDbContext context = new EFDbContext())
            {
                return context.Consignments.Include("Counterparty").Include("Product").Where(c => c.ConsignmentNumber == consignmentNumber).ToList();
            }
        }
    }
}
