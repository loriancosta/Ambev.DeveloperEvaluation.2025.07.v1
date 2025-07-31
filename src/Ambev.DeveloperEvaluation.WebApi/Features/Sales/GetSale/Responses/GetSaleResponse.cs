namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale.Responses;

public record class GetSaleResponse
{
    public Guid Id { get; init; }
    public string SaleNumber { get; init; } = string.Empty;
    public DateTime SaleDate { get; init; }
    public GetSaleCustomerResponse Customer { get; init; } = new();
    public GetSaleBranchResponse Branch { get; init; } = new();
    public List<GetSaleItemResponse> Items { get; init; } = [];
    public decimal TotalAmount { get; init; }
    public bool IsCancelled { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}
