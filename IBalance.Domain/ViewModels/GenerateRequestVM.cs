using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBalance.Domain.ViewModels
{
    public class GenerateRequestVM
    {
        public int ProductId { get; set; }
        public int CounterpartyId { get; set; }
        public int CodesNumber { get; set; }
        public string ConsignmentNumber { get; set; }
    }
}
