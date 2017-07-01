using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DSW_Beta_V2.Models;

namespace DSW_Beta_V2.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<Carrito> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}