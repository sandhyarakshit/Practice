using Microsoft.EntityFrameworkCore;
using SchoolApplication.Models;

namespace SchoolApplication.Repositories
{
    public class SqlStudentRepository : IStudentRepository
    {
        private readonly StudentDbContext context;
        public SqlStudentRepository(StudentDbContext context)
        {
            this.context = context;
        }
        public async Task<List<Student>> GetStudentsAsync()
        {
            return await context.Students.Include(d => d.gender).Include(v => v.address).ToListAsync();
        }

        public async Task<Student> GetStudentAsync(Guid studentId)
        {
            return await context.Students.Include(d => d.gender).Include(v => v.address).FirstOrDefaultAsync(x => x.Id == studentId);
        }

        public async Task<List<Gender>> GetGendersAsync()
        {
            return await context.Gender.ToListAsync();
        }

        public async Task<bool> Exists(Guid studentsId)
        {
            return await context.Students.AnyAsync(x => x.Id == studentsId);
        }
        public async Task<Student> UpdateStudent(Guid studentId, Student request)
        {
            var existingStudent = await GetStudentAsync(studentId);
            if(existingStudent != null)
            {
                existingStudent.FirstName = request.FirstName;
                existingStudent .LastName = request.LastName;
                existingStudent.DateOfBirth = request.DateOfBirth;
                existingStudent.Email = request.Email;
                existingStudent.Mobile = request.Mobile;
                existingStudent.GenderId = request.GenderId;
                existingStudent.address.PhysicalAddress = request.address.PhysicalAddress;
               existingStudent.address.PostalAddress = request.address.PostalAddress;
                await context.SaveChangesAsync();
                return existingStudent;
            }
            return null;
         }

        public async Task<Student> DeleteStudent(Guid studentId)
        {
            var student = await GetStudentAsync(studentId);
            if(student!= null)
            {
                context.Students.Remove(student);
                await context.SaveChangesAsync();
                return student;
            }
            return null;

        }

        public async Task<Student> AddStudent(Student request)
        {
            var student = await context.Students.AddAsync(request);
            await context.SaveChangesAsync();
            return student.Entity;
        }

        public async Task<bool> UpdateProfileImage(Guid studentId, string profileImageUrl)
        {
            var student = await GetStudentAsync(studentId);
            if (student != null)
            {
                student.ProfileImageUrl = profileImageUrl;
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
