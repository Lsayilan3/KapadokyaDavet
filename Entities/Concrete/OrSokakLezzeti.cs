using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class OrSokakLezzeti :IEntity
    {
        public int OrSokakLezzetiId { get; set; }
        public string Photo { get; set; }
        public string Detay { get; set; }
    }
}
