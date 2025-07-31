namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale.Results;

public record CreateSaleResult
{
    public Guid Id { get; init; }
    public string SaleNumber { get; init; } = string.Empty;
    public DateTime SaleDate { get; init; }
    public CreateSaleCustomerResult Customer { get; init; } = new();
    public CreateSaleBranchResult Branch { get; init; } = new();
    public List<CreateSaleItemResult> Items { get; init; } = [];
    public decimal TotalAmount { get; init; }
    public bool IsCancelled { get; init; }
    public DateTime CreatedAt { get; init; }
}
