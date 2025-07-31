using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Events;

public record SaleCreatedEvent
{
    public Guid SaleId { get; init; }
    public string SaleNumber { get; init; } = string.Empty;
    public Customer Customer { get; init; } = new();
    public Branch Branch { get; init; } = new();
    public decimal TotalAmount { get; init; }
    public DateTime CreatedAt { get; init; }
    public List<SaleItem> Items { get; init; } = [];
}
