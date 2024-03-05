namespace SchoolApplication.Models
{
    public class Student
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? MyProperty { get; set; }
        public Guid GenderId { get; set; }
        public Gender? gender { get; set; }
        public Address? address { get; set; }
        
    }
}
