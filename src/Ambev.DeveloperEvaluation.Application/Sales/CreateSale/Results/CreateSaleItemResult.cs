namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale.Results;

public record CreateSaleItemResult
{
    public Guid Id { get; init; }
    public CreateSaleProductResult Product { get; init; } = new();
    public int Quantity { get; init; }
    public decimal UnitPrice { get; init; }
    public decimal Discount { get; init; }
    public decimal TotalAmount { get; init; }
    public bool IsCancelled { get; init; }
}
