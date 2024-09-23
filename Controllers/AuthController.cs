using Facebook_be.Dto;
using Facebook_be.Models;
using Facebook_be.Repositories;
using Facebook_be.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Facebook_be.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly FacebookDbContext _context;
        private readonly IUserRepository _repository;
        private readonly JwtService _jwtService;
        public AuthController(IUserRepository repository, JwtService jwtService, FacebookDbContext context)
        {
            _context = context;
            _repository = repository;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDto dto)
        {
            var user = new User
            {
                Username = dto.UserName,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                FullName = dto.FullName
            };
            var existingEmail = _context.Users.FirstOrDefault(u => u.Email == dto.Email);
            if (existingEmail != null)
            {
                return BadRequest(new { message = "Email alreay register" });
            }
            var existingUserName = _context.Users.FirstOrDefault(u => u.Username == dto.UserName);
            if (existingUserName != null)
            {
                return BadRequest(new { message = "User name alreay register" });
            }

            var createdUser = _repository.Create(user); // Chỉ gọi Create một lần , xác định phương thức Create nào sẽ được gọi dựa trên instance của lớp đang làm việc. (đã đăng ký ở program)

            return Created("success", createdUser); // Trả về đối tượng đã được tạo
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            var user = _repository.GetByUsername(dto.Username);
            if (user == null) // case 1: không tìm thấy tài khoản trong dtb
            {
                return BadRequest(new { message = "Can't find user name" });
            }

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash)) // case 2: tìm thấy tài khoản nhưng không đúng mật khẩu
            {
                return BadRequest(new { message = "Your password is not correct" });
            }

            var jwt = _jwtService.Generate(user.Id);

            //Response.Cookies.Append("jwt", jwt, new CookieOptions
            //{
            //    HttpOnly = true
            //});

            return Ok(new
            {
                message = "success",
                accessToken = jwt,
            });
        }

        [HttpGet("user")]
        public IActionResult User()
        {
            // Lấy JWT từ header
            var authorizationHeader = Request.Headers["Authorization"].FirstOrDefault();

            if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
            {
                return Unauthorized(new { message = "JWT token is missing or improperly formatted." });
            }

            // Lấy token từ chuỗi 'Bearer token_value'
            var jwt = authorizationHeader.Substring("Bearer ".Length).Trim();

            try
            {
                var token = _jwtService.Verify(jwt);

                if (token == null)
                {
                    return Unauthorized(new { message = "Invalid or expired token. Please log in again." });
                }

                int userId = int.Parse(token.Issuer); // Nên kiểm tra kỹ giá trị token.Issuer
                var user = _repository.GetById(userId);

                if (user == null)
                {
                    return NotFound(new { message = "User not found." });
                }

                var userDto = new UserDTO
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    FullName = user.FullName,
                    ProfileImg = user.ProfileImg,
                    CoverImg = user.CoverImg,
                    Bio = user.Bio,
                    DateOfBirth = user.DateOfBirth,
                    Gender = user.Gender,
                    CreatedAt = user.CreatedAt,
                    UpdatedAt = user.UpdatedAt
                };

                return Ok(userDto);
            }
            catch (SecurityTokenException ex)
            {
                return Unauthorized(new { message = "Token verification failed. " + ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred.", details = ex.Message });
            }
        }
    }
}
