using Microsoft.IdentityModel.Tokens;
using NTTRUNG_Lazy_Languages_Domain.Enum;
using NTTRUNG_Lazy_Languages_Domain.Resources.ErrorMessage;
using NTTRUNG_Lazy_Languages_Domain;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NTTRUNG_Lazy_Languages.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _config;
        public JwtMiddleware(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            _config = config;
        }

        public async Task Invoke(HttpContext context)
        {
            // Lấy token từ tiêu đề "Authorization" của yêu cầu HTTP
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                // Xác thực token và trích xuất thông tin
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_config["JWT:SecretKey"]);
                try
                {
                    tokenHandler.ValidateToken(token, new TokenValidationParameters
                                    {
                                        ValidateIssuerSigningKey = true,
                                        IssuerSigningKey = new SymmetricSecurityKey(key),
                                        ValidateIssuer = false,
                                        ValidateAudience = false
                                    }, out SecurityToken validatedToken);
                    // Nếu token hợp lệ, trích xuất thông tin và lưu trữ nó trong context
                    var jwtToken = (JwtSecurityToken)validatedToken;
                    var uniqueNameClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.UniqueName);
                    if (uniqueNameClaim != null)
                    {
                        var userCode = uniqueNameClaim.Value;
                        // Sử dụng giá trị uniqueNameValue ở đây
                        context.Items["UserCode"] = userCode;
                    }
                }
                catch
                {
                    throw new Unauthorized(ErrorMessage.Unauthorized, (int)ErrorCode.Unauthorized);
                }
                
            }

            // Chuyển yêu cầu tiếp theo trong pipeline
            await _next(context);
        }
    }
}
