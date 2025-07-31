using FluentValidation;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale.Dtos;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleValidator : AbstractValidator<CreateSaleCommand>
{
    public CreateSaleValidator()
    {
        RuleFor(sale => sale.SaleNumber)
            .NotEmpty()
            .WithMessage("Sale number is required")
            .MaximumLength(50)
            .WithMessage("Sale number cannot exceed 50 characters");

        RuleFor(sale => sale.Customer)
            .NotNull()
            .WithMessage("Customer information is required")
            .SetValidator(new CreateSaleCustomerValidator());

        RuleFor(sale => sale.Branch)
            .NotNull()
            .WithMessage("Branch information is required")
            .SetValidator(new CreateSaleBranchValidator());

        RuleFor(sale => sale.Items)
            .NotEmpty()
            .WithMessage("Sale must have at least one item");

        RuleForEach(sale => sale.Items)
            .SetValidator(new CreateSaleItemValidator());
    }
}

public class CreateSaleCustomerValidator : AbstractValidator<CreateSaleCustomerDto>
{
    public CreateSaleCustomerValidator()
    {
        RuleFor(customer => customer.ExternalId)
            .NotEmpty()
            .WithMessage("Customer external ID is required");

        RuleFor(customer => customer.Name)
            .NotEmpty()
            .WithMessage("Customer name is required")
            .MaximumLength(100)
            .WithMessage("Customer name cannot exceed 100 characters");

        RuleFor(customer => customer.Email)
            .NotEmpty()
            .WithMessage("Customer email is required")
            .EmailAddress()
            .WithMessage("Customer email must be a valid email address");

        RuleFor(customer => customer.Document)
            .NotEmpty()
            .WithMessage("Customer document is required");
    }
}

public class CreateSaleBranchValidator : AbstractValidator<CreateSaleBranchDto>
{
    public CreateSaleBranchValidator()
    {
        RuleFor(branch => branch.ExternalId)
            .NotEmpty()
            .WithMessage("Branch external ID is required");

        RuleFor(branch => branch.Name)
            .NotEmpty()
            .WithMessage("Branch name is required")
            .MaximumLength(100)
            .WithMessage("Branch name cannot exceed 100 characters");

        RuleFor(branch => branch.City)
            .NotEmpty()
            .WithMessage("Branch city is required");

        RuleFor(branch => branch.State)
            .NotEmpty()
            .WithMessage("Branch state is required");
    }
}

public class CreateSaleItemValidator : AbstractValidator<CreateSaleItemDto>
{
    public CreateSaleItemValidator()
    {
        RuleFor(item => item.Product)
            .NotNull()
            .WithMessage("Product information is required")
            .SetValidator(new CreateSaleProductValidator());

        RuleFor(item => item.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than 0")
            .LessThanOrEqualTo(20)
            .WithMessage("Cannot sell more than 20 identical items");

        RuleFor(item => item.UnitPrice)
            .GreaterThan(0)
            .WithMessage("Unit price must be greater than 0");
    }
}

public class CreateSaleProductValidator : AbstractValidator<CreateSaleProductDto>
{
    public CreateSaleProductValidator()
    {
        RuleFor(product => product.ExternalId)
            .NotEmpty()
            .WithMessage("Product external ID is required");

        RuleFor(product => product.Name)
            .NotEmpty()
            .WithMessage("Product name is required")
            .MaximumLength(100)
            .WithMessage("Product name cannot exceed 100 characters");

        RuleFor(product => product.Price)
            .GreaterThan(0)
            .WithMessage("Product price must be greater than 0");
    }
}
