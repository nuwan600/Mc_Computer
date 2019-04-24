using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mc_Computer_API.Models
{
    public class Mc_Product_Tranasaction
    {
        public string trID { get; set; }
        public string pID { get; set; }
        public int Qty { get; set; }
        public float Unitprice { get; set; }
        public float Amountprice { get; set; }

    }
}