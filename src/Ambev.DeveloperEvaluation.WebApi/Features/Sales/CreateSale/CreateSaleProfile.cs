using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale.Dtos;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale.Results;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale.Responses;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleProfile : Profile
{
    public CreateSaleProfile()
    {
        CreateMap<CreateSaleRequest, CreateSaleCommand>()
            .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => new CreateSaleCustomerDto(
                src.CustomerExternalId,
                src.CustomerName,
                src.CustomerEmail,
                src.CustomerPhone,
                src.CustomerDocument)))
            .ForMember(dest => dest.Branch, opt => opt.MapFrom(src => new CreateSaleBranchDto(
                src.BranchExternalId,
                src.BranchName,
                src.BranchAddress,
                src.BranchCity,
                src.BranchState,
                src.BranchPostalCode)))
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

        CreateMap<CreateSaleItemRequest, CreateSaleItemDto>()
            .ForMember(dest => dest.Product, opt => opt.MapFrom(src => new CreateSaleProductDto(
                src.ProductExternalId,
                src.ProductName,
                src.ProductDescription,
                src.ProductCategory,
                src.ProductPrice)));

        CreateMap<CreateSaleResult, CreateSaleResponse>()
            .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer))
            .ForMember(dest => dest.Branch, opt => opt.MapFrom(src => src.Branch))
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

        CreateMap<CreateSaleCustomerResult, CreateSaleCustomerResponse>();
        CreateMap<CreateSaleBranchResult, CreateSaleBranchResponse>();
        CreateMap<CreateSaleItemResult, CreateSaleItemResponse>();
        CreateMap<CreateSaleProductResult, CreateSaleProductResponse>();
    }
}
