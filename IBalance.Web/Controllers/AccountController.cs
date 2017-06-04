using IBalance.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace IBalance.Web.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(AccountLoginRequestVM loginData)
        {
            if(loginData != null)
            {
                if (loginData.Login == "giro-opt@ya.ru" && loginData.Password == "52545856")
                {
                    FormsAuthentication.SetAuthCookie(loginData.Login, true);
                    return Json(new { result = "redirect", url = Url.Action("Index", "Home") });
                }
                return Json(new { result = "invalid login" });
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
    }
}