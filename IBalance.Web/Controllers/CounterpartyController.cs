using IBalance.Domain.Abstract;
using IBalance.Domain.Entities;
using IBalance.Domain.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace IBalance.Web.Controllers
{
    public class CounterpartyController : Controller
    {
        private ICounterpartyRepository _counterpartyRepository;

        public CounterpartyController(ICounterpartyRepository counterpartyRepository)
        {
            _counterpartyRepository = counterpartyRepository;
        }

        [HttpGet]
        public ActionResult Index(int? page)
        {
            try
            {
                if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    int pageSize = 10;
                    int pageNumber = (page ?? 1);
                    var counterparties = _counterpartyRepository.GetCounterparties();
                    return View(counterparties.ToPagedList(pageNumber, pageSize));
                }
                return RedirectToAction("Index", "Account");
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public ActionResult Add(CounterpartyRequestVM counterparty)
        {
            try
            {
                if (counterparty != null)
                {
                    var newCounterparty = new Counterparty()
                    {
                        Name = counterparty.Name,
                        City = counterparty.City,
                        Email = counterparty.Email,
                        CounterpartyType = counterparty.Type
                    };
                    if (counterparty.Phones != null)
                    {
                        var newPhones = new List<CounterpartyToPhone>();
                        foreach (var phone in counterparty.Phones)
                        {
                            newPhones.Add(new CounterpartyToPhone()
                            {
                                Phone = phone
                            });
                        }
                        newCounterparty.Phones = newPhones;
                    }
                    _counterpartyRepository.SaveCounterparty(newCounterparty);
                    return Json(new { result = "success" });
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

        [HttpPost]
        public ActionResult Update(CounterpartyUpdateRequestVM counterparty)
        {
            try
            {
                if (counterparty != null)
                {
                    var newCounterparty = new Counterparty()
                    {
                        Id = counterparty.Id,
                        Name = counterparty.Name,
                        City = counterparty.City,
                        Email = counterparty.Email,
                        CounterpartyType = counterparty.Type
                    };
                    if (counterparty.Phones != null)
                    {
                        var newPhones = new List<CounterpartyToPhone>();
                        foreach (var phone in counterparty.Phones)
                        {
                            newPhones.Add(new CounterpartyToPhone()
                            {
                                Phone = phone
                            });
                        }
                        newCounterparty.Phones = newPhones;
                    }
                    _counterpartyRepository.DeletePhones(counterparty.Id);
                    _counterpartyRepository.SaveCounterparty(newCounterparty);
                    return Json(new { result = "success" });
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

        [HttpPost]
        public ActionResult Delete(int counterpartyId)
        {
            try
            {
                if (counterpartyId != 0)
                {
                    _counterpartyRepository.DeleteCounterparty(counterpartyId);
                    return Json(new { result = "success" });
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
    }
}