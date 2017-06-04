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
    [RoutePrefix("api/client")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ClientController : ApiController
    {
        private IProductRepository _productRepository;
        private ICounterpartyRepository _counterpartyRepository;
        private IConsignmentRepository _consignmentRepository;

        public ClientController(IProductRepository productRepository,
            ICounterpartyRepository counterpartyRepository, IConsignmentRepository consignmentRepository)
        {
            _productRepository = productRepository;
            _counterpartyRepository = counterpartyRepository;
            _consignmentRepository = consignmentRepository;
        }

        [Route("generate-code")]
        [HttpPost]
        public HttpResponseMessage Generate(GenerateRequestVM generateVM)
        {
            try
            {
                if (generateVM != null)
                {
                    var generator = new Generator();
                    var serialKeys = generator.GenerateCodes(generateVM);
                    foreach (var serialKey in serialKeys)
                    {
                        _consignmentRepository.SaveConsignment(new Consignment()
                        {
                            ProductId = generateVM.ProductId,
                            CounterpartyId = generateVM.CounterpartyId,
                            ConsignmentNumber = generateVM.ConsignmentNumber,
                            SerialKey = serialKey
                        });
                    }

                    var consignments = _consignmentRepository.GetConsignmentByConsignmentNumber(generateVM.ConsignmentNumber);
                    var consignmentVM = new List<ConsignmentRequestVM>();
                    foreach (var consignment in consignments)
                    {
                        consignmentVM.Add(new ConsignmentRequestVM()
                        {
                            ProductionDate = consignment.Product.ProductionDate.ToString("dd-MM-yyyy"),
                            Model = consignment.Product.Model,
                            SerialKey = consignment.SerialKey
                        });
                    }
                    //FileFormatter.FormatFile(consignmentVM, "../../App_Data/consignment.txt");

                    return Request.CreateResponse(HttpStatusCode.OK, consignmentVM);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [Route("get-products")]
        [HttpGet]
        public HttpResponseMessage GetProducts()
        {
            try
            {
                var products = _productRepository.GetProducts();
                var productsVM = new List<ProductGenerationRequestVM>();
                foreach (var product in products)
                {
                    productsVM.Add(new ProductGenerationRequestVM()
                    {
                        Id = product.Id,
                        Model = product.Model,
                        ProductionDate = product.ProductionDate.ToString("dd.MM.yyyy")
                    });
                }
                return Request.CreateResponse(HttpStatusCode.OK, productsVM);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [Route("get-counterparties")]
        [HttpGet]
        public HttpResponseMessage GetCounterparties()
        {
            try
            {
                var counterparties = _counterpartyRepository.GetCounterparties();
                var counterpartiesVM = new List<CounterpartyGenerationRequestVM>();
                foreach (var counterparty in counterparties)
                {
                    counterpartiesVM.Add(new CounterpartyGenerationRequestVM()
                    {
                        Id = counterparty.Id,
                        Name = counterparty.Name
                    });
                }
                return Request.CreateResponse(HttpStatusCode.OK, counterpartiesVM);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}
