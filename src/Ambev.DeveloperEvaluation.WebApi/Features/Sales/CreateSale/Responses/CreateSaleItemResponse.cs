namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale.Responses;

public record class CreateSaleItemResponse
{
    public CreateSaleProductResponse Product { get; init; } = new();
    public int Quantity { get; init; }
    public decimal UnitPrice { get; init; }
    public decimal Discount { get; init; }
    public decimal TotalAmount { get; init; }
    public bool IsCancelled { get; init; }
}