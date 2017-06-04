using IBalance.Domain.Abstract;
using IBalance.Domain.Entities;
using IBalance.Domain.ViewModels;
using IBalance.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace IBalance.Web.Controllers.Api
{
    [RoutePrefix("api/check")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CheckController : ApiController
    {
        private IConsignmentRepository _consignmentRepository;

        public CheckController(IConsignmentRepository consignmentRepository)
        {
            _consignmentRepository = consignmentRepository;
        }

        [Route("get-product-info")]
        [HttpPost]
        public HttpResponseMessage GetProductInfo([FromBody] string serialKey)
        {
            try
            {
                if (serialKey != null)
                {
                    var consignment = _consignmentRepository.GetConsignmentBySerialKey(serialKey);
                    if(consignment != null)
                    {
                        var productVM = new ProductRequestVM()
                        {
                            Model = consignment.Product.Model,
                            ProductionDate = consignment.Product.ProductionDate,
                            Colour = consignment.Product.Colour,
                            WheelsDiameter = consignment.Product.WheelsDiameter,
                            MotorPower = consignment.Product.MotorPower,
                            BatteryManufacturer = consignment.Product.BatteryManufacturer,
                            Amperes = consignment.Product.Amperes,
                            Weight = consignment.Product.Weight,
                            MaxSpeed = consignment.Product.MaxSpeed,
                            PossibleMileage = consignment.Product.PossibleMileage,
                            Motherboard = consignment.Product.Motherboard,
                            Application = consignment.Product.Application,
                            ImageUrl = consignment.Product.ImageUrl
                        };
                        return Request.CreateResponse(HttpStatusCode.OK, productVM);
                    }
                    return Request.CreateResponse(HttpStatusCode.NoContent);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}
