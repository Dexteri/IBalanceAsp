using IBalance.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBalance.Domain.Entities
{
    public class Counterparty
    {
        [Key]
        public int Id { get; set; }
        [StringLength(150)]
        public string Name { get; set; }
        [StringLength(50)]
        public string City { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public CounterpartyType CounterpartyType { get; set; }
        public virtual ICollection<Consignment> Consignments { get; set; }
        public virtual ICollection<CounterpartyToPhone> Phones { get; set; }
    }
}
