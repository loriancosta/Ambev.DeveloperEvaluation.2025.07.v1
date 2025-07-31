namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale.Results;

public record GetSaleItemResult
{
    public Guid Id { get; init; }
    public GetSaleProductResult Product { get; init; } = new();
    public int Quantity { get; init; }
    public decimal UnitPrice { get; init; }
    public decimal Discount { get; init; }
    public decimal DiscountAmount { get; init; }
    public decimal TotalAmount { get; init; }
    public bool IsCancelled { get; init; }
}
