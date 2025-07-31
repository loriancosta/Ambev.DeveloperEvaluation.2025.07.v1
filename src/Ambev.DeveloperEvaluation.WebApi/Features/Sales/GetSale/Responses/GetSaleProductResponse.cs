namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale.Responses;

public record class GetSaleProductResponse
{
    public string ExternalId { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string Category { get; init; } = string.Empty;
    public decimal Price { get; init; }
}