﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIE.Models
{
    public class MNewActivity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Weight { get; set; }
        public List<string> Files { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}
