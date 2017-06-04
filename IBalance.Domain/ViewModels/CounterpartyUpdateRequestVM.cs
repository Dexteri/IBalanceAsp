using IBalance.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBalance.Domain.ViewModels
{
    public class CounterpartyUpdateRequestVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public CounterpartyType Type { get; set; }
        public List<string> Phones { get; set; }
    }
}
