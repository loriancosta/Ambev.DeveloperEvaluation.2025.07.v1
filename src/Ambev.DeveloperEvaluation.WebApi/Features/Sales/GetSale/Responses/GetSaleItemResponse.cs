namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale.Responses;

public class GetSaleItemResponse
{
    public GetSaleProductResponse Product { get; set; } = new();
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public bool IsCancelled { get; set; }
}
