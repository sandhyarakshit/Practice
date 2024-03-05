using Microsoft.EntityFrameworkCore;
using SchoolApplication.Models;
using SchoolApplication.Repositories;


namespace SchoolApplication.Repositories
{
    public class UserRepository : IUserRepository
    {
          private readonly StudentDbContext context;
        public SqlStudentRepository(StudentDbContext context)
        {
            this.context = context;
        }

          public async Task<List<User>> GetUsersAsync()
        {
            return await context.Users.ToListAsync();
        }
    }
}