using IBalance.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBalance.Domain.Entities
{
    public class Consignment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int CounterpartyId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [StringLength(25)]
        public string SerialKey { get; set; }
        [DataType(DataType.Date)]
        public DateTime ConsignmentDate { get; set; }
        [StringLength(50)]
        public string ConsignmentNumber { get; set; }
        [ForeignKey("CounterpartyId")]
        public virtual Counterparty Counterparty { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        public Consignment()
        {
            ConsignmentDate = DateTime.Now.Date;
        }
    }
}
