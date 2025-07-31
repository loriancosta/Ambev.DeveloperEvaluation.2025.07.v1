namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale.Responses;

public record class GetSaleBranchResponse
{
    public string ExternalId { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string Address { get; init; } = string.Empty;
    public string City { get; init; } = string.Empty;
    public string State { get; init; } = string.Empty;
    public string PostalCode { get; init; } = string.Empty;
}