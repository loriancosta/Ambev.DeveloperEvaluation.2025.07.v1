namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale.Requests;

public class CreateSaleItemRequest
{
    public string ProductExternalId { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public string ProductDescription { get; set; } = string.Empty;
    public string ProductCategory { get; set; } = string.Empty;
    public decimal ProductPrice { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
