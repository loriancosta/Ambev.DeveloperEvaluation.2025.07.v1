using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale.Results;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

public class GetSaleProfile : Profile
{
    public GetSaleProfile()
    {
        CreateMap<Sale, GetSaleResult>();

        CreateMap<Customer, GetSaleCustomerResult>();

        CreateMap<Branch, GetSaleBranchResult>();

        CreateMap<SaleItem, GetSaleItemResult>();

        CreateMap<Product, GetSaleProductResult>();
    }
}
