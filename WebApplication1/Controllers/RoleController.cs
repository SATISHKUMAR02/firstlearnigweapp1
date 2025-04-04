using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Data.Repository;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")] // attribute based routing
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICollegeRepository<Role> _roleRepository;
        private ApiResponse _apiResponse;
        public RoleController(IMapper mapper, ICollegeRepository<Role> roleRepository)
        {
            _mapper = mapper;
            _roleRepository = roleRepository;
            _apiResponse = new(); // creates a ApiResponse object

        }
        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse>> CreateRoleAsync(RoleDto model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                Role role = _mapper.Map<Role>(model);
                role.isDeleted = false;
                role.CreatedDate = DateTime.Now;
                role.ModifiedDate = DateTime.Now;
                var result = await _roleRepository.CreateAsync(role);
                model.Id = result.Id;
                _apiResponse.data = model;
                _apiResponse.Status = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;
                return Ok(_apiResponse);
                //return CreatedAtRoute("GetRoleById", new { id = model.Id }, _apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.Status = false;
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiResponse.Errors.Add(ex.Message);
                return _apiResponse;
            }
        }

        [HttpGet]
        [Route("All", Name = "GetAllRoles")]
        public async Task<ActionResult<ApiResponse>> GetRoleAsync()
        {
            try
            {

                var roles = await _roleRepository.GetAllAsync();
                _apiResponse.data = _mapper.Map<List<RoleDto>>(roles);
                _apiResponse.Status = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;
                return Ok(_apiResponse);
                //return CreatedAtRoute("GetRoleById", new { id = model.Id }, _apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.Status = false;
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiResponse.Errors.Add(ex.Message);
                return _apiResponse;
            }



        }

        [HttpGet]
        [Route("{id:int}", Name = "GetAllRolesById")]
        public async Task<ActionResult<ApiResponse>> GetRoleAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest();
                }

                var role = await _roleRepository.GetAsync(role=>role.Id==id);

                if (role == null)
                {
                    return NotFound("specified Id is not present");
                }
                _apiResponse.data = _mapper.Map<RoleDto>(role);
                _apiResponse.Status = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;
                return Ok(_apiResponse);
                //return CreatedAtRoute("GetRoleById", new { id = model.Id }, _apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.Status = false;
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiResponse.Errors.Add(ex.Message);
                return _apiResponse;
            }



        }

        [HttpGet]
        [Route("{name:alpha}", Name = "GetAllRolesByName")]
        public async Task<ActionResult<ApiResponse>> GetRoleAsync(string name)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                {
                    return BadRequest();
                }

                var role = await _roleRepository.GetAsync(role => role.RoleName == name);

                if (role == null)
                {
                    return NotFound("specified Id is not present");
                }
                _apiResponse.data = _mapper.Map<RoleDto>(role);
                _apiResponse.Status = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;
                return Ok(_apiResponse);
                //return CreatedAtRoute("GetRoleById", new { id = model.Id }, _apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.Status = false;
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiResponse.Errors.Add(ex.Message);
                return _apiResponse;
            }

        }
        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse>> UpdateRoleAsync(RoleDto model)
        {
            try
            {
                if (model == null || model.Id<=0)
                {
                    return BadRequest();
                }                                                                          // using no tracking      
                var existingRole = await _roleRepository.GetAsync(role => role.Id == model.Id,true);
                if (existingRole == null)
                {
                    return BadRequest("not found with given specification");
                }
                var newRole = _mapper.Map<Role>(model);
                await _roleRepository.UpdateAsync(newRole);
                _apiResponse.Status = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;
                _apiResponse.data = newRole;
                return Ok(_apiResponse);

            }
            catch (Exception ex)
            {
                _apiResponse.Status = false;
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiResponse.Errors.Add(ex.Message);
                return _apiResponse;
            }
        }

        [HttpDelete]
        [Route("Delete/{id}",Name ="DeleteRoleById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse>> DeleteRoleAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest();
                }                                                                          // using no tracking      
                var role = await _roleRepository.GetAsync(role => role.Id == id);
                if (role == null)
                {
                    return BadRequest("not found with given specification");
                }
                await _roleRepository.DeleteAsync(role);
                
                _apiResponse.Status = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;
                _apiResponse.data = true;
                return Ok(_apiResponse);

            }
            catch (Exception ex)
            {
                _apiResponse.Status = false;
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiResponse.Errors.Add(ex.Message);
                return _apiResponse;
            }
        }

    }
}