namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale.Responses;

public class CancelSaleResponse
{
    public Guid Id { get; set; }
    public string SaleNumber { get; set; } = string.Empty;
    public bool IsCancelled { get; set; }
    public string CancellationReason { get; set; } = string.Empty;
    public DateTime? UpdatedAt { get; set; }
    public decimal TotalAmount { get; set; }
}
