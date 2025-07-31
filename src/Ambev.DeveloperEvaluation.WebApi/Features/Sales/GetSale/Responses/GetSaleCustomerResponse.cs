namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale.Responses;

public record class GetSaleCustomerResponse
{
    public string ExternalId { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Phone { get; init; } = string.Empty;
    public string Document { get; init; } = string.Empty;
}