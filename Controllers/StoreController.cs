using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DSW_Beta_V2.Models;

namespace DSW_Beta_V2.Controllers
{
    public class StoreController : Controller
    {
        //
        // GET: Store
        ProyectoCarritoEntities storeDB = new ProyectoCarritoEntities();

        public ActionResult Index()
        {
            var genres = storeDB.Genero.ToList();
            return View(genres);
        }

        // GET: Store/Details/5
        public ActionResult Browse(string genre)
        {
            var genreModel = storeDB.Genero.Include("Videojuego").Single(g => g.nomGenero == genre);
            return View(genreModel);
        }

        // GET: Store/Create
        public ActionResult Details(int id)
        {
            var game = storeDB.Videojuego.Find(id);
            return View(game);
        }        

        // POST: Store/Delete/5
        [ChildActionOnly]
        public ActionResult GenreMenu()
        {
            var genres = storeDB.Genero.ToList();

            return PartialView(genres);
        }
    }
}
