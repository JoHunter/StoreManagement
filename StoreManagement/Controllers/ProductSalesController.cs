using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StoreManagement.App_Data;

namespace StoreManagement.Controllers
{
    public class ProductSalesController : Controller
    {
        private StoreManageEntities db = new StoreManageEntities();

        // GET: ProductSales
        public ActionResult Index()
        {           
            var productSales = db.ProductSales.Include(p => p.AspNetUser).Include(p => p.Product).Include(p => p.Store);
            ViewBag.Total = productSales.Sum(o => o.Quantity);
            return View(productSales.ToList());
        }

        // GET: ProductSales/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductSale productSale = db.ProductSales.Find(id);
            if (productSale == null)
            {
                return HttpNotFound();
            }
            return View(productSale);
        }

        // GET: ProductSales/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName");
            ViewBag.StoreId = new SelectList(db.Stores, "StoreId", "StoreName");
            return View();
        }

        // POST: ProductSales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProdSalesId,ProductId,StoreId,Quantity,UserId")] ProductSale productSale)
        {
            if (ModelState.IsValid)
            {
                productSale.Date = DateTime.Now;
                productSale.ProdSalesId = Guid.NewGuid();
                db.ProductSales.Add(productSale);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", productSale.UserId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName", productSale.ProductId);
            ViewBag.StoreId = new SelectList(db.Stores, "StoreId", "StoreName", productSale.StoreId);
            return View(productSale);
        }

        // GET: ProductSales/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductSale productSale = db.ProductSales.Find(id);
            if (productSale == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", productSale.UserId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName", productSale.ProductId);
            ViewBag.StoreId = new SelectList(db.Stores, "StoreId", "StoreName", productSale.StoreId);
            return View(productSale);
        }

        // POST: ProductSales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProdSalesId,ProductId,StoreId,Quantity,UserId")] ProductSale productSale)
        {
            if (ModelState.IsValid)
            {
                productSale.Date = DateTime.Now;
                db.Entry(productSale).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", productSale.UserId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName", productSale.ProductId);
            ViewBag.StoreId = new SelectList(db.Stores, "StoreId", "StoreName", productSale.StoreId);
            return View(productSale);
        }

        // GET: ProductSales/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductSale productSale = db.ProductSales.Find(id);
            if (productSale == null)
            {
                return HttpNotFound();
            }
            return View(productSale);
        }

        // POST: ProductSales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ProductSale productSale = db.ProductSales.Find(id);
            db.ProductSales.Remove(productSale);
            db.SaveChanges();
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
