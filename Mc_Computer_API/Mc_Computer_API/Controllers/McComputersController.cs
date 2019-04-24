using Mc_Computer_API.Implementation;
using Mc_Computer_API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Mc_Computer_API.Controllers
{
    public class McComputersController : ApiController
    {

        MasterImplementation McComputersMaster;
        public McComputersController()
        {
            McComputersMaster = new MasterImplementation();
        }


        // GET api/mccomputers
        public string Get()
        {
            //converting to the json
            var json = JsonConvert.SerializeObject(MasterImplementation.GetAllProducts());

            return json;
        }

        // GET api/mccomputers/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/mccomputers
        public void Post(string name, string Description, int qty, float unitprice)
        {
            Mc_Products products= new Mc_Products();
            products.productID=MasterImplementation.AutoIDGenarator("mc_product");
            products.productName=name;
            products.productDescription=Description;
            products.productQty=qty;
            products.productUnitPrice=unitprice;
            if(MasterImplementation.AddProducts(products))
            {
                
                string jsonmessage = @"{  
                    'Message': true  
                }";

                var json = JsonConvert.SerializeObject(jsonmessage);
                Console.WriteLine(json);
            }
            else
            {
                string jsonmessage = @"{  
                    'Message': false  
                }";

                var json = JsonConvert.SerializeObject(jsonmessage);
                Console.WriteLine(json);
            }

        }

        // PUT api/mccomputers/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/mccomputers/5
        public void Delete(int id)
        {
        }
    }
}
