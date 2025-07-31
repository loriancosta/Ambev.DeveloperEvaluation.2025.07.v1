namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale.Requests;

public record class CancelSaleRequest
{
    public Guid Id { get; init; }
    public string CancellationReason { get; init; } = string.Empty;
}