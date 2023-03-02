using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Slider :IEntity
    {
        public int SliderId { get; set; }
        public string Photo { get; set; }
    }
}
