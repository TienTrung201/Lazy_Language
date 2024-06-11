using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using NTTRUNG_Laze_Languages_Application.Interface.Service;
using NTTRUNG_Laze_Languages_Application.Dtos.Entity.Account;
using AutoMapper;
using NTTRUNG_Laze_Languages_Domain.Interface.Repository;
using NTTRUNG_Laze_Languages_Domain.Interface.UnitOfWork;
using NTTRUNG_Laze_Languages_Application.Dtos.Entity;
using NTTRUNG_Laze_Languages_Domain.Model;
using NTTRUNG_Laze_Languages_Domain.Enum;
using NTTRUNG_Laze_Languages_Domain.Resources.ErrorMessage;
using NTTRUNG_Laze_Languages_Domain;
using Microsoft.Extensions.Configuration;
namespace NTTRUNG_Laze_Languages_Application.Service
{
    public class AuthService : IAuthService
    {
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public AuthService(IUserService userService, IMapper mapper, IUserRepository userRepository, IConfiguration configuration)
        {
            _userService = userService;
            _mapper = mapper;
            _userRepository = userRepository;
            _config = configuration;
            _secretKey = _config["JWT:SecretKey"];
            _issuer = _config["JWT:Issuer"];
        }
        public string GenerateJwtToken(string userCode)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, userCode) }),
                Expires = DateTime.UtcNow.AddHours(1), // Token hết hạn sau 1 giờ
                Issuer = _issuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
             return tokenHandler.WriteToken(token);
        }

        public bool ValidateJwtToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                }, out SecurityToken validatedToken);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Method to authenticate user - Check credentials and return JWT if valid
        public async Task<string> AuthenticateUser(LoginDto loginDto)
        {
            var user = new UserModel();
            if (!string.IsNullOrWhiteSpace(loginDto.UserCode))
            {
                user = await _userRepository.GetUserByCodeOrEmail(userCode: loginDto.UserCode);
            }else if (!string.IsNullOrWhiteSpace(loginDto.Email))
            {
                user = await _userRepository.GetUserByCodeOrEmail(email: loginDto.Email);
            }
            // Validate username and password (you might want to retrieve this from a database)
            if (user.PassWord == loginDto.PassWord)
            {
                // Generate JWT token
                return GenerateJwtToken(user.UserCode);
            }
            throw new AuthenticationException(ErrorMessage.LoginError, (int)ErrorCode.LoginError);
        }

        // Method to register new user (you might want to save this info to a database)
        public async Task<string> RegisterUser(RegisterDto registerDto)
        {
            var user = _mapper.Map<UserDto>(registerDto);
            user.EditMode = EditMode.Create;
            var result = await _userService.SaveData([user]);
            if (result > 0)
            {
                return GenerateJwtToken(user.UserCode);
            }
            throw new DBException(ErrorMessage.RegisterError, (int)ErrorCode.RegisterError);
            // Save username and password to database
        }
    }
}
