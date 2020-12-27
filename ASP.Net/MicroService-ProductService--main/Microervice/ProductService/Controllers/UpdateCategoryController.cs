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
    [Route("product/updateCategory")]
    [ApiController]

    public class UpdateCategoryController : Controller
    {
        DatabaseContext db;
        public UpdateCategoryController()
        {
            db = new DatabaseContext();
        }


        // POST: UpdateCategoryController/Create
        [HttpPost]
        public IActionResult Post([FromBody] Products model)
        {
            try
            {
                Products pID = db.Products.Find(model.id);
                Category category = db.Category.Find(model.categoryId);
                pID.categoryId = model.categoryId;
                pID.categoryName = category.categoryName;
                pID.averageRating = 0;
                pID.numberOfRaters = 0;
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
