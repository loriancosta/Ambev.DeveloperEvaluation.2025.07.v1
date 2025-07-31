using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class SaleItem : BaseEntity
{
    public Product Product { get; set; } = new();
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public bool IsCancelled { get; set; }
    public decimal TotalAmount => IsCancelled ? 0 : Quantity * UnitPrice * (1 - Discount);
    public decimal DiscountAmount => IsCancelled ? 0 : Quantity * UnitPrice * Discount;
    public void Cancel() => IsCancelled = true;
}
