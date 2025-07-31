namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale.Responses;

public record class CreateSaleProductResponse
{
    public string ExternalId { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string Category { get; init; } = string.Empty;
}