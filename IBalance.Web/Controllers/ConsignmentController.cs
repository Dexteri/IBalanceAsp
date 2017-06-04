using IBalance.Domain.Abstract;
using IBalance.Domain.Entities;
using IBalance.Domain.ViewModels;
using IBalance.Web.Infrastructure;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace IBalance.Web.Controllers
{
    public class ConsignmentController : Controller
    {
        private IConsignmentRepository _consignmentRepository;
        private ICounterpartyRepository _counterpartyRepository;

        public ConsignmentController(IConsignmentRepository consignmentRepository, ICounterpartyRepository counterpartyRepository)
        {
            _consignmentRepository = consignmentRepository;
            _counterpartyRepository = counterpartyRepository;
        }

        [HttpGet]
        public ActionResult Index(int? page)
        {
            try
            {
                if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    int pageSize = 100;
                    int pageNumber = (page ?? 1);
                    var consignments = _consignmentRepository.GetConsignments();
                    var counterpartiesNames = _counterpartyRepository.GetCounterpartiesNames();
                    var consignmentNumbers = _consignmentRepository.GetConsignmentNumbers();
                    return View(new ConsignmentIndexRequestVM()
                    {
                        ConsignmentNumbers = consignmentNumbers,
                        CounterpartiesNames = counterpartiesNames,
                        Consignments = consignments.ToPagedList(pageNumber, pageSize)
                    });
                }
                return RedirectToAction("Index", "Account");
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        public ActionResult Filter(ConsignmentFilterRequestVM filter)
        {
            try
            {
                if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    if (filter != null)
                    {
                        var counterpartiesNames = _counterpartyRepository.GetCounterpartiesNames();
                        var consignmentNumbers = _consignmentRepository.GetConsignmentNumbers();
                        List<Consignment> consignments = null;
                        if (filter.FilterParameter == "searchByDate")
                        {
                            consignments = _consignmentRepository.GetConsignmentsByDate(Convert.ToDateTime(filter.FilterValue));
                        }
                        else if (filter.FilterParameter == "searchByCounterparty")
                        {
                            consignments = _consignmentRepository.GetConsignmentsByCounterpartyName(filter.FilterValue);
                        }
                        else if (filter.FilterParameter == "searchByConsignmentNumber")
                        {
                            consignments = _consignmentRepository.GetConsignmentsByConsignmentNumber(filter.FilterValue);

                        }
                        return View(new ConsignmentFilterResultVM()
                        {
                            ConsignmentNumbers = consignmentNumbers,
                            CounterpartiesNames = counterpartiesNames,
                            Consignments = consignments
                        });
                    }
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                return RedirectToAction("Index", "Account");
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public ActionResult DeleteAll()
        {
            try
            {
                _consignmentRepository.DeleteAllConsignments();
                return Json(new { result = "success" });
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public ActionResult DeleteSelected(List<int> idList)
        {
            try
            {
                if (idList != null)
                {
                    _consignmentRepository.DeleteConsignments(idList);
                    return Json(new { result = "success" });
                }
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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
                var consignments = _consignmentRepository.GetConsignments();
                var consignmentsVM = new List<ConsignmentRequestVM>();
                foreach (var consignment in consignments)
                {
                    consignmentsVM.Add(new ConsignmentRequestVM()
                    {
                        SerialKey = consignment.SerialKey,
                        ProductionDate = consignment.Product.ProductionDate.ToString("dd-MM-yyyy"),
                        Model = consignment.Product.Model,
                    });
                }
                var pathToFile = Server.MapPath("~/App_Data/consignment.txt");
                FileFormatter.FormatFile(consignmentsVM, pathToFile);
                string fileType = "text/plain";
                string fileName = "consignment.txt";
                return File(pathToFile, fileType, fileName); ;
            }
            return null;
        }

        [HttpPost]
        public ActionResult FormatSelected(List<int> idList)
        {
            try
            {
                if (idList != null)
                {
                    var consignments = _consignmentRepository.GetConsignmentsWithId(idList);
                    var consignmentsVM = new List<ConsignmentRequestVM>();
                    foreach (var consignment in consignments)
                    {
                        consignmentsVM.Add(new ConsignmentRequestVM()
                        {
                            SerialKey = consignment.SerialKey,
                            ProductionDate = consignment.Product.ProductionDate.ToString("dd-MM-yyyy"),
                            Model = consignment.Product.Model,
                        });
                    }
                    var pathToFile = Server.MapPath("~/App_Data/consignment.txt");
                    FileFormatter.FormatFile(consignmentsVM, pathToFile);
                    return Json(new { redirect = Url.Action("DownloadSelected") });
                }
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        public FileResult DownloadSelected()
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
    }
}