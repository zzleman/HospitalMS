using AutoMapper;
using ZeusMed.Core.Entities;
using ZeusMed.UI.Areas.ZeusMedAdmin.ViewModels.DonorViewModel;

namespace ZeusMed.UI.Mappers;

public class DonorProfile : Profile
{
    public DonorProfile()
    {
        CreateMap<DonorVM, Donor>().ReverseMap();
    }
}

