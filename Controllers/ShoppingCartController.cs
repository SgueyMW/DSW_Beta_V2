using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DSW_Beta_V2.Models;
using DSW_Beta_V2.ViewModels;

namespace DSW_Beta_V2.Controllers
{
    public class ShoppingCartController : Controller
    {
        //
        // GET: ShoppingCart
        ProyectoCarritoEntities storeDB = new ProyectoCarritoEntities();

        public ActionResult Index()
        {
            var cart = CarritoCompra.GetCart(this.HttpContext);
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };
            return View(viewModel);
        }
                
        public ActionResult AddToCart(int id)
        {
            var addedGame = storeDB.Videojuego.Single(game => game.codigo == id);
            var cart = CarritoCompra.GetCart(this.HttpContext);
            cart.AddToCart(addedGame);
            return RedirectToAction("Index");
        }

        // POST: ShoppingCart/Create
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            var cart = CarritoCompra.GetCart(this.HttpContext);
            string gameName = storeDB.Carrito.Single(item => item.idRecord == id).Videojuego.titulo;
            int itemCount = cart.RemoveFromCart(id);
            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(gameName) + "ha sido eliminado del carrito de compra.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };
            return Json(results);               
        }

        // POST: ShoppingCart/Delete/5
        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = CarritoCompra.GetCart(this.HttpContext);
            ViewData["CartCount"] = cart.GetCount();
            return PartialView("CartSummary");            
        }
    }
}
