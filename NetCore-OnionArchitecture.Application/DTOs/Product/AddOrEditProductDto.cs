
namespace NetCore_OnionArchitecture.Application.DTOs.Product;

public class AddOrEditProductDto
{
    public int? ProductId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int Price { get; set; }
    public int Stock { get; set; }
    public int CategoryId { get; set; }
}
