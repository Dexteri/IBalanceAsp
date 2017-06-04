using IBalance.Domain.Abstract;
using IBalance.Domain.Entities;
using IBalance.Domain.ViewModels;
using IBalance.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace IBalance.Web.Controllers
{
    public class GenerationController : Controller
    {
        private IProductRepository _productRepository;
        private ICounterpartyRepository _counterpartyRepository;
        private IConsignmentRepository _consignmentRepository;

        public GenerationController(IProductRepository productRepository,
            ICounterpartyRepository counterpartyRepository, IConsignmentRepository consignmentRepository)
        {
            _productRepository = productRepository;
            _counterpartyRepository = counterpartyRepository;
            _consignmentRepository = consignmentRepository;
        }

        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
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
                    var data = new GenerationRequestVM()
                    {
                        Counterparties = counterpartiesVM,
                        Products = productsVM
                    };
                    return View(data);
                }
                return RedirectToAction("Index", "Account");
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public ActionResult Generate(GenerateRequestVM generateVM)
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
                    //string pathToFile = (Server.MapPath("~/App_Data/consignment.xml"));
                    //  var serializer = new Serializer(pathToFile);
                    FileFormatter.FormatFile(consignmentVM, Server.MapPath("~/App_Data/consignment.txt"));
                    // serializer.SerializeObject(consignmentVM);

                    return Json(new { result = serialKeys, consignmentNumber = generateVM.ConsignmentNumber });
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }
       
        public FileResult Download()
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var pathToFile = Server.MapPath("~/App_Data/consignment.txt");
                string fileType = "text/plain";
                string fileName = "consignment.txt";
                return File(pathToFile, fileType, fileName);
            }
            return null;
        }

        [HttpGet]
        public ActionResult FormatXml()
        {
            try
            {
                if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    string pathToFile = (Server.MapPath("~/App_Data/consignment.xml"));
                    return Content(System.IO.File.ReadAllText(pathToFile), "text/xml");
                }
                return RedirectToAction("Index", "Account");
            }
            catch (Exception e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }
    }
}