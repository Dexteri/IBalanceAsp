using IBalance.Domain.Abstract;
using IBalance.Domain.Entities;
using IBalance.Domain.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace IBalance.Web.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public ActionResult Index(int? page)
        {
            try
            {
                if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    int pageSize = 5;
                    int pageNumber = (page ?? 1);
                    var products = _productRepository.GetProducts();
                    return View(products.ToPagedList(pageNumber, pageSize));
                }
                return RedirectToAction("Index", "Account");
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public ActionResult Add(ProductRequestVM product)
        {
            try
            {
                if(product != null)
                {
                    var fileContent = Request.Files;
                    if (Request.Files.Count > 0)
                    {
                        var file = Request.Files[0];
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/images/"), fileName);
                        file.SaveAs(path);
                        product.ImageUrl = $"/images/{fileName}";
                    }
                    else
                    {
                        product.ImageUrl = "/images/default_img.png";
                    }
                    var newProduct = new Product()
                    {
                        Model = product.Model,
                        ProductionDate = product.ProductionDate,
                        Colour = product.Colour,
                        WheelsDiameter = product.WheelsDiameter,
                        MotorPower = product.MotorPower,
                        BatteryManufacturer = product.BatteryManufacturer,
                        Amperes = product.Amperes,
                        Weight = product.Weight,
                        MaxSpeed = product.MaxSpeed,
                        PossibleMileage = product.PossibleMileage,
                        Motherboard = product.Motherboard,
                        Application = product.Application,
                        ImageUrl = product.ImageUrl
                    };
                    _productRepository.SaveProduct(newProduct);
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
        public ActionResult Update(Product product)
        {
            try
            {
                if (product != null)
                {
                    var fileContent = Request.Files;
                    if (Request.Files.Count > 0)
                    {
                        var file = Request.Files[0];
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/images/"), fileName);
                        if (!System.IO.File.Exists(path))
                        {
                            file.SaveAs(path);
                        }
                        product.ImageUrl = $"/images/{fileName}";
                    }
                    else if(product.ImageUrl == "undefined")
                    {
                        product.ImageUrl = "/images/default_img.png";
                    }
                    _productRepository.SaveProduct(product);
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
        public ActionResult Copy(Product product)
        {
            try
            {
                if (product != null)
                {
                    var fileContent = Request.Files;
                    if (Request.Files.Count > 0)
                    {
                        var file = Request.Files[0];
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/images/"), fileName);
                        if (!System.IO.File.Exists(path))
                        {
                            file.SaveAs(path);
                        }
                        product.ImageUrl = $"/images/{fileName}";
                    }
                    else if (product.ImageUrl == "undefined")
                    {
                        product.ImageUrl = "/images/default_img.png";
                    }
                    _productRepository.SaveProduct(product);
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
        public ActionResult Delete(int productId)
        {
            try
            {
                if (productId != 0)
                {
                    _productRepository.DeleteProduct(productId);
                    return Json(new { result = "success" });
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
            catch(Exception e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }
    }
}