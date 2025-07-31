namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale.Responses;

public class CreateSaleItemResponse
{
    public CreateSaleProductResponse Product { get; set; } = new();
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalAmount { get; set; }
    public bool IsCancelled { get; set; }
}
