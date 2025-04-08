using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Data.Repository;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // private readonly IMapper _mapper;
        // private readonly IStudentRepository _studentRepository;
        private ApiResponse _apiresponse;
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {

            //_studentRepository = studentRepository;
            //_mapper = mapper;
            _apiresponse = new();
            _userService = userService;
        }
        [HttpPost] // POST method
        [Route("CreateUser")] 
        // these are responsible for preventing undocumented printing in the Swagger Ui ,
        // also for showing the different responses 
        [ProducesResponseType(201)] 
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<ActionResult<ApiResponse>> CreateUserAsync(UserDto dto)
        {
            try
            {
                var usercreated = await _userService.CreateUserAsync(dto);
                _apiresponse.data = usercreated;
                _apiresponse.Status = true;
                _apiresponse.StatusCode = HttpStatusCode.OK;
                return Ok(_apiresponse);
            } catch (Exception ex)
            {
                _apiresponse.Errors.Add(ex.Message);

                _apiresponse.Status = false;
                _apiresponse.StatusCode = HttpStatusCode.InternalServerError;
                return _apiresponse;

            }
        }
        [HttpGet] // Get all users
        [Route("GetAllUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<ActionResult<ApiResponse>> GetUserAsync()
        {
            try
            {
                var users = await _userService.GetUsersAsync();
                //_apiresponse.data = _mapper.Map<List<Users>>(users);
                _apiresponse.data = users;
                _apiresponse.Status = true;
                _apiresponse.StatusCode = HttpStatusCode.OK;
                return Ok(_apiresponse);
            }
            catch (Exception ex)
            {
                _apiresponse.Errors.Add(ex.Message);

                _apiresponse.Status = false;
                _apiresponse.StatusCode = HttpStatusCode.InternalServerError;
                return _apiresponse;

            }
        }

        // Get users by ID
        // POST method
        [HttpGet("GetUserById/{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<ActionResult<ApiResponse>> GetUserByIdAsync(int id)
        {
            try 
            {
                var user = await _userService.GetUserByIdAsync(id);
                //_apiresponse.data = _mapper.Map<List<Users>>(users);
                _apiresponse.data = user;
                _apiresponse.Status = true;
                _apiresponse.StatusCode = HttpStatusCode.OK;
                return Ok(_apiresponse);
            }
            catch (Exception ex)
            {
                _apiresponse.Errors.Add(ex.Message);

                _apiresponse.Status = false;
                _apiresponse.StatusCode = HttpStatusCode.InternalServerError;
                return _apiresponse;

            }
        }

        // GET user by name

        // GET method
        [HttpGet("GetUserByName/{username:alpha}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<ActionResult<ApiResponse>> GetUserByNameAsync(string username)
        {
            try
            {
                var user = await _userService.GetUserByNameAsync(username);
                //_apiresponse.data = _mapper.Map<List<Users>>(users);
                _apiresponse.data = user;
                _apiresponse.Status = true;
                _apiresponse.StatusCode = HttpStatusCode.OK;
                return Ok(_apiresponse);
            }
            catch (Exception ex)
            {
                _apiresponse.Errors.Add(ex.Message);

                _apiresponse.Status = false;
                _apiresponse.StatusCode = HttpStatusCode.InternalServerError;
                return _apiresponse;

            }
        }

        //update 
        [HttpPut]
        [Route("UpdateUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse>> UpdateUserAsync(UserDto model)
        {
            try
            {
                if (model == null || model.Id <= 0)
                {
                    return BadRequest();
                }                                                                          // using no tracking      

                var result = await _userService.UpdateUserAsync(model);
                _apiresponse.Status = true;
                _apiresponse.StatusCode = HttpStatusCode.OK;
                _apiresponse.data = result;
                return Ok(_apiresponse);

            }
            catch (Exception ex)
            { 
                _apiresponse.Status = false;
                _apiresponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiresponse.Errors.Add(ex.Message);
                return _apiresponse;
            }
        }
        [HttpDelete]
        [Route("Delete/{id}", Name = "DeleteUserById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse>> DeleteUserAsync(int id)
        {
            try
            {
                                                                                      // using no tracking      
                var role = await _userService.DeleteUserAsync(id);

                _apiresponse.Status = true;
                _apiresponse.StatusCode = HttpStatusCode.OK;
                _apiresponse.data = role;
                return Ok(_apiresponse);

            }
            catch (Exception ex)
            {
                _apiresponse.Status = false;
                _apiresponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiresponse.Errors.Add(ex.Message);
                return _apiresponse;
            }
        }
    }
}
