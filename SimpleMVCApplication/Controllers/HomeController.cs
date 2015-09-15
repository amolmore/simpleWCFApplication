using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleMVCApplication.Models;
using SimpleMVCApplication.SimpleServiceReference;

namespace SimpleMVCApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //really simple action that returns json data of products from northwind
        public JsonResult GetProducts()
        {
            var client = new SimpleServiceClient();
            var products = client.GetProductData();

            //maybe use value injector for this
            List<ProductVM> productVMs = new List<ProductVM>();
            foreach (var product in products)
            {
                var productVM = new ProductVM();
                productVM.ProductID = product.ProductID;
                productVM.ProductName = product.ProductName;
                productVM.UnitInStock = product.UnitInStock;
                productVM.Discontinued = product.Discontinued;
                productVMs.Add(productVM);
            }
            return Json(productVMs,JsonRequestBehavior.AllowGet);
        }


    }
}