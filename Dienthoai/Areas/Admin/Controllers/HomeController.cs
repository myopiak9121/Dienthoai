using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Dienthoai.Models;
using PagedList;

namespace Dienthoai.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private Qlbanhang db = new Qlbanhang();

        // GET: Admin/Home
        public ActionResult Index(int? page)
        {
            /*var sanphams = db.Sanphams.Include(s => s.Hangsanxuat).Include(s => s.Hedieuhanh);
            return View(sanphams.ToList());*/
            // 1. Tham số int? dùng để thể hiện null và kiểu int( số nguyên)
            // page có thể có giá trị là null ( rỗng) và kiểu int.

            // 2. Nếu page = null thì đặt lại là 1.
            if (page == null) page = 1;

            // 3. Tạo truy vấn sql, lưu ý phải sắp xếp theo trường nào đó, ví dụ OrderBy
            // theo Masp mới có thể phân trang.
            var sp = db.Sanphams.OrderBy(x => x.Masp);

            // 4. Tạo kích thước trang (pageSize) hay là số sản phẩm hiển thị trên 1 trang
            int pageSize = 5;

            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);

            // 5. Trả về các sản phẩm được phân trang theo kích thước và số trang.
            return View(sp.ToPagedList(pageNumber, pageSize));

        }

        // GET: Admin/Home/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sanpham sanpham = db.Sanphams.Find(id);
            if (sanpham == null)
            {
                return HttpNotFound();
            }
            return View(sanpham);
        }

        // GET: Admin/Home/Create
        public ActionResult Create()
        {
            ViewBag.Mahang = new SelectList(db.Hangsanxuats, "Mahang", "Tenhang");
            ViewBag.Mahdh = new SelectList(db.Hedieuhanhs, "Mahdh", "Tenhdh");
            return View();
        }

        // POST: Admin/Home/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Masp,Tensp,Giatien,Soluong,Mota,Thesim,Bonhotrong,Sanphammoi,Ram,Anhbia,Mahang,Mahdh")] Sanpham sanpham)
        {
            if (ModelState.IsValid)
            {
                db.Sanphams.Add(sanpham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Mahang = new SelectList(db.Hangsanxuats, "Mahang", "Tenhang", sanpham.Mahang);
            ViewBag.Mahdh = new SelectList(db.Hedieuhanhs, "Mahdh", "Tenhdh", sanpham.Mahdh);
            return View(sanpham);
        }

        // GET: Admin/Home/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sanpham sanpham = db.Sanphams.Find(id);
            if (sanpham == null)
            {
                return HttpNotFound();
            }
            ViewBag.Mahang = new SelectList(db.Hangsanxuats, "Mahang", "Tenhang", sanpham.Mahang);
            ViewBag.Mahdh = new SelectList(db.Hedieuhanhs, "Mahdh", "Tenhdh", sanpham.Mahdh);
            return View(sanpham);
        }

        // POST: Admin/Home/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Masp,Tensp,Giatien,Soluong,Mota,Thesim,Bonhotrong,Sanphammoi,Ram,Anhbia,Mahang,Mahdh")] Sanpham sanpham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanpham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Mahang = new SelectList(db.Hangsanxuats, "Mahang", "Tenhang", sanpham.Mahang);
            ViewBag.Mahdh = new SelectList(db.Hedieuhanhs, "Mahdh", "Tenhdh", sanpham.Mahdh);
            return View(sanpham);
        }

        // GET: Admin/Home/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sanpham sanpham = db.Sanphams.Find(id);
            if (sanpham == null)
            {
                return HttpNotFound();
            }
            return View(sanpham);
        }

        // POST: Admin/Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sanpham sanpham = db.Sanphams.Find(id);
            db.Sanphams.Remove(sanpham);
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
