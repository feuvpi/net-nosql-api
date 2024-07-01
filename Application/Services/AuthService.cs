using api_reservas.Core.Dtos;
using api_reservas.Core.Interfaces;
using api_reservas.Core.Models;
using api_reservas.Core.Models.Config;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace api_reservas.Services
{
    public class AuthService : IAuthenticate
    {

        private UserService _userService;
        

        public AuthService(UserService userService) 
        { 
            _userService = userService;
        }
        public async Task<LoginResponseDto> Login(LoginDto loginDTO, JwtSettings _jwtSettings)
        {
            // -- retrieve user and authenticat
            var user = await _userService.FindByEmail(loginDTO.Email);
            if (user == null) throw new Exception("Invalid credentials.");
            loginDTO.Salt = user.PasswordSalt;
            if(loginDTO.Password != user.Password) throw new Exception("Invalid credentials.");

            // -- generate 
            var token = GenerateToken(user, _jwtSettings);

            var response = new LoginResponseDto
            {
                Token = token,
                Id = user.Id
            };

            return response;
        }


        public async Task<LoginResponseDto> CreateUserAsync(CreateUserDTO newUser, JwtSettings jwtSettings)
        {
            try
            {
                // -- email check
                var user = await _userService.FindByEmail(newUser.Email);
                if (user != null) throw new Exception("Email already in use.");

                // -- create new user
                Usuario createUser = new Usuario(newUser);
                var newUserId = await _userService.CreateAsync(createUser);   

                if(string.IsNullOrEmpty(newUserId.ToString())) throw new Exception("Database is unavailable. Please contact support.");
                createUser.Id = newUserId.ToString();

                // -- Uma colecao apenas, Usuario, que tera dentro dele o objeto condomino/condominio
                var token = GenerateToken(createUser, jwtSettings);
                var response = new LoginResponseDto
                {
                    Token = token,
                    Id = createUser.Id
                };
                
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public string GenerateToken(Usuario user, JwtSettings _jwtSettings)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                //new Claim(ClaimTypes.Actor, user.Funcao.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.TokenExpireInMinutes),
                SigningCredentials = credentials,
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(securityToken);

        }

        public async Task<GetMeResponseDto> ValidateTokenAndRetrieveUser(string token, JwtSettings _jwtSettings)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParams = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key)),
                    ValidIssuer = _jwtSettings.Issuer,
                    ValidAudience = _jwtSettings.Audience,
                };

                var principal = tokenHandler.ValidateToken(token, validationParams, out SecurityToken validatedToken);
                var userId = principal.FindFirst(ClaimTypes.NameIdentifier).Value;
                var isCondominio = bool.Parse(principal.FindFirst(ClaimTypes.Actor).Value);

                return new GetMeResponseDto { Id = userId, IsCondominio = isCondominio };
            }
            catch (Exception ex)
            {
                return null;
            }
        }



    }
}





