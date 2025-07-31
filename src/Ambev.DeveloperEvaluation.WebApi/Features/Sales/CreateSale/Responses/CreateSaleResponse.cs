namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale.Responses;

public record class CreateSaleResponse
{
    public Guid Id { get; init; }
    public string SaleNumber { get; init; } = string.Empty;
    public DateTime SaleDate { get; init; }
    public CreateSaleCustomerResponse Customer { get; init; } = new();
    public CreateSaleBranchResponse Branch { get; init; } = new();
    public List<CreateSaleItemResponse> Items { get; init; } = [];
    public decimal TotalAmount { get; init; }
    public bool IsCancelled { get; init; }
}
