namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale.Results;

public record GetSaleResult
{
    public Guid Id { get; init; }
    public string SaleNumber { get; init; } = string.Empty;
    public DateTime SaleDate { get; init; }
    public GetSaleCustomerResult Customer { get; init; } = new();
    public GetSaleBranchResult Branch { get; init; } = new();
    public List<GetSaleItemResult> Items { get; init; } = [];
    public decimal TotalAmount { get; init; }
    public bool IsCancelled { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}
