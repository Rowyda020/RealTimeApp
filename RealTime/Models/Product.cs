using System.Xml.Linq;

namespace RealTime.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; } 
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
