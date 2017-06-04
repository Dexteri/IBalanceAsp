using IBalance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBalance.Domain.ViewModels
{
    public class ConsignmentFilterResultVM
    {
        public List<Consignment> Consignments { get; set; }
        public List<string> CounterpartiesNames { get; set; }
        public List<string> ConsignmentNumbers { get; set; }
    }
}
