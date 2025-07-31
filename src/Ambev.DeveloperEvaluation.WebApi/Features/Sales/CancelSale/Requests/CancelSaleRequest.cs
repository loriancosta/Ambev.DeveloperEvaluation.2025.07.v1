namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale.Requests;

public class CancelSaleRequest
{
    public Guid Id { get; set; }
    public string CancellationReason { get; set; } = string.Empty;
}
