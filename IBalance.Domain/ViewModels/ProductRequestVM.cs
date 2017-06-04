using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBalance.Domain.ViewModels
{
    public class ProductRequestVM
    {
        public string Model { get; set; }
        public DateTime ProductionDate { get; set; }
        public string Colour { get; set; }
        public string WheelsDiameter { get; set; }
        public string MotorPower { get; set; }
        public string BatteryManufacturer { get; set; }
        public string Amperes { get; set; }
        public string Weight { get; set; }
        public string MaxSpeed { get; set; }
        public string PossibleMileage { get; set; }
        public string Motherboard { get; set; }
        public string Application { get; set; }
        public string ImageUrl { get; set; }
    }
}
