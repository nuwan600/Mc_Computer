using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mc_Computer_API.Models
{
    public class Mc_Products
    {

        public string productID { get; set; }
        public string productName { get; set; }
        public string productDescription { get; set; }
        public int productQty { get; set; }
        public float productUnitPrice { get; set; }
    }
}