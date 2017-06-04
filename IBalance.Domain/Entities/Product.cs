using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBalance.Domain.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Model { get; set; }
        [DataType(DataType.Date)]
        public DateTime ProductionDate { get; set; }
        [StringLength(200)]
        public string Colour { get; set; }
        [StringLength(20)]
        public string WheelsDiameter { get; set; }
        [StringLength(20)]
        public string MotorPower { get; set; }
        [StringLength(20)]
        public string BatteryManufacturer { get; set; }
        [StringLength(20)]
        public string Amperes { get; set; }
        [StringLength(20)]
        public string Weight { get; set; }
        [StringLength(20)]
        public string MaxSpeed { get; set; }
        [StringLength(20)]
        public string PossibleMileage { get; set; }
        [StringLength(20)]
        public string Motherboard { get; set; }
        [StringLength(50)]
        public string Application { get; set; }
        [StringLength(255)]
        public string ImageUrl { get; set; }

        public virtual ICollection<Consignment> Consignments { get; set; }
    }
}
