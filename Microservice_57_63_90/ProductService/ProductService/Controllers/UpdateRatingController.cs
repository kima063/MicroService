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
    [Route("product/updateRating")]
    [ApiController]
    public class UpdateRatingController : Controller
    {
        DatabaseContext db;
        public UpdateRatingController()
        {
            db = new DatabaseContext();
        }

        // POST: UpdateRatingController/Create
        [HttpPost]
        public IActionResult Post([FromBody] Products model)
        {
            try
            {
                Products pID = db.Products.Find(model.id);
                pID.averageRating = model.averageRating;
                pID.numberOfRaters = model.numberOfRaters;
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