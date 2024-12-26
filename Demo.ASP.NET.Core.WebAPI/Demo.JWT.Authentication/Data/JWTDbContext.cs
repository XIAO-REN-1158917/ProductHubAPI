using Microsoft.EntityFrameworkCore;

namespace Demo.JWT.Authentication.Data
{
    public class JWTDbContext : DbContext
    {
        public JWTDbContext(DbContextOptions<JWTDbContext> options) : base(options)
        {
        }
        public DbSet<JWTUser>? JWTUsers { get; set; }
    }
}