namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale.Results;

public record CreateSaleProductResult
{
    public Guid Id { get; init; }
    public string ExternalId { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string Category { get; init; } = string.Empty;
}
