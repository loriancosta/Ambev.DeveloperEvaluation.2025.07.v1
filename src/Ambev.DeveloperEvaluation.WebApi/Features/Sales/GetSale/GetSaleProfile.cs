using Ambev.DeveloperEvaluation.Application.Sales.GetSale.Results;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale.Responses;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

public class GetSaleProfile : Profile
{

    public GetSaleProfile()
    {
        CreateMap<GetSaleResult, GetSaleResponse>()
            .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer))
            .ForMember(dest => dest.Branch, opt => opt.MapFrom(src => src.Branch))
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

        CreateMap<GetSaleCustomerResult, GetSaleCustomerResponse>();
        CreateMap<GetSaleBranchResult, GetSaleBranchResponse>();
        CreateMap<GetSaleItemResult, GetSaleItemResponse>();
        CreateMap<GetSaleProductResult, GetSaleProductResponse>();
    }
}
