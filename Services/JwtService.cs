using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Facebook_be.Services
{
    public class JwtService
    {
        private string secretKey = "this is my custom Secret key for authentication"; // một chuỗi giá trị bất kỳ, biến này chứa một khóa bí mật sẽ được sử dụng để mã hóa và ghép chuỗi token.
        public string Generate(int id)
        {
            var secretKeyBytes = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)); // đại diện cho khóa đối xứng được sử dụng để ký và xác minh token
                                                                                              // Khi mã hóa dữ liệu, cần sử dụng khóa đối xứng để biến dữ liệu gốc thành dạng mã hóa.
                                                                                              // Khóa này sẽ được sử dụng để ký token JWT.

            var signInCredentials = new SigningCredentials(secretKeyBytes, SecurityAlgorithms.HmacSha256Signature); // tạo thông tin chứng thực, Đây là cách token sẽ được ký để đảm bảo tính toàn vẹn.


            var header = new JwtHeader(signInCredentials); // chứa thuật toán dùng để mã hóa
            var payload = new JwtPayload(id.ToString(), null, null, null, DateTime.UtcNow.AddMinutes(15)); // hạn 1 ngày

            var securityToken = new JwtSecurityToken(header, payload); // kết hợp header, payload

            return new JwtSecurityTokenHandler().WriteToken(securityToken); // tạo ra signature sau đó tạo ra chuỗi token dưới dạng văn bản 
        }

        public JwtSecurityToken Verify(string jwt)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            tokenHandler.ValidateToken(jwt, new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
            }, out SecurityToken validatedToken);

            return (JwtSecurityToken)validatedToken;
        }
    }
}
