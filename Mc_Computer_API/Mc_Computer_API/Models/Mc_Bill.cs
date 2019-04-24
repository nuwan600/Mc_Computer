using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mc_Computer_API.Models
{
    public class Mc_Bill
    {

        public string tractionID { get; set; }
        public List<Mc_Products> loadProductsList { get; set; }
        public string Totalamount { get; set; }
    }
}