using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchoolApplication.DomainModels;
using SchoolApplication.Models;
using SchoolApplication.Repositories;
using System.Net.NetworkInformation;
using Address = SchoolApplication.DomainModels.Address;
using Gender = SchoolApplication.Models.Gender;
using Student = SchoolApplication.DomainModels.Student;

namespace SchoolApplication.Controllers
{
    [ApiController]
    public class StudentsController : Controller
    {
        private readonly IStudentRepository studentRepository;

        private readonly IMapper mapper;
        private readonly IImageRepository imageRepository;

        public StudentsController(IStudentRepository studentRepository, IMapper mapper ,
            IImageRepository imageRepository)
        {
            this.studentRepository = studentRepository;
            this.mapper = mapper;
            this.imageRepository = imageRepository;
        }
        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetAllStudents()
        {
             var students = await studentRepository.GetStudentsAsync();
            return Ok(mapper.Map<List<Student>>(students));
          
        }

        [HttpGet]
        [Route("[controller]/{studentId:guid}"), ActionName("GetStudentAsync")]
        public async Task<IActionResult> GetStudentAsync([FromRoute] Guid studentId)
        {
            var student = await studentRepository.GetStudentAsync(studentId);
            //Fetch Student Details
            
            //Return Student
            if (student == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(mapper.Map<Student>(student));
            }
        }
        [HttpPut]
        [Route("[controller]/{studentId:guid}")]
        public async Task<IActionResult> UpdateStudentsAsync([FromRoute] Guid studentId ,[FromBody] UpdateStudentRequest request)
        {
           if( await studentRepository.Exists (studentId))
            {
                var updateStudent = await studentRepository.UpdateStudent(studentId , mapper.Map<Models.Student>(request));
               if(updateStudent != null)
                {
                    return Ok(mapper.Map<Student>(updateStudent));
                }
                //Update Details      
            }
                return NotFound();
            
        }

        [HttpDelete]
        [Route ("[controller]/{studentId:guid}")]
        public async Task<IActionResult>DeleteStudentAsync([FromRoute] Guid studentId)
        {
            if(await studentRepository.Exists (studentId))
            {
                var student = await studentRepository.DeleteStudent(studentId);
                return Ok(mapper.Map<Student>(student));
            }
            return NotFound();

        }
        [HttpPost]
        [Route("[controller]/Add")]
        public async Task<IActionResult> AddStudentAsync([FromBody] AddStudentRequest request)
        {
           var student =await studentRepository.AddStudent(mapper.Map<Models.Student>(request));
            return CreatedAtAction(nameof(GetStudentAsync), new {studentId = student.Id},
                mapper.Map<Student>(student));
        }

        /*
        [HttpPost]
        [Route("[controller]/{studentId:guid}/upload-image")]
        public async Task<IActionResult> UploadImage([FromRoute] Guid studentId , IFormFile profileImage)
        {
            if(await studentRepository.Exists(studentId))
            {
                //Upload the Image to local storage
                var fileName = Guid.NewGuid() + Path.GetExtension(profileImage.FileName);
               var fileImagePath = await imageRepository.Upload(profileImage, fileName);
                if (await studentRepository.UpdateProfileImage(studentId, fileImagePath))
                {
                    return Ok(fileImagePath);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, "Error uploading Image");
            }
            return NotFound();
        } */
    }
}
