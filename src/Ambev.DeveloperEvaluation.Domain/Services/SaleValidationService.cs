using Ambev.DeveloperEvaluation.Common.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Services;

public class SaleValidationService : ISaleValidationService
{
    public ValidationResultDetail ValidateQuantity(int quantity)
    {
        var validationResult = new ValidationResultDetail { IsValid = true, Errors = [] };

        if (quantity > 20)
        {
            validationResult.IsValid = false;
            validationResult.Errors = validationResult.Errors.Append(new ValidationErrorDetail
            {
                Error = nameof(quantity),
                Detail = "Cannot sell more than 20 identical items"
            });
        }

        return validationResult;
    }

    public ValidationResultDetail ValidateSale(string saleNumber, object customer, object branch, bool hasItems)
    {
        var validationResult = new ValidationResultDetail { IsValid = true, Errors = [] };

        if (string.IsNullOrWhiteSpace(saleNumber))
        {
            validationResult.IsValid = false;
            validationResult.Errors = validationResult.Errors.Append(new ValidationErrorDetail
            {
                Error = nameof(saleNumber),
                Detail = "Sale number is required"
            });
        }

        if (customer == null)
        {
            validationResult.IsValid = false;
            validationResult.Errors = validationResult.Errors.Append(new ValidationErrorDetail
            {
                Error = nameof(customer),
                Detail = "Customer is required"
            });
        }

        if (branch == null)
        {
            validationResult.IsValid = false;
            validationResult.Errors = validationResult.Errors.Append(new ValidationErrorDetail
            {
                Error = nameof(branch),
                Detail = "Branch is required"
            });
        }

        if (!hasItems)
        {
            validationResult.IsValid = false;
            validationResult.Errors = validationResult.Errors.Append(new ValidationErrorDetail
            {
                Error = "Items",
                Detail = "Sale must have at least one item"
            });
        }

        return validationResult;
    }
}
