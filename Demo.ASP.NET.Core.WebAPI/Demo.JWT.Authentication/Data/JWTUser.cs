using System.ComponentModel.DataAnnotations;

namespace Demo.JWT.Authentication.Data
{
    public class JWTUser
    {
        [Key]
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? UserPwd { get; set; }

        public string? Role { get; set; }
    }
}
