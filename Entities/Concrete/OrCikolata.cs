﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class OrCikolata :IEntity
    {
        public int OrCikolataId { get; set; }
        public string Photo { get; set; }
        public string Title { get; set; }
        public string Tag { get; set; }
        public string Detay { get; set; }
    }
}