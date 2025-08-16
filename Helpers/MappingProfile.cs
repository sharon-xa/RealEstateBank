using AutoMapper;

using RealEstateBank.Data.Dtos.Bank;
using RealEstateBank.Data.Dtos.User;
using RealEstateBank.Entities;

namespace RealEstateBank.Helpers;

public class MappingProfile : Profile {
    public MappingProfile() {
        CreateMap<AppUser, UserDto>();
        CreateMap<UserDto, AppUser>();
        CreateMap<RegisterForm, AppUser>();
        CreateMap<Bank, BankDto>();
    }
}
