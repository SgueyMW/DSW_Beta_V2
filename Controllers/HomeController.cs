using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DSW_Beta_V2.Models;

namespace DSW_Beta_V2.Controllers
{
    public class HomeController : Controller
    {
        //
        //GET: /Home/
        ProyectoCarritoEntities storeDB = new ProyectoCarritoEntities();
        public ActionResult Index()
        {
            var videojuegos = GetTopSellingGames(5);
            return View();
        }

        private List<Videojuego> GetTopSellingGames(int count)
        {
            //Group the order details by game and return
            //the games with the highest count

            return storeDB.Videojuego
                .OrderByDescending(a => a.DetalleOrden.Count())
                .Take(count)
                .ToList();
        }
    }
}