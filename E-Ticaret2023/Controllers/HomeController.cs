using E_Ticaret2023.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Ticaret2023.Controllers
{
    public class HomeController : Controller

    {   Entities db = new Entities();
        public ActionResult Index()
        {

            ViewBag.Kategoriler=db.Kategoriler.ToList();
            ViewBag.Urunler=db.Urunler.ToList();
            return View();
        }
        public ActionResult Kategori(int id)
        {
            ViewBag.Kategoriler = db.Kategoriler.ToList();
            ViewBag.Kategori = db.Kategoriler.Find(id);
            return View(db.Urunler.Where(x=>x.KategoriID==id).ToList());
        }

        public ActionResult Urun(int id)
        {
            ViewBag.Kategoriler = db.Kategoriler.ToList();
            return View(db.Urunler.Find(id));
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
    }
}