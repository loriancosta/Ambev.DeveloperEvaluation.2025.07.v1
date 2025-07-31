using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
{
    public CreateSaleRequestValidator()
    {
        RuleFor(x => x.SaleNumber)
            .NotEmpty()
            .WithMessage("Sale number is required")
            .MaximumLength(50)
            .WithMessage("Sale number cannot exceed 50 characters");

        RuleFor(x => x.CustomerExternalId)
            .NotEmpty()
            .WithMessage("Customer external ID is required")
            .MaximumLength(50)
            .WithMessage("Customer external ID cannot exceed 50 characters");

        RuleFor(x => x.CustomerName)
            .NotEmpty()
            .WithMessage("Customer name is required")
            .MaximumLength(100)
            .WithMessage("Customer name cannot exceed 100 characters");

        RuleFor(x => x.CustomerEmail)
            .NotEmpty()
            .WithMessage("Customer email is required")
            .EmailAddress()
            .WithMessage("Customer email must be a valid email address")
            .MaximumLength(100)
            .WithMessage("Customer email cannot exceed 100 characters");

        RuleFor(x => x.CustomerDocument)
            .NotEmpty()
            .WithMessage("Customer document is required")
            .MaximumLength(20)
            .WithMessage("Customer document cannot exceed 20 characters");

        RuleFor(x => x.BranchExternalId)
            .NotEmpty()
            .WithMessage("Branch external ID is required")
            .MaximumLength(50)
            .WithMessage("Branch external ID cannot exceed 50 characters");

        RuleFor(x => x.BranchName)
            .NotEmpty()
            .WithMessage("Branch name is required")
            .MaximumLength(100)
            .WithMessage("Branch name cannot exceed 100 characters");

        RuleFor(x => x.BranchCity)
            .NotEmpty()
            .WithMessage("Branch city is required")
            .MaximumLength(50)
            .WithMessage("Branch city cannot exceed 50 characters");

        RuleFor(x => x.BranchState)
            .NotEmpty()
            .WithMessage("Branch state is required")
            .MaximumLength(50)
            .WithMessage("Branch state cannot exceed 50 characters");

        RuleFor(x => x.Items)
            .NotEmpty()
            .WithMessage("At least one item is required")
            .Must(items => items.Count > 0)
            .WithMessage("Sale must contain at least one item");

        RuleForEach(x => x.Items)
            .SetValidator(new CreateSaleItemRequestValidator());
    }
}

/// <summary>
/// Validator for CreateSaleItemRequest
/// </summary>
public class CreateSaleItemRequestValidator : AbstractValidator<CreateSaleItemRequest>
{
    /// <summary>
    /// Initializes validation rules for CreateSaleItemRequest
    /// </summary>
    public CreateSaleItemRequestValidator()
    {
        RuleFor(x => x.ProductExternalId)
            .NotEmpty()
            .WithMessage("Product external ID is required")
            .MaximumLength(50)
            .WithMessage("Product external ID cannot exceed 50 characters");

        RuleFor(x => x.ProductName)
            .NotEmpty()
            .WithMessage("Product name is required")
            .MaximumLength(100)
            .WithMessage("Product name cannot exceed 100 characters");

        RuleFor(x => x.ProductPrice)
            .GreaterThan(0)
            .WithMessage("Product price must be greater than zero");

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than zero")
            .LessThanOrEqualTo(20)
            .WithMessage("Cannot sell more than 20 identical items");

        RuleFor(x => x.UnitPrice)
            .GreaterThan(0)
            .WithMessage("Unit price must be greater than zero");
    }
}
