using AutoMapper;
using ZeusMed.Core.Entities;
using ZeusMed.UI.Areas.ZeusMedAdmin.ViewModels.DoctorViewModel;

namespace ZeusMed.UI.Mappers;

public class DoctorProfile : Profile
{
	public DoctorProfile()
	{
		CreateMap<DoctorPostVM, Doctor>().ReverseMap();
	}
}

