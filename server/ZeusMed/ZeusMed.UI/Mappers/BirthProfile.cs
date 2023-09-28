using AutoMapper;
using ZeusMed.Core.Entities;
using ZeusMed.UI.Areas.ZeusMedAdmin.ViewModels.BirthViewModel;

namespace ZeusMed.UI.Mappers;

public class BirthProfile : Profile
{
    public BirthProfile()
    {
        CreateMap<BirthPostVM, Birth>().ReverseMap();
    }
}

