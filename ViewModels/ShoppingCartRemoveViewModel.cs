using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DSW_Beta_V2.Models;

namespace DSW_Beta_V2.ViewModels
{
    public class ShoppingCartRemoveViewModel
    {
        public string Message { get; set; }
        public decimal CartTotal { get; set; }
        public int CartCount { get; set; }
        public int ItemCount { get; set; }
        public int DeleteId { get; set; }
    }
}