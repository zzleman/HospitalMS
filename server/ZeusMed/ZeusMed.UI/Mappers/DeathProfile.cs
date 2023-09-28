using AutoMapper;
using ZeusMed.Core.Entities;
using ZeusMed.UI.Areas.ZeusMedAdmin.ViewModels.DeathViewModel;

namespace ZeusMed.UI.Mappers;

public class DeathProfile : Profile
{
    public DeathProfile()
    {
        CreateMap<DeathPostVM, Death>().ReverseMap();
    }
}

