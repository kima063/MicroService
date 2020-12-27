using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.Database.Entities
{
    public class Products
    {

        public Guid id { get; set; }

        public string name { get; set; }

        public Guid categoryId { get; set; }
        public String categoryName { get; set; }

        public float averageRating { get; set; }

        public int numberOfRaters { get; set; }

    }
}
