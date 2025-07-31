using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Events;

public record SaleModifiedEvent
{
    public Guid SaleId { get; init; }
    public string SaleNumber { get; init; } = string.Empty;
    public decimal TotalAmount { get; init; }
    public DateTime ModifiedAt { get; init; }
    public List<SaleItem> Items { get; init; } = [];
}
