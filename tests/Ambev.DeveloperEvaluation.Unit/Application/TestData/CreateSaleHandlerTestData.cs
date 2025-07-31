using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale.Dtos;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale.Results;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

public static class CreateSaleHandlerTestData
{
    private static readonly Faker<CreateSaleCommand> createSaleCommandFaker = new Faker<CreateSaleCommand>()
        .CustomInstantiator(f => new CreateSaleCommand(
            f.Commerce.Ean13(),
            new CreateSaleCustomerDto(
                f.Random.AlphaNumeric(10),
                f.Person.FullName,
                f.Internet.Email(),
                f.Phone.PhoneNumber("(##) #####-####"),
                f.Random.Replace("###.###.###-##")
            ),
            new CreateSaleBranchDto(
                f.Random.AlphaNumeric(10),
                f.Company.CompanyName(),
                f.Address.FullAddress(),
                f.Address.City(),
                f.Address.State(),
                f.Address.ZipCode()
            ),
            GenerateValidItems(f, 2)
        ));

    private static readonly Faker<Sale> saleFaker = new Faker<Sale>()
        .RuleFor(s => s.Id, f => f.Random.Guid())
        .RuleFor(s => s.SaleNumber, f => f.Commerce.Ean13())
        .RuleFor(s => s.SaleDate, f => f.Date.Recent())
        .RuleFor(s => s.Customer, f => GenerateValidCustomer(f))
        .RuleFor(s => s.Branch, f => GenerateValidBranch(f))
        .RuleFor(s => s.Items, f => GenerateValidSaleItems(f, 2))
        .RuleFor(s => s.IsCancelled, f => false)
        .RuleFor(s => s.CreatedAt, f => f.Date.Recent())
        .RuleFor(s => s.UpdatedAt, f => null);

    private static readonly Faker<CreateSaleResult> createSaleResultFaker = new Faker<CreateSaleResult>()
        .RuleFor(r => r.Id, f => f.Random.Guid())
        .RuleFor(r => r.SaleNumber, f => f.Commerce.Ean13())
        .RuleFor(r => r.SaleDate, f => f.Date.Recent())
        .RuleFor(r => r.Customer, f => GenerateValidCustomerResult(f))
        .RuleFor(r => r.Branch, f => GenerateValidBranchResult(f))
        .RuleFor(r => r.Items, f => GenerateValidItemResults(f, 2))
        .RuleFor(r => r.TotalAmount, f => f.Random.Decimal(10, 1000))
        .RuleFor(r => r.IsCancelled, f => false);

    public static CreateSaleCommand GenerateValidCommand()
    {
        return createSaleCommandFaker.Generate();
    }

    public static CreateSaleCommand GenerateCommandWithQuantity(int quantity)
    {
        var faker = new Faker();
        return new CreateSaleCommand(
            faker.Commerce.Ean13(),
            new CreateSaleCustomerDto(
                faker.Random.AlphaNumeric(10),
                faker.Person.FullName,
                faker.Internet.Email(),
                faker.Phone.PhoneNumber("(##) #####-####"),
                faker.Random.Replace("###.###.###-##")
            ),
            new CreateSaleBranchDto(
                faker.Random.AlphaNumeric(10),
                faker.Company.CompanyName(),
                faker.Address.FullAddress(),
                faker.Address.City(),
                faker.Address.State(),
                faker.Address.ZipCode()
            ),
            [
                new CreateSaleItemDto(
                    new CreateSaleProductDto(
                        faker.Random.AlphaNumeric(10),
                        faker.Commerce.ProductName(),
                        faker.Commerce.ProductDescription(),
                        faker.Commerce.Categories(1)[0],
                        faker.Random.Decimal(1, 100)
                    ),
                    quantity,
                    faker.Random.Decimal(1, 100)
                )
            ]
        );
    }

    public static Sale GenerateValidSale()
    {
        return saleFaker.Generate();
    }

    public static CreateSaleResult GenerateValidResult()
    {
        return createSaleResultFaker.Generate();
    }

    private static List<CreateSaleItemDto> GenerateValidItems(Faker faker, int count)
    {
        var items = new List<CreateSaleItemDto>();
        for (int i = 0; i < count; i++)
        {
            items.Add(new CreateSaleItemDto(
                new CreateSaleProductDto(
                    faker.Random.AlphaNumeric(10),
                    faker.Commerce.ProductName(),
                    faker.Commerce.ProductDescription(),
                    faker.Commerce.Categories(1)[0],
                    faker.Random.Decimal(1, 100)
                ),
                faker.Random.Int(1, 3),
                faker.Random.Decimal(1, 100)
            ));
        }
        return items;
    }

    private static Customer GenerateValidCustomer(Faker faker)
    {
        return new Customer
        {
            Id = faker.Random.Guid(),
            ExternalId = faker.Random.AlphaNumeric(10),
            Name = faker.Person.FullName,
            Email = faker.Internet.Email(),
            Phone = faker.Phone.PhoneNumber("(##) #####-####"),
            Document = faker.Random.Replace("###.###.###-##")
        };
    }

    private static Branch GenerateValidBranch(Faker faker)
    {
        return new Branch
        {
            Id = faker.Random.Guid(),
            ExternalId = faker.Random.AlphaNumeric(10),
            Name = faker.Company.CompanyName(),
            Address = faker.Address.FullAddress(),
            City = faker.Address.City(),
            State = faker.Address.State(),
            PostalCode = faker.Address.ZipCode(),
            IsActive = true
        };
    }

    private static List<SaleItem> GenerateValidSaleItems(Faker faker, int count)
    {
        var items = new List<SaleItem>();
        for (int i = 0; i < count; i++)
        {
            items.Add(new SaleItem
            {
                Id = faker.Random.Guid(),
                Product = new Product
                {
                    Id = faker.Random.Guid(),
                    ExternalId = faker.Random.AlphaNumeric(10),
                    Name = faker.Commerce.ProductName(),
                    Description = faker.Commerce.ProductDescription(),
                    Category = faker.Commerce.Categories(1)[0],
                    Price = faker.Random.Decimal(1, 100),
                    IsActive = true
                },
                Quantity = faker.Random.Int(1, 3),
                UnitPrice = faker.Random.Decimal(1, 100),
                Discount = 0,
                IsCancelled = false
            });
        }
        return items;
    }

    private static CreateSaleCustomerResult GenerateValidCustomerResult(Faker faker)
    {
        return new CreateSaleCustomerResult
        {
            ExternalId = faker.Random.AlphaNumeric(10),
            Name = faker.Person.FullName,
            Email = faker.Internet.Email()
        };
    }

    private static CreateSaleBranchResult GenerateValidBranchResult(Faker faker)
    {
        return new CreateSaleBranchResult
        {
            ExternalId = faker.Random.AlphaNumeric(10),
            Name = faker.Company.CompanyName(),
            City = faker.Address.City()
        };
    }

    private static List<CreateSaleItemResult> GenerateValidItemResults(Faker faker, int count)
    {
        var items = new List<CreateSaleItemResult>();
        for (int i = 0; i < count; i++)
        {
            items.Add(new CreateSaleItemResult
            {
                Product = new CreateSaleProductResult
                {
                    ExternalId = faker.Random.AlphaNumeric(10),
                    Name = faker.Commerce.ProductName(),
                    Category = faker.Commerce.Categories(1)[0]
                },
                Quantity = faker.Random.Int(1, 3),
                UnitPrice = faker.Random.Decimal(1, 100),
                Discount = 0,
                TotalAmount = faker.Random.Decimal(1, 300),
                IsCancelled = false
            });
        }
        return items;
    }
}
