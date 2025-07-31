using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Events;

/// <summary>
/// Event raised when an item in a sale is cancelled.
/// </summary>
public record ItemCancelledEvent
{
    /// <summary>
    /// Gets the ID of the sale containing the cancelled item.
    /// </summary>
    public Guid SaleId { get; init; }

    /// <summary>
    /// Gets the sale number.
    /// </summary>
    public string SaleNumber { get; init; } = string.Empty;

    /// <summary>
    /// Gets the ID of the cancelled item.
    /// </summary>
    public Guid ItemId { get; init; }

    /// <summary>
    /// Gets the cancelled item information.
    /// </summary>
    public SaleItem CancelledItem { get; init; } = new();

    /// <summary>
    /// Gets the date when the item was cancelled.
    /// </summary>
    public DateTime CancelledAt { get; init; }

    /// <summary>
    /// Gets the reason for item cancellation.
    /// </summary>
    public string CancellationReason { get; init; } = string.Empty;
}
