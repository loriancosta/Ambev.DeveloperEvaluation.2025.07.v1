namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale.Responses;

public class GetSaleProductResponse
{
    public string ExternalId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
