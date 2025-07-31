using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Services;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Sale : BaseEntity
{
    private readonly ISaleValidationService? _validationService;
    private readonly IDiscountService? _discountService;

    public string SaleNumber { get; set; } = string.Empty;
    public DateTime SaleDate { get; set; }
    public Customer Customer { get; set; } = null!;
    public Branch Branch { get; set; } = null!;
    public List<SaleItem> Items { get; set; } = [];
    public bool IsCancelled { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public decimal TotalAmount => Items.Where(i => !i.IsCancelled).Sum(i => i.TotalAmount);

    public Sale()
    {
        CreatedAt = DateTime.UtcNow;
        SaleDate = DateTime.UtcNow;
    }

    public Sale(ISaleValidationService validationService, IDiscountService discountService) : this()
    {
        _validationService = validationService;
        _discountService = discountService;
    }

    public ValidationResultDetail AddItem(Product product, int quantity, decimal unitPrice)
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
            return validationResult;
        }

        decimal discount = _discountService?.CalculateDiscount(quantity) ?? quantity switch
        {
            >= 10 and <= 20 => 0.20m,
            >= 4 and < 10 => 0.10m,
            _ => 0m
        };

        var saleItem = new SaleItem
        {
            Product = product,
            Quantity = quantity,
            UnitPrice = unitPrice,
            Discount = discount
        };

        Items.Add(saleItem);
        UpdatedAt = DateTime.UtcNow;

        return validationResult;
    }

    public void Cancel()
    {
        if (!IsCancelled)
        {
            IsCancelled = true;
            UpdatedAt = DateTime.UtcNow;
        }
    }

    public void CancelItem(Guid itemId)
    {
        var item = Items.FirstOrDefault(i => i.Id == itemId);
        if (item != null)
        {
            item.Cancel();
            UpdatedAt = DateTime.UtcNow;
        }
    }

    public ValidationResultDetail Validate()
    {
        var validationResult = new ValidationResultDetail { IsValid = true, Errors = [] };

        if (string.IsNullOrWhiteSpace(SaleNumber))
        {
            validationResult.IsValid = false;
            validationResult.Errors = validationResult.Errors.Append(new ValidationErrorDetail
            {
                Error = nameof(SaleNumber),
                Detail = "Sale number is required"
            });
        }

        if (Customer == null)
        {
            validationResult.IsValid = false;
            validationResult.Errors = validationResult.Errors.Append(new ValidationErrorDetail
            {
                Error = nameof(Customer),
                Detail = "Customer is required"
            });
        }

        if (Branch == null)
        {
            validationResult.IsValid = false;
            validationResult.Errors = validationResult.Errors.Append(new ValidationErrorDetail
            {
                Error = nameof(Branch),
                Detail = "Branch is required"
            });
        }

        if (!Items.Any())
        {
            validationResult.IsValid = false;
            validationResult.Errors = validationResult.Errors.Append(new ValidationErrorDetail
            {
                Error = nameof(Items),
                Detail = "Sale must have at least one item"
            });
        }

        return validationResult;
    }
}
