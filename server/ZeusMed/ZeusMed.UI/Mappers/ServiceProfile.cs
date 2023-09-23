using AutoMapper;
using ZeusMed.Core.Entities;
using ZeusMed.UI.Areas.ZeusMedAdmin.ViewModels.ServiceViewModel;

namespace ZeusMed.UI.Mappers;

public class ServiceProfile : Profile
{
    public ServiceProfile()
    {
        CreateMap<ServicePostVM, Service>().ReverseMap();
    }
}

