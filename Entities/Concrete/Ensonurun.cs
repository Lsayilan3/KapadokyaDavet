using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Ensonurun :IEntity
    {
        public int EnsonurunId { get; set; }
        public string Photo { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
    }
}
