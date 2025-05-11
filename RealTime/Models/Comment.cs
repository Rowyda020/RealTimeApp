namespace RealTime.Models
{
    public class Comment
    {
      
            public int Id { get; set; }
            public string Content { get; set; }
            public DateTime CreatedAt { get; set; }
            public string UserName { get; set; } // Added to store the username
            public int ProductId { get; set; }
            public Product Product { get; set; }
        
    }
}
