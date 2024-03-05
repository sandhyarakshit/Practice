using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolApplication.Models;

namespace SchoolApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IConfiguration _config;
        private readonly StudentDbContext _context;
        private readonly IUserRepository _userRepository;

        public UserController(IConfiguration config, StudentDbContext context, IUserRepository userRepository)
        {
            _config = config;
            _context = context;
            _userRepository = userRepository;
        }

        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetAllUsers()
        {
             var users= await _userRepository.GetUsersAsync();
            return Ok(users);
          
        }

        [AllowAnonymous]
        [HttpPost("CreateUser")]
        public IActionResult Create(User user)
        {
            if (_context.Users.Where(u => u.Email == user.Email).FirstOrDefault() != null)
            {
                return Ok("Already Exists");
            }
            user.MemberSince = DateTime.Now;
            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok("Success");
        }
        
/*
        [HttpPost]
        public async IActionResult UpdateUser(User user)
        {
          var existingUser = _context.Users.Where(u =>u.Email == user.Email).FirstOrDefault() ;
            if(existingUser != null)
            {
                existingUser.FirstName = user.FirstName;
                 existingUser.LastName =user.LastName;
                 existingUser.Email = user.Email ;
                 existingUser.Mobile = user.Mobile ;
                 existingUser.Gender = user.Gender;
                 existingUser.Pwd = user.Pwd;
                 existingUser.MemberSince = user.MemberSince;
                 _context.SaveChangesAsync();
                  return Ok("Success");
            }
        }

        

        public async IActionResult GetUserByEmail (string email)
        {
            var foundUser = await _context.Users.FirstOrDefault( z => z.Email == email );
            return foundUser;
        }
*/
        [AllowAnonymous]
        [HttpPost("LoginUser")]
        public IActionResult Login(Login user)
        {
            var userAvailable = _context.Users.Where(u => u.Email == user.Email && u.Pwd == user.Password).FirstOrDefault();
           if( userAvailable!=null ) {

                return Ok(new JwtService(_config).GenerateToken(
                    userAvailable.UserId.ToString(),
                    userAvailable.FirstName,
                    userAvailable.LastName,
                    userAvailable.Email,
                    userAvailable.Mobile,
                    userAvailable.Gender
                    ));
            }
            return Ok("Failure");
        }
       
    }
}
