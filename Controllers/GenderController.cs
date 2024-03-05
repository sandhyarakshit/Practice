using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SchoolApplication.DomainModels;
using SchoolApplication.Repositories;

namespace SchoolApplication.Controllers
{
    [ApiController]
    public class GenderController : Controller
    {
        private readonly IStudentRepository studentRepository;
        private readonly IMapper mapper;

        public GenderController(IStudentRepository studentRepository, IMapper mapper)
        {
            this.studentRepository = studentRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetAllGenders()
        {
            var genderList = await studentRepository.GetGendersAsync();
            if(genderList == null || genderList.IsNullOrEmpty()) { 
            return NotFound();
            }

            return Ok(mapper.Map<List<Gender>>(genderList));
           
        }
       
    }
}
