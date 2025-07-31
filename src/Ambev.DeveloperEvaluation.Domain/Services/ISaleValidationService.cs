using Ambev.DeveloperEvaluation.Common.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Services;

public interface ISaleValidationService
{
    ValidationResultDetail ValidateQuantity(int quantity);
    ValidationResultDetail ValidateSale(string saleNumber, object customer, object branch, bool hasItems);
}
