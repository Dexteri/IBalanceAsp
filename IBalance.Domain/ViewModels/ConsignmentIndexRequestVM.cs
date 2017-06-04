using IBalance.Domain.Entities;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBalance.Domain.ViewModels
{
    public class ConsignmentIndexRequestVM
    {
        public IPagedList<Consignment> Consignments { get; set; }
        public List<string> CounterpartiesNames { get; set; }
        public List<string> ConsignmentNumbers { get; set; }
    }
}
