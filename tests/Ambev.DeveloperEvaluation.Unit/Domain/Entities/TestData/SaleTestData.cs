using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

public static class SaleTestData
{
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

    private static readonly Faker<SaleItem> saleItemFaker = new Faker<SaleItem>()
        .RuleFor(si => si.Id, f => f.Random.Guid())
        .RuleFor(si => si.Product, f => GenerateValidProduct(f))
        .RuleFor(si => si.Quantity, f => f.Random.Int(1, 5))
        .RuleFor(si => si.UnitPrice, f => f.Random.Decimal(1, 100))
        .RuleFor(si => si.Discount, f => 0)
        .RuleFor(si => si.IsCancelled, f => false);

    private static readonly Faker<Customer> customerFaker = new Faker<Customer>()
        .RuleFor(c => c.Id, f => f.Random.Guid())
        .RuleFor(c => c.ExternalId, f => f.Random.AlphaNumeric(10))
        .RuleFor(c => c.Name, f => f.Person.FullName)
        .RuleFor(c => c.Email, f => f.Internet.Email())
        .RuleFor(c => c.Phone, f => f.Phone.PhoneNumber("(##) #####-####"))
        .RuleFor(c => c.Document, f => f.Random.Replace("###.###.###-##"));

    private static readonly Faker<Branch> branchFaker = new Faker<Branch>()
        .RuleFor(b => b.Id, f => f.Random.Guid())
        .RuleFor(b => b.ExternalId, f => f.Random.AlphaNumeric(10))
        .RuleFor(b => b.Name, f => f.Company.CompanyName())
        .RuleFor(b => b.Address, f => f.Address.FullAddress())
        .RuleFor(b => b.City, f => f.Address.City())
        .RuleFor(b => b.State, f => f.Address.State())
        .RuleFor(b => b.PostalCode, f => f.Address.ZipCode())
        .RuleFor(b => b.IsActive, f => true);

    private static readonly Faker<Product> productFaker = new Faker<Product>()
        .RuleFor(p => p.Id, f => f.Random.Guid())
        .RuleFor(p => p.ExternalId, f => f.Random.AlphaNumeric(10))
        .RuleFor(p => p.Name, f => f.Commerce.ProductName())
        .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
        .RuleFor(p => p.Category, f => f.Commerce.Categories(1)[0])
        .RuleFor(p => p.Price, f => f.Random.Decimal(1, 100))
        .RuleFor(p => p.IsActive, f => true);

    public static Sale GenerateValidSale() => saleFaker.Generate();

    public static SaleItem GenerateValidSaleItem() => saleItemFaker.Generate();

    public static Customer GenerateValidCustomer() => customerFaker.Generate();

    public static Branch GenerateValidBranch() => branchFaker.Generate();

    public static Product GenerateValidProduct() => productFaker.Generate();

    public static Sale GenerateSaleWithQuantity(int quantity)
    {
        var sale = GenerateValidSale();
        sale.Items.Clear();
        
        var item = GenerateValidSaleItem();
        item.Quantity = quantity;
        sale.Items.Add(item);
        
        return sale;
    }

    public static Sale GenerateSaleWithIdenticalItems(int quantity)
    {
        var sale = GenerateValidSale();
        sale.Items.Clear();
        
        var product = GenerateValidProduct();
        var item = new SaleItem
        {
            Id = Guid.NewGuid(),
            Product = product,
            Quantity = quantity,
            UnitPrice = 10.00m,
            Discount = 0,
            IsCancelled = false
        };
        
        sale.Items.Add(item);
        return sale;
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

    private static Product GenerateValidProduct(Faker faker)
    {
        return new Product
        {
            Id = faker.Random.Guid(),
            ExternalId = faker.Random.AlphaNumeric(10),
            Name = faker.Commerce.ProductName(),
            Description = faker.Commerce.ProductDescription(),
            Category = faker.Commerce.Categories(1)[0],
            Price = faker.Random.Decimal(1, 100),
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
                Product = GenerateValidProduct(faker),
                Quantity = faker.Random.Int(1, 3),
                UnitPrice = faker.Random.Decimal(1, 100),
                Discount = 0,
                IsCancelled = false
            });
        }
        return items;
    }
}
