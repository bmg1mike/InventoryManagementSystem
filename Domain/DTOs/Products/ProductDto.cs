namespace Domain;

public record AddProductDto(string ProductName,
                            int Quantity,
                            decimal Price);

public class GetProductssDto
{
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
