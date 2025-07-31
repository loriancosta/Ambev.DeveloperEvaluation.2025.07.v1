using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale.Requests;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleRequest
{
    public string SaleNumber { get; set; } = string.Empty;
    public string CustomerExternalId { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerEmail { get; set; } = string.Empty;
    public string CustomerPhone { get; set; } = string.Empty;
    public string CustomerDocument { get; set; } = string.Empty;
    public string BranchExternalId { get; set; } = string.Empty;
    public string BranchName { get; set; } = string.Empty;
    public string BranchAddress { get; set; } = string.Empty;
    public string BranchCity { get; set; } = string.Empty;
    public string BranchState { get; set; } = string.Empty;
    public string BranchPostalCode { get; set; } = string.Empty;
    public List<CreateSaleItemRequest> Items { get; set; } = [];
}
