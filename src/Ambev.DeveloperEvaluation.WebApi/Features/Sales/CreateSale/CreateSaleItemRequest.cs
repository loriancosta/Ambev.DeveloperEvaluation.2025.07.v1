namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public record class CreateSaleItemRequest
{
    public string ProductExternalId { get; init; } = string.Empty;
    public string ProductName { get; init; } = string.Empty;
    public string ProductDescription { get; init; } = string.Empty;
    public string ProductCategory { get; init; } = string.Empty;
    public decimal ProductPrice { get; init; }
    public int Quantity { get; init; }
    public decimal UnitPrice { get; init; }
}