namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale.Responses;

public record class CreateSaleBranchResponse
{
    public string ExternalId { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string City { get; init; } = string.Empty;
}