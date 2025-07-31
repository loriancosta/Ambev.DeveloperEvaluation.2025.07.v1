namespace Ambev.DeveloperEvaluation.Domain.Events;

public record SaleCancelledEvent
{
    public Guid SaleId { get; init; }
    public string SaleNumber { get; init; } = string.Empty;
    public DateTime CancelledAt { get; init; }
    public string CancellationReason { get; init; } = string.Empty;
}
