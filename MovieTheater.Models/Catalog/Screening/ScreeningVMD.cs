﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Models.Catalog.Screening
{
    public class ScreeningVMD
    {
        public int Id { get; set; }
        public DateTime TimeStart { get; set; }
        public string Film { get; set; }
        public string Room { get; set; }
        public string KindOfScreening { get; set; }
    }
}
