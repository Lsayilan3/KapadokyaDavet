using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class SliderTwo :IEntity
    {
        public int SliderTwoId { get; set; }
        public string Title { get; set; }
        public string Detay { get; set; }
        public int Price { get; set; }
        public int DiscountPrice { get; set; }
        public string Photo { get; set; }
    }
}
