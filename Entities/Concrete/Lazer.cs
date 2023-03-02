using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
     public class Lazer :IEntity
    {
        public int LazerId { get; set; }
        public string Photo { get; set; }
        public string Title { get; set; }
        public string Tag { get; set; }
        public string Price { get; set; }
        public string DiscountPrice { get; set; }
    }
}
