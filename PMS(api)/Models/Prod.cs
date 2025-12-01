using System;
using System.Collections.Generic;

namespace PMS_api_.Models
{
    public partial class Prod
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
