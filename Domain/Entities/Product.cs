using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class Product : BaseEntity
{
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string ProductNumber { get; set; } 
    public AppUser Owner { get; set; }
    [ForeignKey("Owner")]
    public string OwnerId { get; set; }
    
}