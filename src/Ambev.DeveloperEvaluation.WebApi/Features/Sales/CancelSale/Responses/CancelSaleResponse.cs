namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale.Responses;

public record class CancelSaleResponse
{
    public Guid Id { get; init; }
    public string SaleNumber { get; init; } = string.Empty;
    public bool IsCancelled { get; init; }
    public string CancellationReason { get; init; } = string.Empty;
    public DateTime? UpdatedAt { get; init; }
    public decimal TotalAmount { get; init; }
}
