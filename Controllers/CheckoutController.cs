using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DSW_Beta_V2.Models;

namespace DSW_Beta_V2.Controllers
{
    public class CheckoutController : Controller
    {
        //
        // GET: Checkout
        ProyectoCarritoEntities storeDB = new ProyectoCarritoEntities();
        const string PromoCode = "Waifu";

        public ActionResult Index()
        {
            return View();
        }

        // GET: Checkout/Create
        public ActionResult AddressAndPayment()
        {
            return View();
        }

        // POST: Checkout/Create
        [HttpPost]
        public ActionResult AddressAndPayment(FormCollection values)
        {
            var order = new Orden();
            TryUpdateModel(order);

            try
            {
                if (string.Equals(values["PromoCode"], PromoCode,
                    StringComparison.OrdinalIgnoreCase) == false)
                {
                    return View(order);
                }
                else
                {
                    order.nombre = User.Identity.Name;
                    order.FechaOrden = DateTime.Now;

                    //Save order
                    storeDB.Orden.Add(order);
                    storeDB.SaveChanges();

                    //Process the order
                    var cart = CarritoCompra.GetCart(this.HttpContext);
                    cart.CreateOrder(order);

                    return RedirectToAction("Complete",
                        new { id = order.idOrden});
                }
                
            }
            catch
            {
                //Invalid - reDisplay with errors
                return View(order);
            }
        }

        //
        // GET: Checkout/Complete
        public ActionResult Complete(int id)
        {
            //Validate customer owns this order
            bool isValid = storeDB.Orden.Any(
                o => o.idOrden == id &&
                o.nombre == User.Identity.Name);

            if (isValid)
            {
                return View(id);
            }
            return View("Error");
        }

    }
}
