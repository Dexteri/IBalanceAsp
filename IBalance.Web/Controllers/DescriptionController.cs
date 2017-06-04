using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IBalance.Web.Controllers
{
    public class DescriptionController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}