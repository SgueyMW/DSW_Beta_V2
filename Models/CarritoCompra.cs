using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DSW_Beta_V2.Models
{
    public class CarritoCompra
    {
        ProyectoCarritoEntities storeDB = new ProyectoCarritoEntities();

        string ShoppingCartId { get; set; }
        public const string CartSessionKey = "idCarrito";
        //permite acceso a cookies
        public string GetCartId(HttpContextBase context)
        {
            if(context.Session[CartSessionKey] == null)
            {
                if(!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] = context.User.Identity.Name;
                }
                else
                {
                    //genera un guid aleatorio
                    Guid tempCartId = Guid.NewGuid();
                    //enviar tempIdCarrito de retorno al cliente como un cookie
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return context.Session[CartSessionKey].ToString();
        }

        public static CarritoCompra GetCart(HttpContextBase context)
        {
            var cart = new CarritoCompra();
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }
        //simplifica la llamada al carrito de compras
        public static CarritoCompra GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }

        public void AddToCart(Videojuego videojuego)
        {
            var cartItem = storeDB.Carrito.SingleOrDefault(
                            c => c.idCarrito == ShoppingCartId && c.codigo == videojuego.codigo);
            if(cartItem == null)
            {
                //crear un nuevo item en el carrito
                cartItem = new Carrito
                {
                    codigo = videojuego.codigo,
                    idCarrito = ShoppingCartId,
                    contador = 1,
                    fechaCreado = DateTime.Now
                };
                storeDB.Carrito.Add(cartItem);
            }
            else
            {
                cartItem.contador++;
            }
            storeDB.SaveChanges();
        }

        public int RemoveFromCart(int id)
        {
            var cartItem = storeDB.Carrito.Single(cart => cart.idCarrito == ShoppingCartId && cart.idRecord == id);
            int itemCount = 0;
            if (cartItem != null)
            {
                if(cartItem.contador > 1)
                {
                    cartItem.contador--;
                    itemCount = cartItem.contador;
                }
                else
                {
                    storeDB.Carrito.Remove(cartItem);
                }
                storeDB.SaveChanges();
            }
            return itemCount;
        }

        public void EmptyCart()
        {
            var cartItems = storeDB.Carrito.Where(cart => cart.idCarrito == ShoppingCartId);
            foreach(var cartItem in cartItems)
            {
                storeDB.Carrito.Remove(cartItem);
            }
            storeDB.SaveChanges();
        }

        public List<Carrito> GetCartItems()
        {
            return storeDB.Carrito.Where(cart => cart.idCarrito == ShoppingCartId).ToList();
        }

        public int GetCount()
        {
            //obtiene la cantidad de cada item del carrito y lo sumariza(no estoy seguro de q sea una palabra)
            int? count = (from cartItems in storeDB.Carrito
                          where cartItems.idCarrito == ShoppingCartId
                          select (int?)cartItems.contador).Sum();
            return count ?? 0;
        }

        public decimal GetTotal()
        {
            decimal? total = (from cartItems in storeDB.Carrito
                              where cartItems.idCarrito == ShoppingCartId
                              select (int?)cartItems.contador * cartItems.Videojuego.precio).Sum();
            return total ?? decimal.Zero;
        }

        public int CreateOrder(Orden order)
        {
            decimal orderTotal = 0;
            var cartItems = GetCartItems();

            foreach(var item in cartItems)
            {
                var orderDetail = new DetalleOrden
                {
                    codigo = item.codigo,
                    idOrden = order.idOrden,
                    preciouni = item.Videojuego.precio,
                    cantidad = item.contador
                };
                orderTotal += (item.contador * item.Videojuego.precio);
                storeDB.DetalleOrden.Add(orderDetail);
            }
            order.total = orderTotal;
            storeDB.SaveChanges();
            EmptyCart();
            return order.idOrden;
        }
    }
}