using AutoMapper;
using BuildingBlocks.Domain.Data;
using BuildingBlocks.EventBus.Interfaces;
using IAC.Application.Dtos.Authentication;
using IAC.Application.Dtos.Users;
using IAC.Application.IntegrationEvents.Events;
using IAC.Application.Services.Interfaces;
using IAC.Domain.Constants;
using IAC.Domain.Entities;
using IAC.Domain.Exceptions.Authentication;
using IAC.Domain.Exceptions.Roles;
using IAC.Domain.Exceptions.Users;
using IAC.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace IAC.Application.Services;

public class AuthenticateService : IAuthenticateService
{
    private readonly IUserRepository _userRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventBus _eventBus;
    private readonly ILogger<AuthenticateService> _logger;

    public AuthenticateService(IUserRepository userRepository,
                               ITokenService tokenService,
                               UserManager<ApplicationUser> userManager,
                               IMapper mapper,
                               IUnitOfWork unitOfWork, 
                               IEventBus eventBus, 
                               ILogger<AuthenticateService> logger)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _userManager = userManager;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _eventBus = eventBus;
        _logger = logger;
    }
    public async Task SignUpAsync(SignUpDto signUpDto)
    {
        var isUserExist = await _userRepository.IsPhoneExist(signUpDto.PhoneNumber)
            && await _userRepository.IsEmailExist(signUpDto.Email);
        if (isUserExist)
            throw new UserExistException("User is already exist");
        var user = new ApplicationUser
        {
            FullName = signUpDto.FullName,
            UserName = signUpDto.PhoneNumber,
            PhoneNumber = signUpDto.PhoneNumber,
            Email = signUpDto.Email,
            PasswordHash = signUpDto.Password
        };

        await _unitOfWork.BeginTransactionAsync();
        
        try
        {
            var result = await _userManager.CreateAsync(user, signUpDto.Password);
            if (!result.Succeeded)
                throw new UserCreateFailException(result.Errors.First().Description);   
            
            var addRoleResult = await _userManager.AddToRoleAsync(user, AppRole.Customer);
            if (!addRoleResult.Succeeded)
                throw new RoleNotFoundException(nameof(ApplicationRole.Name), AppRole.Customer);
            
            var eventMessage = new UserSignedUpIntegrationEvent(Guid.Parse(user.Id), signUpDto.FullName, signUpDto.Email, signUpDto.PhoneNumber);
            
            try 
            {
                _eventBus.Publish(eventMessage);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error Publishing integration event: {IntegrationEventId}", eventMessage.Id);
                throw;
            }
            
            await _unitOfWork.CommitTransactionAsync();
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task<TokenDto> RefreshTokenAsync(RefreshTokenDto refreshTokenDto)
    {
        await _tokenService.ValidateRefreshTokenAsync(refreshTokenDto.RefreshToken);
        
        var user = await _userRepository.GetByRefreshTokenAsync(refreshTokenDto.RefreshToken)
                   ?? throw new RefreshTokenNotFoundException();
        
        var tokenDto = new TokenDto
        {
            AccessToken = await _tokenService.GenerateAccessTokenAsync(user.Id),
            RefreshToken = _tokenService.GenerateRefreshToken()
        };
        
        await _tokenService.RevokeRefreshTokenAsync(refreshTokenDto.RefreshToken);
        
        await _tokenService.AddRefreshTokenAsync(user.Id, tokenDto.RefreshToken);
        
        return tokenDto;
    }
    
    public async Task<TokenDto> LoginAsync(LoginDto logInDto)
    {
        var user = await _userRepository.GetByPhoneNumberAsync(logInDto.Identifier) 
                   ?? await _userRepository.GetByEmailAsync(logInDto.Identifier);
        
        if (user == null)
            throw new UserNotFoundException(logInDto.Identifier);
        
        var isValid = await _userManager.CheckPasswordAsync(user, logInDto.Password);
        if (!isValid)
            throw new InvalidPasswordException();
        
        var userDto = _mapper.Map<UserDto>(user);

        var tokenDto = new TokenDto
        {
            AccessToken = await _tokenService.GenerateAccessTokenAsync(user.Id),
            RefreshToken = _tokenService.GenerateRefreshToken(),
            User = userDto
        };
        
        await _tokenService.AddRefreshTokenAsync(user.Id, tokenDto.RefreshToken);
        
        return tokenDto;
    } 
}