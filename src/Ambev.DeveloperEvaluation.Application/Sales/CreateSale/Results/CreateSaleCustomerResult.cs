namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale.Results;

public record CreateSaleCustomerResult
{
    public Guid Id { get; init; }
    public string ExternalId { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
}
