using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class OrCatering :IEntity
    {
        public int OrCateringId { get; set; }
        public string Photo { get; set; }
        public string Detay { get; set; }
    }
}
