using AutoMapper;
using ePizza.Application.Contracts;
using ePizza.Application.CustomExceptions;
using ePizza.Application.DTOs.Request;
using ePizza.Application.DTOs.Response;
using ePizza.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ePizza.Application.Implementation
{
    public class TokenGeneratorService : ITokenGeneratorService
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IUserTokenService _userTokenService;

        public TokenGeneratorService(IUserService userService,
            IConfiguration configuration,
            IMapper mapper,
            IUserTokenService userTokenService)
        {
            _userService = userService;
            this._configuration = configuration;
            _mapper = mapper;
            _userTokenService = userTokenService;
        }

        public async Task<TokenResponseDto> GenerateRefreshTokenAsync(RefreshTokenRequestDto request)
        {

            // check if my access token is valid

            var claimPrincipal
                 = GetTokenClaimPrincipal(request.AccessToken);

            if (claimPrincipal == null) throw new InvalidAccessTokenException("The provided access token is not valid");


            await ValidatePreviousTokenDetails(claimPrincipal, request);

            //
            var emailAddress = claimPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)!.Value;
            var userDetails = await _userService.GetUserAsync(emailAddress);
            return GenerateToken(userDetails);

        }

        public async Task<TokenResponseDto> GenerateTokenAsync(
            string userName,
            string password)
        {
            // validate user

            var user = await _userService.GetUserAsync(userName);

            if (user == null)
                throw new UserNotFoundException($"The provided email address {userName} doesn't exists in database.");

            // check pasword

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.Password);

            if (!isPasswordValid)
                throw new InvalidCredentialsException("The supplied password is invalid");

            // jwt

            var tokenResponseDto = GenerateToken(user);

            if (tokenResponseDto is not null)
            {
                await _userTokenService.PersistToken(new UserTokenRequestDto()
                {
                    AccessToken = tokenResponseDto.AccessToken,
                    RefreshToken = tokenResponseDto.RefreshToken,
                    UserId = user.Id
                });

                return tokenResponseDto;
            }

            return new TokenResponseDto();

        }


        private ClaimsPrincipal? GetTokenClaimPrincipal(string accessToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]!);

            var tokenValidationParametr
                 = new TokenValidationParameters()
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = false,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = _configuration["Jwt:Issuer"],
                     ValidAudience = _configuration["Jwt:Audience"],
                     IssuerSigningKey = new SymmetricSecurityKey(key)
                 };

            return tokenHandler.ValidateToken(accessToken, tokenValidationParametr, out _);
        }


        private TokenResponseDto GenerateToken(UserDomain userDomain)
        {
            var secretKey = _configuration["Jwt:Secret"];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor
                = new SecurityTokenDescriptor
                {
                    Subject = new System.Security.Claims.ClaimsIdentity([
                           new Claim(ClaimTypes.Name, userDomain.Name),
                           new Claim(ClaimTypes.Email, userDomain.Email),
                           new Claim("UserId", userDomain.Id.ToString())
                        ]),
                    Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(_configuration["Jwt:TokenExpiryInMinutes"])),
                    SigningCredentials = credentials,
                    Issuer = _configuration["Jwt:Issuer"],
                    Audience = _configuration["Jwt:Audience"]
                };


            var tokenHandler = new JsonWebTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);


            return new TokenResponseDto()
            {
                AccessToken = token,
                RefreshToken = GenerateRefreshToken()
            };
        }


        private string GenerateRefreshToken()
        {
            var randomBytes = new byte[64];

            using var range = RandomNumberGenerator.Create();
            range.GetBytes(randomBytes);

            return Convert.ToBase64String(randomBytes);
        }

        private async Task ValidatePreviousTokenDetails(
          ClaimsPrincipal principal,
          RefreshTokenRequestDto requestDto)
        {
            var previousTokenDetails =
                await FetchPreviousTokenDetails(principal);

            if (previousTokenDetails == null
                || previousTokenDetails.RefreshToken != requestDto.RefreshToken
                 || previousTokenDetails.AccessToken != requestDto.AccessToken
                || previousTokenDetails.RefreshTokenExpiryTime < DateTime.UtcNow)
                throw new Exception("Invalid Refresh Token Token");
        }

        private async Task<RefreshTokenResponseDto> FetchPreviousTokenDetails(
            ClaimsPrincipal claimsPrincipal)
        {
            var userId
                = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == "UserId")!.Value;

            var userTokenDomain = await _userService.GetUserTokenAsync(Convert.ToInt32(userId));
            return _mapper.Map<RefreshTokenResponseDto>(userTokenDomain);
        }
    }
}
