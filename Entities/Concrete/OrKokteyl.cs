using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class OrKokteyl :IEntity
    {
        public int OrKokteylId { get; set; }
        public string Photo { get; set; }
        public string Detay { get; set; }
    }
}
