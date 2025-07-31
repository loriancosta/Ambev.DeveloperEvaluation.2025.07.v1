using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale.Requests;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public record class CreateSaleRequest
{
    public string SaleNumber { get; init; } = string.Empty;
    public string CustomerExternalId { get; init; } = string.Empty;
    public string CustomerName { get; init; } = string.Empty;
    public string CustomerEmail { get; init; } = string.Empty;
    public string CustomerPhone { get; init; } = string.Empty;
    public string CustomerDocument { get; init; } = string.Empty;
    public string BranchExternalId { get; init; } = string.Empty;
    public string BranchName { get; init; } = string.Empty;
    public string BranchAddress { get; init; } = string.Empty;
    public string BranchCity { get; init; } = string.Empty;
    public string BranchState { get; init; } = string.Empty;
    public string BranchPostalCode { get; init; } = string.Empty;
    public List<CreateSaleItemRequest> Items { get; init; } = [];
}