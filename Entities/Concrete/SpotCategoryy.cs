using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class SpotCategoryy :IEntity
    {
        public int SpotCategoryyId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
