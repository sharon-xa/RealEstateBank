using System.Net;

using AutoMapper;

using RealEstateBank.Data;
using RealEstateBank.Data.Dtos.User;
using RealEstateBank.Data.Enums;
using RealEstateBank.Entities;
using RealEstateBank.Interface;
using RealEstateBank.Utils;
using RealEstateBank.Utils.Exceptions;

namespace RealEstateBank.Services;

public interface IUserService {
    Task<UserDto> Login(LoginForm loginForm);
    Task<UserDto> DeleteUser(Guid id, Guid userId);
    Task<UserDto> Register(RegisterForm registerForm);
    Task<UserDto> UpdateUser(UpdateUserForm updateUserForm, Guid userId);
    Task<UserDto> ChangeMyPassword(ChangePasswordForm form, Guid id);
    Task<PaginatedResult<UserDto>> GetAll(UserFilter filter);
    Task<UserDto?> GetUserById(Guid id);
    Task<string> GetAccessToken(Guid? userId, DateTime? ExpiryDate);
    Task<bool?> UpdateUserRole(Guid userId, UserRole role);
}

public class UserService : IUserService {
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private readonly IRepositoryWrapper _repositoryWrapper;
    private readonly ITokenService _tokenService;

    public UserService(
        IRepositoryWrapper repositoryWrapper,
        IMapper mapper,
        ITokenService tokenService,
        DataContext context
    ) {
        _repositoryWrapper = repositoryWrapper;
        _mapper = mapper;
        _tokenService = tokenService;
        _context = context;
    }

    public async Task<UserDto> Register(RegisterForm registerForm) {

        var newUser = new AppUser {
            Email = registerForm.Email!,
            FullName = registerForm.FullName!,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerForm.Password),
            PhoneNumber = registerForm.PhoneNumber!,
            Role = UserRole.User,
            Gender = registerForm.Gender!.Value,
            Birthday = registerForm.Birthday!.Value,
        };

        newUser.RefreshToken = _tokenService.CreateRefreshToken(newUser);

        var user = await _repositoryWrapper.Users.CreateUser(newUser);

        var userDto = _mapper.Map<UserDto>(user);
        userDto.Token = _tokenService.CreateToken(user);
        return userDto;
    }

    public async Task<UserDto> Login(LoginForm loginForm) {
        if (string.IsNullOrWhiteSpace(loginForm.Email))
            throw new AppException("No email provided", nameof(UserService), nameof(Login), 400);

        var user = await _repositoryWrapper.Users.Get(u => u.Email.ToLower() == loginForm.Email.ToLower());

        if (user == null || user.Deleted)
            throw new AppException("Invalid credentials", nameof(UserService), nameof(Login), StatusCodes.Status401Unauthorized);

        if (!BCrypt.Net.BCrypt.Verify(loginForm.Password, user.PasswordHash))
            throw new AppException("Invalid credentials", nameof(UserService), nameof(Login), StatusCodes.Status401Unauthorized);

        user.RefreshToken = _tokenService.CreateRefreshToken(user);

        var userDto = _mapper.Map<UserDto>(user);
        userDto.Token = _tokenService.CreateToken(user);
        return userDto;
    }

    public Task<UserDto> UpdateUser(UpdateUserForm updateUserForm, Guid userId) {
        throw new NotImplementedException();
    }

    public Task<UserDto> ChangeMyPassword(ChangePasswordForm form, Guid id) {
        throw new NotImplementedException();
    }

    public Task<UserDto> DeleteUser(Guid id, Guid userId) {
        throw new NotImplementedException();
    }

    public Task<string> GetAccessToken(Guid? userId, DateTime? ExpiryDate) {
        throw new NotImplementedException();
    }

    public Task<PaginatedResult<UserDto>> GetAll(UserFilter filter) {
        throw new NotImplementedException();
    }

    public async Task<UserDto?> GetUserById(Guid id) {
        var userModel = await _repositoryWrapper.Users.GetById(id);
        if (userModel == null)
            return null;
        return _mapper.Map<UserDto>(userModel);
    }

    public async Task<bool?> UpdateUserRole(Guid userId, UserRole role) {
        var userModel = await _repositoryWrapper.Users.GetById(userId);
        if (userModel == null || userModel.Deleted)
            return null;

        userModel.Role = role;

        var updatedUser = await _repositoryWrapper.Users.UpdateUser(userModel);
        if (updatedUser == null) return false;

        return true;
    }
}
