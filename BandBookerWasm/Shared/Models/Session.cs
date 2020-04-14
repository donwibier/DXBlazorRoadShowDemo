using System;
using System.Collections.Generic;
using System.Text;

namespace BandBookerWasm.Shared.Models
{
    public class Session
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Event Event { get; set; }
    }
}
