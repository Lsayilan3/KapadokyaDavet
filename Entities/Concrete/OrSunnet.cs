using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class OrSunnet :IEntity
    {
        public int OrSunnetId { get; set; }
        public string Photo { get; set; }
        public string Detay { get; set; }
    }
}
