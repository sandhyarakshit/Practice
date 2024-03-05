using Microsoft.EntityFrameworkCore;

namespace SchoolApplication.Models
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options) {
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Gender> Gender { get; set; }
        public DbSet<Address> Address { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
