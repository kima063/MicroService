using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductService.Database;

namespace ProductService.Controllers
{
    [Route("product/remove")]
    [ApiController]
    public class RemoveProductController : Controller
    {
        DatabaseContext db;

        public RemoveProductController()
        {
            db = new DatabaseContext();
        }

        // POST: RemoveProductController/Delete/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                db.Products.Remove(db.Products.Find(id));
                db.SaveChanges();
                return StatusCode(StatusCodes.Status202Accepted);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
    }
}