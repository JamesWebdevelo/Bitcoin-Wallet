using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer;
using BusinessLayer.Models;
using BusinessLayer.Configuration;

namespace WebApiLayer.Controllers
{
    [Produces("application/json")]
    [Route("api/Wallet")]
    public class WalletController : Controller
    {
        // GET: api/Wallet
        [HttpGet]
        public IEnumerable<string> Get()
        {
            string password = "admin";

            Config.Load();
            Wallet.GenerateWallet(password);
            var result = Receiver.GetPublicAddresses(password);

            return result;
        }

        // GET: api/Wallet/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Wallet
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Wallet/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
