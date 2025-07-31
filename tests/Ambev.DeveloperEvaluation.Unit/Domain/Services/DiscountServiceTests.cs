using Ambev.DeveloperEvaluation.Domain.Services;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Services;

public class DiscountServiceTests
{
    private readonly DiscountService _discountService;

    public DiscountServiceTests()
    {
        _discountService = new DiscountService();
    }

    [Theory(DisplayName = "CalculateDiscount_WithDifferentQuantities_Should_ReturnCorrectDiscount")]
    [InlineData(1, 0.0)]
    [InlineData(3, 0.0)]
    [InlineData(4, 0.10)]
    [InlineData(9, 0.10)]
    [InlineData(10, 0.20)]
    [InlineData(20, 0.20)]
    [InlineData(21, 0.0)]
    public void CalculateDiscount_WithDifferentQuantities_Should_ReturnCorrectDiscount(int quantity, decimal expectedDiscount)
    {
        var result = _discountService.CalculateDiscount(quantity);

        result.Should().Be(expectedDiscount);
    }

    [Fact(DisplayName = "CalculateDiscount_WithZeroQuantity_Should_ReturnZeroDiscount")]
    public void CalculateDiscount_WithZeroQuantity_Should_ReturnZeroDiscount()
    {
        var result = _discountService.CalculateDiscount(0);

        result.Should().Be(0.0m);
    }

    [Fact(DisplayName = "CalculateDiscount_WithNegativeQuantity_Should_ReturnZeroDiscount")]
    public void CalculateDiscount_WithNegativeQuantity_Should_ReturnZeroDiscount()
    {
        var result = _discountService.CalculateDiscount(-5);

        result.Should().Be(0.0m);
    }

    [Theory(DisplayName = "CalculateDiscount_WithBoundaryValues_Should_ReturnCorrectDiscount")]
    [InlineData(3, 0.0)]
    [InlineData(4, 0.10)]
    [InlineData(9, 0.10)]
    [InlineData(10, 0.20)]
    [InlineData(20, 0.20)]
    [InlineData(21, 0.0)]
    public void CalculateDiscount_WithBoundaryValues_Should_ReturnCorrectDiscount(int quantity, decimal expectedDiscount)
    {
        var result = _discountService.CalculateDiscount(quantity);

        result.Should().Be(expectedDiscount);
        ValidateDiscountRange(result);
    }

    private static void ValidateDiscountRange(decimal discount)
    {
        discount.Should().BeGreaterOrEqualTo(0.0m);
        discount.Should().BeLessOrEqualTo(1.0m);
    }
}
