using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using E_Ticaret2023.Models;

namespace E_Ticaret2023.Controllers
{
    public class UrunlerController : Controller
    {
        private Entities db = new Entities();

        // GET: Urunler
        public ActionResult Index()
        {
            var urunler = db.Urunler.Include(u => u.Kategoriler);
            return View(urunler.ToList());
        }

        // GET: Urunler/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urunler urunler = db.Urunler.Find(id);
            if (urunler == null)
            {
                return HttpNotFound();
            }
            return View(urunler);
        }

        // GET: Urunler/Create
        public ActionResult Create()
        {
            ViewBag.KategoriID = new SelectList(db.Kategoriler, "KateoriID", "KategoriAdi");
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Urunler urunler,HttpPostedFileBase urunResim)
        {
            if (ModelState.IsValid)
            {
                db.Urunler.Add(urunler);
                db.SaveChanges();
                if (urunResim!=null)
                {
                    string dosya = Path.Combine(Server.MapPath("~/Resim/"), urunler.UrunID + ".jpg");
                    urunResim.SaveAs(dosya);
                }
                return RedirectToAction("Index");
            }

            ViewBag.KategoriID = new SelectList(db.Kategoriler, "KateoriID", "KategoriAdi", urunler.KategoriID);
            return View(urunler);
        }

        // GET: Urunler/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urunler urunler = db.Urunler.Find(id);
            if (urunler == null)
            {
                return HttpNotFound();
            }
            ViewBag.KategoriID = new SelectList(db.Kategoriler, "KateoriID", "KategoriAdi", urunler.KategoriID);
            return View(urunler);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UrunID,UrunAdi,KategoriID,UrunAciklamasi,UrunFiyati")] Urunler urunler)
        {
            if (ModelState.IsValid)
            {
                db.Entry(urunler).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KategoriID = new SelectList(db.Kategoriler, "KateoriID", "KategoriAdi", urunler.KategoriID);
            return View(urunler);
        }

        // GET: Urunler/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urunler urunler = db.Urunler.Find(id);
            if (urunler == null)
            {
                return HttpNotFound();
            }
            return View(urunler);
        }

        // POST: Urunler/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Urunler urunler = db.Urunler.Find(id);
            db.Urunler.Remove(urunler);
            db.SaveChanges();
            string dosya = Path.Combine(Server.MapPath("~/Resim/"), urunler.UrunID + ".jpg");
            FileInfo fi = new FileInfo(dosya);
            if (fi.Exists)
            {
                fi.Delete();
            }

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
