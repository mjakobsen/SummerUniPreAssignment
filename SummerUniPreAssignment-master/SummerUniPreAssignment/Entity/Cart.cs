using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SummerUniPreAssignment.Entity
{
    public class Cart
    {
        public Cart()
        {
            Items = new List<Item>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public List<Item> Items { get; set; }
    }
}
