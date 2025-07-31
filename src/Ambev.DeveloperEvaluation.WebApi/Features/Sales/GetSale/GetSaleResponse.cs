using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale.Responses;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

public class GetSaleResponse
{
    public Guid Id { get; set; }
    public string SaleNumber { get; set; } = string.Empty;
    public DateTime SaleDate { get; set; }
    public GetSaleCustomerResponse Customer { get; set; } = new();
    public GetSaleBranchResponse Branch { get; set; } = new();
    public List<GetSaleItemResponse> Items { get; set; } = [];
    public decimal TotalAmount { get; set; }
    public bool IsCancelled { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
