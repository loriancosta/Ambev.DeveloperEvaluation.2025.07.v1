namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale.Results;

public record CreateSaleBranchResult
{
    public Guid Id { get; init; }
    public string ExternalId { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string City { get; init; } = string.Empty;
}
