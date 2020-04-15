﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BandBookerWasm.Shared.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool AllDay { get; set; }
        public int Status { get; set; }
       
    }
}
