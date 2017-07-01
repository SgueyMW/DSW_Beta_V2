using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DSW_Beta_V2.Models;

namespace DSW_Beta_V2.Controllers
{
    public class StoreManagerController : Controller
    {
        private ProyectoCarritoEntities db = new ProyectoCarritoEntities();

        //
        // GET: StoreManager

        public ActionResult Index()
        {
            var game = db.Videojuego.Include(a => a.Desarrolladora).Include(a => a.Genero);
            return View(game.ToList());
        }

        //
        // GET: StoreManager/Details/5
        public ActionResult Details(int id = 0)
        {
            Videojuego game = db.Videojuego.Find(id);
            if(game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        //
        // GET: StoreManager/Create
        public ActionResult Create()
        {
            ViewBag.IdCategoria = new SelectList(db.Categoria, "idCategoria", "nomCategoria");
            ViewBag.IdDesarrolladora = new SelectList(db.Desarrolladora, "IdDesarrolladora", "nomDesarrolladora");
            ViewBag.IdEstado = new SelectList(db.Estado, "idEstado", "nomEstado");
            ViewBag.IdGenero = new SelectList(db.Genero, "idGenero", "nomGenero");
            ViewBag.IdPais = new SelectList(db.Pais, "idPais", "nomPais");
            ViewBag.IdPlataforma = new SelectList(db.Plataforma, "idPlataforma", "nomPlataforma");
            return View();
        }

        //
        // POST: StoreManager/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Videojuego game)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add insert logic here
                db.Videojuego.Add(game);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdCategoria = new SelectList(db.Categoria, "idCategoria", "nomCategoria", game.idCategoria);
            ViewBag.IdDesarrolladora = new SelectList(db.Desarrolladora, "IdDesarrolladora", "nomDesarrolladora", game.idDesarrolladora);
            ViewBag.IdEstado = new SelectList(db.Estado, "idEstado", "nomEstado", game.idEstado);
            ViewBag.IdGenero = new SelectList(db.Genero, "idGenero", "nomGenero", game.idGenero);
            ViewBag.IdPais = new SelectList(db.Pais, "idPais", "nomPais", game.idPais);
            ViewBag.IdPlataforma = new SelectList(db.Plataforma, "idPlataforma", "nomPlataforma", game.idPlataforma);
            return View(game);
            
        }

        //
        // GET: StoreManager/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Videojuego game = db.Videojuego.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCategoria = new SelectList(db.Categoria, "idCategoria", "nomCategoria", game.idCategoria);
            ViewBag.IdDesarrolladora = new SelectList(db.Desarrolladora, "IdDesarrolladora", "nomDesarrolladora", game.idDesarrolladora);
            ViewBag.IdEstado = new SelectList(db.Estado, "idEstado", "nomEstado", game.idEstado);
            ViewBag.IdGenero = new SelectList(db.Genero, "idGenero", "nomGenero", game.idGenero);
            ViewBag.IdPais = new SelectList(db.Pais, "idPais", "nomPais", game.idPais);
            ViewBag.IdPlataforma = new SelectList(db.Plataforma, "idPlataforma", "nomPlataforma", game.idPlataforma);
            return View(game);
        }

        //
        // POST: StoreManager/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Videojuego game)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add update logic here
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCategoria = new SelectList(db.Categoria, "idCategoria", "nomCategoria", game.idCategoria);
            ViewBag.IdDesarrolladora = new SelectList(db.Desarrolladora, "IdDesarrolladora", "nomDesarrolladora", game.idDesarrolladora);
            ViewBag.IdEstado = new SelectList(db.Estado, "idEstado", "nomEstado", game.idEstado);
            ViewBag.IdGenero = new SelectList(db.Genero, "idGenero", "nomGenero", game.idGenero);
            ViewBag.IdPais = new SelectList(db.Pais, "idPais", "nomPais", game.idPais);
            ViewBag.IdPlataforma = new SelectList(db.Plataforma, "idPlataforma", "nomPlataforma", game.idPlataforma);
            return View(game);            
        }

        //
        // GET: StoreManager/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Videojuego game = db.Videojuego.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        //
        // POST: StoreManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Videojuego game = db.Videojuego.Find(id);
            db.Videojuego.Remove(game);
            db.SaveChanges();
            return RedirectToAction("Index");            
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
