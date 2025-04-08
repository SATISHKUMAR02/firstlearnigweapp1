using System.Linq.Expressions;
using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Data.Repository;
using WebApplication1.Models;
using WebApplication1.Models.Students;

namespace WebApplication1.Controllers
{
  
        [Route("api/sample/[controller]")]
        [ApiController]v// do not remove , required for validations as well
        [Authorize(AuthenticationSchemes ="LoginForLocalUsers",Roles ="Superadmin,Admin,Student")] // this line denotes that all the endpoints are secured and to access them , authentication is necessary

        //[EnableCors(PolicyName="AllowOnlyGoogle")] => this applies for all the methods in the StudentController
        public class Studentcontrollers : ControllerBase
        {
        private readonly ILogger<Studentcontrollers> _logger;
        //private readonly CollegeDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IStudentRepository _studentRepository;
        private ApiResponse _apiresponse;
        public Studentcontrollers(ILogger<Studentcontrollers> logger, IStudentRepository studentRepository,IMapper mapper) // constructor of the Studentcontrollers
        {
            _logger = logger; 
            /* logger is a special tool to record application behaviour and events like errors warnings and messages
            LOG Levels in Logger => 1) Trace => detailed Logs
            2) Debug => Debugging information during devlopment
            3) Information => general Application flow info
            4) wanring something unexpected 
            5) Error failure in the application
            6) critical serious error
            
            
            
            */
            _studentRepository = studentRepository;
            _mapper = mapper;
            _apiresponse = new ApiResponse();
            // These are dependency injections and they are being copied in the local variable
        }
            [HttpGet]
            //[Authorize]// denotes only this endpoint is secure and to access this endpoint , we need to authenticate
            [Route("All")]
            [ProducesResponseType(200)]
            [ProducesResponseType(400)]
            [ProducesResponseType(401)]
            [ProducesResponseType(403)]
            //[ProducesResponseType(500)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            //[AllowAnonymous] this line denotes that anyone can access this endpoint
            //[EnableCors(PolicyName ="AllowOnlyGoogle")] => this policy applies only for this particular method
            //public IEnumerable<Student> GetStudents()
            //{
            //    return CollegeRepository.Students;
            //} without following the statdard

            public async Task<ActionResult<ApiResponse>> GetStudentAsync()
            {
            try
            {
                _logger.LogInformation("Get for all students started");
                // OK 200 status code
                //1)    var students = new List<Studentdto>();
                //    foreach (var item in CollegeRepository.Students) { 
                //    Studentdto obj = new Studentdto()
                //    {
                //        Id = item.Id,
                //        StudentName = item.Name,
                //        email = item.email

                //    };
                //    students.Add(obj); 
                //}

                // var students = _dbContext.Students.Select(s => new Student()
                //{
                //    Id = s.Id,
                //    StudentName = s.StudentName,
                //    Email = s.Email,
                //    Address=s.Address,
                //   DOB = s.DOB

                // }).ToList(); // this is using LINQ method
                //3)

                //if i want to perform validations after removing the apicontroller
                // I need to (ModelState.IsValid)
                // ModelState is an inbuilt object which will be check is the model from the from body is valid or not 
                // it will return message if any wrong data
                // the above method is not preferred due to more code, 
                // we can get the same repsonse if we add apicontroller and 
                var students = await _studentRepository.GetAllAsync();
                _apiresponse.data = _mapper.Map<List<Student>>(students);
                _apiresponse.Status = true;
                _apiresponse.StatusCode = HttpStatusCode.OK;
                return Ok(_apiresponse);
            }
            catch(Exception ex)
            {
                _apiresponse.Errors.Add(ex.Message);
                _apiresponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiresponse.Status = false;
                return _apiresponse;
            }
        }


        [HttpGet("{id:int}", Name = "GetStudentById")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        //            [DisableCors] methiod to disable CORS policy
        public async Task<ActionResult<ApiResponse>> GetStudentByIdAsync(int id)

        {
            try {
                _logger.LogInformation("Get for all students started");

                // status code 400
                if (id <= 0)
                {
                    _logger.LogWarning("Bad request");
                    return BadRequest();
                }
                // empty student -> status code 404
                var student = await _studentRepository.GetAsync(student => student.Id == id);
                if (student == null)
                {
                    _logger.LogError("not found");
                    return NotFound();
                }
                // var curstudent = _mapper.Map<Student>(student);
                // src    destination
                //var studentdto = new Student
                //{
                //    Id = student.Id,
                //    StudentName = student.StudentName,
                //    Email = student.Email,
                //    DOB= student.DOB,


                //}; // LINQ method 
                _apiresponse.data = _mapper.Map<Student>(student);
                _apiresponse.Status = true;
                _apiresponse.StatusCode = HttpStatusCode.OK;
                return Ok(_apiresponse);
            }
         catch (Exception ex)
        {
                _apiresponse.Errors.Add(ex.Message);
                _apiresponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiresponse.Status = false;
                return _apiresponse;
            }
        }

        [HttpGet("{name:alpha}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<ApiResponse>> GetStudentByNameAsync(string name)

        {
            try
            {
                // status code 400
                if (string.IsNullOrEmpty(name))
                {
                    _logger.LogWarning("Bad request");
                    return BadRequest();
                }
                // empty student -> status code 404
                var student = await _studentRepository.GetAsync(student => student.StudentName.ToLower().Contains(name.ToLower()));
                if (student == null)
                {
                    return NotFound($"the student with {name} not found");
                }
                //var curstudent = _mapper.Map<Student>(student);
                // src    destination
                _apiresponse.data = _mapper.Map<Student>(student);
                _apiresponse.Status = true;
                _apiresponse.StatusCode = HttpStatusCode.OK;
                return Ok(_apiresponse);
                //return Ok(CollegeRepository.Students.Where(student => student.Name == name).FirstOrDefault());
            }
            catch (Exception ex)
            {
                _apiresponse.Errors.Add(ex.Message);
                _apiresponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiresponse.Status = false;
                return _apiresponse;
            }
        }


        [HttpPost] // POST method
        [Route("Create")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<ActionResult<ApiResponse>> CreateStudentAsync([FromBody] Student model)

        {
            try
            {
                if (model == null)
                {
                    _logger.LogWarning("Bad request");
                    return BadRequest();
                }

                //int newId = _dbContext.Students.LastOrDefault().Id + 1;
                //Student student = new Student
                //{
                //    //Id = newId,
                //    StudentName = model.StudentName,
                //    Address = model.Address,
                //    Email = model.Email,
                //    DOB = model.DOB 

                //}; // without auto mapper  so better to use an auto mapper
                //always use automapper instead of manual copying
                Student student = _mapper.Map<Student>(model);
                // src    destination

                var studentcreated = await _studentRepository.CreateAsync(student);
                model.Id = studentcreated.Id;
                _apiresponse.data = model;
                _apiresponse.StatusCode = HttpStatusCode.OK;
                _apiresponse.Status = true;

                return CreatedAtRoute("GetStudentById", new { id = model.Id }, _apiresponse); // status code 201
                                                                                              //return Ok(model);
            }
            catch (Exception ex)
            {
                _apiresponse.Errors.Add(ex.Message);
                _apiresponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiresponse.Status = false;
                return _apiresponse;
            }
        }
        //PUT
        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<ActionResult<ApiResponse>> UpdateStudentAsync([FromBody] Student model)
        {
            try
            {
                if (model == null || model.Id <= 0)
                {
                    _logger.LogWarning("Bad request");
                    BadRequest();
                }
                var existingStudent = await _studentRepository.GetAsync(student => student.Id == model.Id, true);
                if (existingStudent == null)
                    return NotFound();
                //var nreRecord = new Student()
                //{
                //    Id=existingStudent.Id,
                //    StudentName=model.StudentName,
                //    Email=model.Email,
                //    Address=model.Address,
                //    DOB=model.DOB
                //};
                var nreRecord = _mapper.Map<Student>(model);
                //_dbContext.Students.Update(nreRecord); //no async for update
                //existingStudent.StudentName = model.StudentName;
                //existingStudent.Email = model.Email;
                //existingStudent.Address = model.Address;
                //existingStudent.DOB=model.DOB;

                await _studentRepository.UpdateAsync(nreRecord);
                return NoContent();
            }
            catch (Exception ex)
            {
                _apiresponse.Errors.Add(ex.Message);
                _apiresponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiresponse.Status = false;
                return _apiresponse;
            }
        }

        //PATCH
        [HttpPatch] // for performing patch operation , we need to install two packages
        // Microsoft.AspNetCore.JsonPatch and  Microsoft.AspNetCore.Mvc.NewtonsoftJson //
        // =====================================================================================================================very important
        [Route("{id:int}/UpdatePartially")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]                                  // when performing patch we need to mentino JsonPatchDocument here
        public async Task<ActionResult<ApiResponse>> UpdateStudentPartial(int id, [FromBody] JsonPatchDocument<Student> patchDocument)
        {
            try
            {


                if (patchDocument == null || id <= 0)
                {
                    BadRequest();
                }

                var existingStudent = await _studentRepository.GetAsync(student => student.Id == id, true);


                if (existingStudent == null)
                    return NotFound();

                var curstudent = _mapper.Map<Student>(existingStudent);
                patchDocument.ApplyTo(curstudent, ModelState); // ModelState object is passed so that , if any error occurs any occurs when applying/ updating 
                // we ask modelstate to store the error and give it as a response to us
//=====================================================================================================================================================very important
                /*PATCH STRUCTURE
                [
                  { "operationType":,
                    "path":"/studentName",
                    "op":"replace",
                    "from":,
                    "value":"satish", will update satish kumar to satish 204 means success for patch operation
                    // not 200
                
                  }
                ]
                
                
                */
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);

                }
                existingStudent = _mapper.Map<Student>(curstudent);


                //existingStudent.StudentName = curstudent.StudentName;
                //existingStudent.Email = curstudent.Email;
                //existingStudent.Address = curstudent.Address;
                //existingStudent.DOB =curstudent.DOB;

                                
                // below two lines are Db lines that are included in the repository
                //_dbContext.Students.Update(existingStudent);
                //await _dbContext.SaveChangesAsync();
                await _studentRepository.UpdateAsync(existingStudent);
                return NoContent();
            }
            catch (Exception ex)
            {
                _apiresponse.Errors.Add(ex.Message);
                _apiresponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiresponse.Status = false;
                return _apiresponse;
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> DeleteStudentById(int id)

        {
            try
            {
            if (id <= 0)
                {
                    return BadRequest();
                }
                var stu = await _studentRepository.GetAsync(student => student.Id == id);
                if (stu == null)
                    return NotFound();
                // _dbContext.Students.Remove(stu); // no async for delete and update
                //await _dbContext.SaveChangesAsync();
                await _studentRepository.DeleteAsync(stu);
                _apiresponse.data = true;
                _apiresponse.Status = true;
                _apiresponse.StatusCode = HttpStatusCode.OK;
                return Ok(_apiresponse);
            }
            catch (Exception ex)
            {
                _apiresponse.Errors.Add(ex.Message);
                _apiresponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiresponse.Status = false;
                return _apiresponse;
            }
        }
        }
    }
/* Content Negotiation is a feture which returns different return types , that is if the reponse needs to be in JSON -> or if the respnse needs to be in XML*/
