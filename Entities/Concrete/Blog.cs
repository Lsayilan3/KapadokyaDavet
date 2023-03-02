using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Blog :IEntity
    {
        public int BlogId { get; set; }
        public string Photo { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string PostDate { get; set; }
        public string Author { get; set; }
    }
}
