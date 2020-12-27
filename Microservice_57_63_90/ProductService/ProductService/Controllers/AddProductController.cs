using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductService.Database;
using ProductService.Database.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductService.Controllers
{
    [Route("product/add")]
    [ApiController]
    public class AddProductController : ControllerBase
    {
        DatabaseContext db;
        public AddProductController()
        {
            db = new DatabaseContext();
        }

        // POST api/<ValuesController>
        [HttpPost]
        public IActionResult Post([FromBody] Products model)
        {
            try
            {
                if (db.Products.Any(nm => nm.name == model.name))
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
                else
                {
                    Category category = db.Category.Find(model.categoryId);
                    model.categoryName = category.categoryName;
                    model.averageRating = 0;
                    model.numberOfRaters = 0;
                    db.Products.Add(model);
                    db.SaveChanges();
                    return StatusCode(StatusCodes.Status201Created, model);
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
    }
}
