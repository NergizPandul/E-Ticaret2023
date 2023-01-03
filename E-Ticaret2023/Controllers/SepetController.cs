using E_Ticaret2023.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Ticaret2023.Controllers
{
    [Authorize]
    public class SepetController : Controller
    {
       Entities db=new Entities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SepeteEkle(int id,int adet)
        {
            string kulID = User.Identity.GetUserId(); //login olan kullanıcının idsini almak için
            Sepet sepettekiurun=db.Sepet.FirstOrDefault(x=>x.UrunID==id && x.KullaniciID==kulID);
            Urunler urun = db.Urunler.Find(id);
            if (sepettekiurun==null)
            {
                Sepet yeniurun = new Sepet()
                {
                    KullaniciID = kulID,
                    UrunID = id,
                    Adet=adet,
                    ToplamTutar=urun.UrunFiyati * adet
                };
                db.Sepet.Add(yeniurun);
            }
            else
            {
                sepettekiurun.Adet = sepettekiurun.Adet + adet;
                sepettekiurun.ToplamTutar = sepettekiurun.Adet * urun.UrunFiyati;

            }
            db.SaveChanges();
            return RedirectToAction("Index");
           
        }
    }
}