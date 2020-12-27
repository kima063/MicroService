using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductService.Database;
using ProductService.Database.Entities;

namespace ProductService.Controllers
{
    [Route("product/list")]
    [ApiController]
    public class FetchListOfProductController : Controller
    {
        DatabaseContext db;
        public FetchListOfProductController()
        {
            db = new DatabaseContext();
        }
        // GET: FetchListOfProductController
        [HttpGet]
        public IEnumerable<Products> Get()
        {
            return db.Products.ToList();
        }
    }
}
