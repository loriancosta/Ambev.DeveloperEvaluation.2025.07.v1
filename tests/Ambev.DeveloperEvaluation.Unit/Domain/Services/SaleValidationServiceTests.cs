using Ambev.DeveloperEvaluation.Domain.Services;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Services;

public class SaleValidationServiceTests
{
    private readonly SaleValidationService _validationService;

    public SaleValidationServiceTests()
    {
        _validationService = new SaleValidationService();
    }

    [Theory(DisplayName = "ValidateQuantity_WithDifferentValues_Should_ReturnCorrectValidation")]
    [InlineData(1, true)]
    [InlineData(10, true)]
    [InlineData(20, true)]
    [InlineData(21, false)]
    [InlineData(25, false)]
    public void ValidateQuantity_WithDifferentValues_Should_ReturnCorrectValidation(int quantity, bool expectedIsValid)
    {
        var result = _validationService.ValidateQuantity(quantity);

        result.IsValid.Should().Be(expectedIsValid);
        if (!expectedIsValid)
        {
            result.Errors.Should().NotBeEmpty();
            result.Errors.Should().Contain(e => e.Error == nameof(quantity));
        }
    }

    [Fact(DisplayName = "ValidateQuantity_WithValidQuantity_Should_ReturnValidResult")]
    public void ValidateQuantity_WithValidQuantity_Should_ReturnValidResult()
    {
        var result = _validationService.ValidateQuantity(15);

        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Theory(DisplayName = "ValidateSale_WithDifferentParameters_Should_ValidateCorrectly")]
    [InlineData("SALE123", true, true, true, true)]
    [InlineData("", true, true, true, false)]
    [InlineData("SALE123", false, true, true, false)]
    [InlineData("SALE123", true, false, true, false)]
    [InlineData("SALE123", true, true, false, false)]
    public void ValidateSale_WithDifferentParameters_Should_ValidateCorrectly(
        string saleNumber, bool hasCustomer, bool hasBranch, bool hasItems, bool expectedIsValid)
    {
        var customer = hasCustomer ? new object() : null;
        var branch = hasBranch ? new object() : null;

        var result = _validationService.ValidateSale(saleNumber, customer, branch, hasItems);

        result.IsValid.Should().Be(expectedIsValid);
        if (!expectedIsValid)
        {
            result.Errors.Should().NotBeEmpty();
        }
    }

    [Fact(DisplayName = "ValidateSale_WithAllValidParameters_Should_ReturnValidResult")]
    public void ValidateSale_WithAllValidParameters_Should_ReturnValidResult()
    {
        var result = _validationService.ValidateSale("SALE123", new object(), new object(), true);

        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Fact(DisplayName = "ValidateSale_WithNullCustomer_Should_ReturnInvalidWithCustomerError")]
    public void ValidateSale_WithNullCustomer_Should_ReturnInvalidWithCustomerError()
    {
        var result = _validationService.ValidateSale("SALE123", null, new object(), true);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.Error == "customer");
        result.Errors.Should().Contain(e => e.Detail == "Customer is required");
    }

    [Fact(DisplayName = "ValidateSale_WithEmptySaleNumber_Should_ReturnInvalidWithSaleNumberError")]
    public void ValidateSale_WithEmptySaleNumber_Should_ReturnInvalidWithSaleNumberError()
    {
        var result = _validationService.ValidateSale("", new object(), new object(), true);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.Error == "saleNumber");
        result.Errors.Should().Contain(e => e.Detail == "Sale number is required");
    }

    [Fact(DisplayName = "ValidateSale_WithMultipleErrors_Should_ReturnAllErrors")]
    public void ValidateSale_WithMultipleErrors_Should_ReturnAllErrors()
    {
        var result = _validationService.ValidateSale("", null, null, false);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(4);
        ValidateErrorsContainExpectedMessages(result.Errors);
    }

    private static void ValidateErrorsContainExpectedMessages(IEnumerable<Ambev.DeveloperEvaluation.Common.Validation.ValidationErrorDetail> errors)
    {
        var errorMessages = errors.Select(e => e.Detail).ToList();
        errorMessages.Should().Contain("Sale number is required");
        errorMessages.Should().Contain("Customer is required");
        errorMessages.Should().Contain("Branch is required");
        errorMessages.Should().Contain("Sale must have at least one item");
    }
}
