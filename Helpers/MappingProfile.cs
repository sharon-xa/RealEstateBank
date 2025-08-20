using AutoMapper;

using RealEstateBank.Data.Dtos.AcceptedCitizen;
using RealEstateBank.Data.Dtos.Advertisement;
using RealEstateBank.Data.Dtos.Bank;
using RealEstateBank.Data.Dtos.Branch;
using RealEstateBank.Data.Dtos.Service;
using RealEstateBank.Data.Dtos.User;
using RealEstateBank.Entities;

namespace RealEstateBank.Helpers;

public class MappingProfile : Profile {
    public MappingProfile() {
        CreateMap<AppUser, UserDto>();
        CreateMap<UserDto, AppUser>();
        CreateMap<RegisterForm, AppUser>();
        CreateMap<Bank, BankDto>();
        CreateMap<Advertisement, AdvertisementDto>();
        CreateMap<AdvertisementForm, Advertisement>();
        CreateMap<Service, ServiceDto>();
        CreateMap<ServiceDto, Service>();
        CreateMap<ServiceForm, Service>();
        CreateMap<AcceptedCitizen, AcceptedCitizenDto>();
        CreateMap<AcceptedCitizenDto, AcceptedCitizen>();
        CreateMap<Branch, BranchDto>();
        CreateMap<BranchDto, Branch>();
        CreateMap<BranchForm, Branch>();
    }
}
