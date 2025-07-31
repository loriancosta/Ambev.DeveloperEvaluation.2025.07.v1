namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale.Responses;

public record class CreateSaleCustomerResponse
{
    public string ExternalId { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
}