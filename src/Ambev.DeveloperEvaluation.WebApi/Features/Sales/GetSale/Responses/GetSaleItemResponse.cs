namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale.Responses;

public record class GetSaleItemResponse
{
    public GetSaleProductResponse Product { get; init; } = new();
    public int Quantity { get; init; }
    public decimal UnitPrice { get; init; }
    public decimal Discount { get; init; }
    public decimal DiscountAmount { get; init; }
    public decimal TotalAmount { get; init; }
    public bool IsCancelled { get; init; }
}