using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class OrAcilis :IEntity
    {
        public int OrAcilisId { get; set; }
        public string Photo { get; set; }
        public string Detay { get; set; }
    }
}
