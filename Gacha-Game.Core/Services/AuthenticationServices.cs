﻿using Gacha_Game.Core.Domain.IdentityEntities;
using Gacha_Game.Core.Dtos.AuthenticationDto;
using Gacha_Game.Core.Helper;
using Gacha_Game.Core.ServiceContract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

public class AuthenticationServices : IAuthenticationServices
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly JwtDTO _jwt;
    private readonly IEmailSender _emailSender;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly SignInManager<ApplicationUser> _signInManager;
    public AuthenticationServices(UserManager<ApplicationUser> userManager,
                                  RoleManager<ApplicationRole> roleManager,
                                  IOptions<JwtDTO> jwt,
                                  IEmailSender emailSender,
                                  IHttpContextAccessor httpContextAccessor,
                                  SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _jwt = jwt.Value;
        _emailSender = emailSender;
        _httpContextAccessor = httpContextAccessor;
        _signInManager = signInManager;
    }

    public async Task<AuthenticationResponse> RegisterClientAsync(RegisterDTO clientRegisterDTO)
    {
        if (await _userManager.FindByEmailAsync(clientRegisterDTO.Email) != null)
            return new AuthenticationResponse { Message = "Email is already registered!" };

        var user = new ApplicationUser
        {
            UserName = clientRegisterDTO.UserName,
            Email = clientRegisterDTO.Email
        };

        var result = await _userManager.CreateAsync(user, clientRegisterDTO.Password);

        if (!result.Succeeded)
            return CreateErrorResponse(result.Errors);

        var roleName = clientRegisterDTO.Role == RolesOption.ADMIN ? "ADMIN" : "USER";
        var addRoleResult = await AddRoleToUserAsync(new AddRoleDTO { RoleName = roleName, UserID = user.Id });
        if (!string.IsNullOrEmpty(addRoleResult))
            return new AuthenticationResponse { Message = addRoleResult };

        var authenticationUser = await GenerateJwtToken(user);

        if (user.RefreshTokens.Any(x => x.IsActive))
        {
            var activeRefreshToken = user.RefreshTokens.FirstOrDefault(x => x.IsActive);
            authenticationUser.RefreshToken = activeRefreshToken.Token;
            authenticationUser.RefreshTokenExpiration = activeRefreshToken.ExpiredOn;
        }
        else
        {
            var otpCode = OtpHelper.GenerateOtp();
            user.OTPCode = otpCode;
            user.OTPExpiration = DateTime.UtcNow.AddMinutes(10);
            await _userManager.UpdateAsync(user);


            string emailBody = EmailBodyHelper.EmailBody("Confirm Your Email", "Thank you for registering with Gacha Game. To complete your registration, please confirm your email address by Using this code: ", user.Email, otpCode);
            await _emailSender.SendEmailAsync(user.Email, "Confirm Email", emailBody);

            var newRefreshToken = GenerateRefreshToken();
            authenticationUser.RefreshToken = newRefreshToken.Token;
            authenticationUser.RefreshTokenExpiration = newRefreshToken.ExpiredOn;
            user.RefreshTokens.Add(newRefreshToken);
            await _userManager.UpdateAsync(user);
        }
        authenticationUser.UserID = user.Id;


        return authenticationUser;
    }
    public async Task<AuthenticationResponse> GenerateJwtToken(ApplicationUser user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);
        var roleClaims = new List<Claim>();

        foreach (var role in roles)
            roleClaims.Add(new Claim("roles", role));

        var claims = new[]
        {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id.ToString())
            }
        .Union(userClaims)
        .Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwt.Issuer,
            audience: _jwt.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddDays(_jwt.DurationInDays),
            signingCredentials: signingCredentials);

        return new AuthenticationResponse
        {
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            Email = user.Email,
            Roles = roles.ToList(),
            Message = "Success Operation",
            RefreshTokenExpiration = DateTime.UtcNow.AddDays(_jwt.DurationInDays),
            IsAuthenticated = true
        };
    }

    public async Task<string> AddRoleToUserAsync(AddRoleDTO model)
    {
        var role = await _roleManager.FindByNameAsync(model.RoleName);
        if (role == null)
        {
            var createRoleResult = await _roleManager.CreateAsync(new ApplicationRole { Name = model.RoleName });
            if (!createRoleResult.Succeeded)
                return string.Join(", ", createRoleResult.Errors.Select(e => e.Description));
        }

        var user = await _userManager.FindByIdAsync(model.UserID.ToString());
        if (user == null)
            return "User not found.";

        if (await _userManager.IsInRoleAsync(user, model.RoleName))
            return "User is already assigned to this role.";

        var addRoleResult = await _userManager.AddToRoleAsync(user, model.RoleName);
        if (!addRoleResult.Succeeded)
            return string.Join(", ", addRoleResult.Errors.Select(e => e.Description));

        return null;
    }

    public async Task<AuthenticationResponse> LoginAsync(LoginDTO loginDTO)
    {
        var user = await _userManager.FindByEmailAsync(loginDTO.Email);

        if (user is null || !await _userManager.CheckPasswordAsync(user, loginDTO.Password))
        {
            return new AuthenticationResponse() { Message = "Email or Password is incorrect!" };
        }

        var authenticationUser = await GenerateJwtToken(user);

        if (user.RefreshTokens.Any(x => x.IsActive))
        {
            var activeRefreshToken = user.RefreshTokens.FirstOrDefault(x => x.IsActive);

            authenticationUser.RefreshToken = activeRefreshToken.Token;
            authenticationUser.RefreshTokenExpiration = activeRefreshToken.ExpiredOn;
        }
        else
        {
            var newRefreshToken = GenerateRefreshToken();

            authenticationUser.RefreshToken = newRefreshToken.Token;
            authenticationUser.RefreshTokenExpiration = newRefreshToken.ExpiredOn;
            user.RefreshTokens.Add(newRefreshToken);
            await _userManager.UpdateAsync(user);
        }
        authenticationUser.UserID = user.Id;

        return authenticationUser;
    }

    private AuthenticationResponse CreateErrorResponse(IEnumerable<IdentityError> errors)
    {
        var errorMessages = string.Join(", ", errors.Select(e => e.Description));
        return new AuthenticationResponse { Message = errorMessages };
    }

    private RefreshToken GenerateRefreshToken()
    {
        byte[] bytes = new byte[64];
        RandomNumberGenerator.Fill(bytes);
        return new RefreshToken()
        {
            CreatedOn = DateTime.UtcNow,
            ExpiredOn = DateTime.UtcNow.AddDays(120),
            Token = Convert.ToBase64String(bytes)
        };
    }

    public async Task<AuthenticationResponse> RefreshTokenAsync(string token)
    {
        var user = _userManager.Users
             .SingleOrDefault(x => x.RefreshTokens.Any(rt => rt.Token == token));

        if (user == null)
        {
            return new AuthenticationResponse() { Message = "Invalid token" };
        }

        var refreshToken = user.RefreshTokens.Single(rt => rt.Token == token);

        if (!refreshToken.IsActive)
        {
            return new AuthenticationResponse() { Message = "Inactive token" };
        }

        refreshToken.RevokedOn = DateTime.UtcNow;

        var newRefreshToken = GenerateRefreshToken();
        user.RefreshTokens.Add(newRefreshToken);

        await _userManager.UpdateAsync(user);

        var authenticationUser = await GenerateJwtToken(user);

        authenticationUser.RefreshToken = newRefreshToken.Token;
        authenticationUser.RefreshTokenExpiration = newRefreshToken.ExpiredOn;

        return authenticationUser;
    }

    public async Task<bool> RevokeTokenAsync(string token)
    {
        var user = _userManager.Users
            .SingleOrDefault(x => x.RefreshTokens.Any(x => x.Token == token));

        if (user == null)
            return false;

        var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

        if (!refreshToken.IsActive)
            return false;

        refreshToken.RevokedOn = DateTime.UtcNow;
        await _userManager.UpdateAsync(user);

        return true;
    }
    public async Task<bool> ForgotPassword(ForgotPasswordDTO forgotPasswordDTO)
    {
        var user = await _userManager.FindByEmailAsync(forgotPasswordDTO.Email!);

        if (user is null)
            return false;


        var otpCode = OtpHelper.GenerateOtp();


        user.OTPCode = otpCode;
        user.OTPExpiration = DateTime.UtcNow.AddMinutes(10);
        await _userManager.UpdateAsync(user);
        string emailBody = EmailBodyHelper.EmailBody("Reset Your Password", "We received a request to reset your password. Please use the following OTP code to proceed:", user.Email, otpCode);
        await _emailSender.SendEmailAsync(user.Email, "Reset Password OTP", emailBody);
        return true;
    }
    public async Task<UserDto?> GetUserByIdAsync(Guid userID)
    {
        var user = await _userManager.Users
            .FirstOrDefaultAsync(x => x.Id == userID);
        if (user == null)
            return null;

        var userdto = new UserDto
        {
            UserID = user.Id,
            Email = user.Email,
            UserName = user.UserName,
            Currancy = user.Currancy,
            Gems = user.Gems,
            Exp = user.Exp,
            Level = user.Level
        };
        return userdto;
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var users = await _userManager.Users.ToListAsync();
        var userDtos = users.Select(user => new UserDto
        {
            UserID = user.Id,
            Email = user.Email,
            UserName = user.UserName
        });
        return userDtos;
    }

    public async Task IncreaseCurrancy(IncreaseCurrancyDto model)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == model.UserID);
        if (user != null)
        {
            user.Currancy += model.Amount;
            await _userManager.UpdateAsync(user);
        }
        else
        {
            throw new Exception("User not found");
        }
    }
}