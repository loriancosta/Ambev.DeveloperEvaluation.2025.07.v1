using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class SaleTests
{
    [Fact(DisplayName = "TotalAmount_WithMultipleItems_Should_ReturnCorrectSum")]
    public void TotalAmount_WithMultipleItems_Should_ReturnCorrectSum()
    {
        var sale = SaleTestData.GenerateValidSale();
        var expectedTotal = sale.Items.Sum(i => i.TotalAmount);

        var actualTotal = sale.TotalAmount;

        actualTotal.Should().Be(expectedTotal);
    }

    [Fact(DisplayName = "TotalAmount_WithNoItems_Should_ReturnZero")]
    public void TotalAmount_WithNoItems_Should_ReturnZero()
    {
        var sale = SaleTestData.GenerateValidSale();
        sale.Items.Clear();

        var actualTotal = sale.TotalAmount;

        actualTotal.Should().Be(0);
    }

    [Fact(DisplayName = "Cancel_WithActiveSale_Should_SetIsCancelledToTrue")]
    public void Cancel_WithActiveSale_Should_SetIsCancelledToTrue()
    {
        var sale = SaleTestData.GenerateValidSale();
        sale.IsCancelled.Should().BeFalse();

        sale.Cancel();

        sale.IsCancelled.Should().BeTrue();
        sale.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact(DisplayName = "Cancel_WithAlreadyCancelledSale_Should_RemainCancelled")]
    public void Cancel_WithAlreadyCancelledSale_Should_RemainCancelled()
    {
        var sale = SaleTestData.GenerateValidSale();
        sale.Cancel();
        var firstCancellationTime = sale.UpdatedAt;

        sale.Cancel();

        sale.IsCancelled.Should().BeTrue();
        sale.UpdatedAt.Should().Be(firstCancellationTime);
    }

    [Fact(DisplayName = "AddItem_WithValidItem_Should_AddToCollection")]
    public void AddItem_WithValidItem_Should_AddToCollection()
    {
        var sale = SaleTestData.GenerateValidSale();
        var initialCount = sale.Items.Count;
        var product = SaleTestData.GenerateValidProduct();

        var result = sale.AddItem(product, 2, 10.00m);

        result.IsValid.Should().BeTrue();
        sale.Items.Should().HaveCount(initialCount + 1);
        sale.Items.Last().Product.Should().Be(product);
        sale.Items.Last().Quantity.Should().Be(2);
        sale.Items.Last().UnitPrice.Should().Be(10.00m);
    }

    [Fact(DisplayName = "AddItem_WithMoreThan20Items_Should_ReturnValidationError")]
    public void AddItem_WithMoreThan20Items_Should_ReturnValidationError()
    {
        var sale = SaleTestData.GenerateValidSale();
        var product = SaleTestData.GenerateValidProduct();

        var result = sale.AddItem(product, 25, 10.00m);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(1);
        result.Errors.First().Detail.Should().Contain("Cannot sell more than 20 identical items");
    }

    [Theory(DisplayName = "SaleNumber_WithInvalidValue_Should_AllowCreationButFailValidation")]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void SaleNumber_WithInvalidValue_Should_AllowCreationButFailValidation(string invalidSaleNumber)
    {
        var sale = new Sale
        {
            SaleNumber = invalidSaleNumber,
            Customer = SaleTestData.GenerateValidCustomer(),
            Branch = SaleTestData.GenerateValidBranch(),
            Items = []
        };

        var result = sale.Validate();

        result.IsValid.Should().BeFalse();
    }

    [Fact(DisplayName = "Customer_WithNullValue_Should_AllowCreationButFailValidation")]
    public void Customer_WithNullValue_Should_AllowCreationButFailValidation()
    {
        var sale = new Sale
        {
            SaleNumber = "SALE-001",
            Customer = null!,
            Branch = SaleTestData.GenerateValidBranch(),
            Items = []
        };

        var result = sale.Validate();

        result.IsValid.Should().BeFalse();
    }

    [Fact(DisplayName = "Branch_WithNullValue_Should_AllowCreationButFailValidation")]
    public void Branch_WithNullValue_Should_AllowCreationButFailValidation()
    {
        var sale = new Sale
        {
            SaleNumber = "SALE-001",
            Customer = SaleTestData.GenerateValidCustomer(),
            Branch = null!,
            Items = []
        };

        var result = sale.Validate();

        result.IsValid.Should().BeFalse();
    }

    [Fact(DisplayName = "SaleDate_WithNewSale_Should_SetToCurrentDate")]
    public void SaleDate_WithNewSale_Should_SetToCurrentDate()
    {
        var sale = new Sale
        {
            SaleNumber = "SALE-001",
            Customer = SaleTestData.GenerateValidCustomer(),
            Branch = SaleTestData.GenerateValidBranch(),
            Items = []
        };

        sale.SaleDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }
}
