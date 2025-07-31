namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale.Results;

public record CancelSaleResult
{
    public Guid Id { get; init; }
    public string SaleNumber { get; init; } = string.Empty;
    public bool IsCancelled { get; init; }
    public DateTime? UpdatedAt { get; init; }
    public string Message { get; init; } = string.Empty;
}
