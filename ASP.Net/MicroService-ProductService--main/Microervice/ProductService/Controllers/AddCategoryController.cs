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
    [Route("category/add")]
    [ApiController]
    public class AddCategoryController : Controller
    {
        // GET: AddCategoryController
        DatabaseContext db;
        public AddCategoryController()
        {
            db = new DatabaseContext();
        }

        [HttpPost]
        public IActionResult Post([FromBody] Category model)
        {
            try
            {
                db.Category.Add(model);
                db.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, model);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
    }
}
