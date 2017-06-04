using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBalance.Domain.ViewModels
{
    public class GenerationRequestVM
    {
        public List<CounterpartyGenerationRequestVM> Counterparties { get; set; }
        public List<ProductGenerationRequestVM> Products { get; set; }
    }
}
