using AutoMapper;
using RealEstateBank.Data;
using RealEstateBank.Data.Dtos.User;
using RealEstateBank.Data.Enums;
using RealEstateBank.Entities;
using RealEstateBank.Interface;
using RealEstateBank.Utils;

namespace RealEstateBank.Services;

public interface IUserService
{
    Task<UserDto> Login(LoginForm loginForm);
    Task<UserDto> DeleteUser(Guid id, Guid userId);
    Task<UserDto> Register(RegisterForm registerForm);
    Task<UserDto> UpdateUser(UpdateUserForm updateUserForm, Guid userId);
    Task<UserDto> ChangeMyPassword(ChangePasswordForm form, Guid id);
    Task<PaginatedResult<UserDto>> GetAll(UserFilter filter);
    Task<UserDto> GetUserById(Guid id);
    Task<string> GetAccessToken(Guid? userId, DateTime? ExpiryDate);
}

public class UserService : IUserService
{
    private readonly IRepositoryWrapper _repositoryWrapper;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;
    private readonly DataContext _context;

    public UserService(
        IRepositoryWrapper repositoryWrapper,
        IMapper mapper,
        ITokenService tokenService,
        DataContext context
    )
    {
        _repositoryWrapper = repositoryWrapper;
        _mapper = mapper;
        _tokenService = tokenService;
        _context = context;
    }

    public async Task<UserDto> Login(LoginForm loginForm)
    {
        if (string.IsNullOrWhiteSpace(loginForm.Email))
            throw new Exception("Please provide an email");

        var user = await _repositoryWrapper.Users.Get(
            u => u.Email != null && u.Email.ToLower() == loginForm.Email.ToLower()
        );

        if (user == null || user.Deleted == true)
            throw new Exception("User not found");

        if (!BCrypt.Net.BCrypt.Verify(loginForm.Password, user.PasswordHash))
            throw new Exception("Wrong password");

        var userDto = _mapper.Map<UserDto>(user);
        userDto.Token = _tokenService.CreateToken(userDto);
        userDto.RefreshToken = _tokenService.CreateRefreshToken(userDto);

        return userDto;
    }

    public async Task<UserDto> Register(RegisterForm registerForm)
    {
        var user = await _repositoryWrapper.Users.Get(u =>
            u.Email == registerForm.Email
        );
        if (user != null)
            throw new Exception("User already exists");

        var newUser = new AppUser
        {
            Email = registerForm.Email!,
            FullName = registerForm.FullName!,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerForm.Password),
            PhoneNumber = registerForm.PhoneNumber!,
            Role = UserRole.User,
            Gender = registerForm.Gender!.Value,
            Birthday = registerForm.Birthday!.Value,
        };

        await _repositoryWrapper.Users.CreateUser(newUser);

        var userDto = _mapper.Map<UserDto>(newUser);
        userDto.Token = _tokenService.CreateToken(userDto);
        userDto.RefreshToken = _tokenService.CreateRefreshToken(userDto);

        return userDto;
    }

    public Task<UserDto> ChangeMyPassword(ChangePasswordForm form, Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<UserDto> DeleteUser(Guid id, Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetAccessToken(Guid? userId, DateTime? ExpiryDate)
    {
        throw new NotImplementedException();
    }

    public Task<PaginatedResult<UserDto>> GetAll(UserFilter filter)
    {
        throw new NotImplementedException();
    }

    public Task<UserDto> GetUserById(Guid id)
    {
        throw new NotImplementedException();
    }


    public Task<UserDto> UpdateUser(UpdateUserForm updateUserForm, Guid userId)
    {
        throw new NotImplementedException();
    }
}