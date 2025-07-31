using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale.Responses;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleResponse
{
    public Guid Id { get; set; }
    public string SaleNumber { get; set; } = string.Empty;
    public DateTime SaleDate { get; set; }
    public CreateSaleCustomerResponse Customer { get; set; } = new();
    public CreateSaleBranchResponse Branch { get; set; } = new();
    public List<CreateSaleItemResponse> Items { get; set; } = [];
    public decimal TotalAmount { get; set; }
    public bool IsCancelled { get; set; }
}
