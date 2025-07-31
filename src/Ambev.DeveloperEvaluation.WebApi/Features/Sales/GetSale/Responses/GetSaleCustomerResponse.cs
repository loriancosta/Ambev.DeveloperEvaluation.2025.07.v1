namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale.Responses;

public class GetSaleCustomerResponse
{
    public string ExternalId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Document { get; set; } = string.Empty;
}
