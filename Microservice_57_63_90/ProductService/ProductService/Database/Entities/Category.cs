using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.Database.Entities
{
    public class Category
    {
        public Guid categoryId { get; set; }
        public String categoryName { get; set; }
    }
}
