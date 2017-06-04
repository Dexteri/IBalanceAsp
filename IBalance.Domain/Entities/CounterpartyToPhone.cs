using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBalance.Domain.Entities
{
    public class CounterpartyToPhone
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int CounterpartyId { get; set; }
        [Attributes.Phone]
        public string Phone { get; set; }
        [ForeignKey("CounterpartyId")]
        public virtual Counterparty Counterparty { get; set; }
    }
}
