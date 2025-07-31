namespace Ambev.DeveloperEvaluation.Domain.Services;

public class DiscountService : IDiscountService
{
    public decimal CalculateDiscount(int quantity) => quantity switch
    {
        >= 10 and <= 20 => 0.20m,
        >= 4 and < 10 => 0.10m,
        _ => 0m
    };
}
