namespace UserDataAccessService.Handlers.Commands.Token
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    using MediatR;

    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;

    using UserDataAccessService.Data.Contracts;
    using UserDataAccessService.Data.Models;
    using UserDataAccessService.Handlers.Commands.Response;

    public class CreateAccessTokenCommandHandler : IRequestHandler<CreateAccessTokenCommand, TokenResponse>
    {
        private readonly IConfiguration configuration;
        private readonly IRepository<RefreshToken> repository;

        public CreateAccessTokenCommandHandler(IConfiguration configuration, IRepository<RefreshToken> repository)
        {
            this.configuration = configuration;
            this.repository = repository;
        }

        public async Task<TokenResponse> Handle(CreateAccessTokenCommand request, CancellationToken cancellationToken)
        {
            DateTime expiration = DateTime.UtcNow.AddMinutes(int.Parse(configuration["Jwt:TokenValidityMins"]));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, request.Email),
                new Claim(JwtRegisteredClaimNames.Sub, request.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: creds
            );

            var refreshToken = new RefreshToken()
            {
                Token = Guid.NewGuid().ToString(),
                UserId = request.UserId,
                ExpiryDate = DateTime.UtcNow.AddMinutes(int.Parse(configuration["Jwt:RefreshTokenValidityMins"]))
            };

            await repository.AddAsync(refreshToken, cancellationToken);
            await repository.SaveChangesAsync(cancellationToken);

            return new TokenResponse()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresIn = (int)expiration.Subtract(DateTime.UtcNow).TotalSeconds,
                RefreshToken = refreshToken.Token
            };  
        }
    }
}
