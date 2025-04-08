using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Data.Repository;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolePrivilegeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICollegeRepository<RolePrivilege> _rolePrivilegeRepository;
        private ApiResponse _apiResponse;
        public RolePrivilegeController(IMapper mapper, ICollegeRepository<RolePrivilege> rolePrivilegeRepository) 
        // the above code is inherting ICollegeRepository and giving to all the RolePrivileges Repository 
        // so we create a common repository for all the functions and then inherit the same and use it differently
        
        
        
        {
            _mapper = mapper;
            _rolePrivilegeRepository = rolePrivilegeRepository;
            _apiResponse = new();
        }
        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse>> CreateRolePrivilegeAsync(RolePrivilegeDto model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                RolePrivilege rolePrivilege = _mapper.Map<RolePrivilege>(model);
                rolePrivilege.isDeleted = false;
                rolePrivilege.CreatedDate = DateTime.Now;
                rolePrivilege.DeletedDate = DateTime.Now;
                var result = await _rolePrivilegeRepository.CreateAsync(rolePrivilege);
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
        [Route("All", Name = "GetAllRolePrivileges")]
        public async Task<ActionResult<ApiResponse>> GetRolePrivilegeAsync()
        {
            try
            {

                var roles = await _rolePrivilegeRepository.GetAllAsync();
                _apiResponse.data = _mapper.Map<List<RolePrivilegeDto>>(roles);
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
        [Route("{id:int}", Name = "GetAllRolePrivilegesById")]
        public async Task<ActionResult<ApiResponse>> GetRolePrivilegeIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest();
                }

                var role = await _rolePrivilegeRepository.GetAsync(role => role.Id == id);

                if (role == null)
                {
                    return NotFound("specified Id is not present");
                }
                _apiResponse.data = _mapper.Map<RolePrivilegeDto>(role);
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
        [Route("RoleId/{id:int}", Name = "GetAllRolePrivilegesByRoleId")]
        public async Task<ActionResult<ApiResponse>> GetRolePrivilegeRoleIdAsync(int roleid)
        {
            try
            {
                if (roleid <= 0)
                {
                    return BadRequest();
                }

                var role = await _rolePrivilegeRepository.GetAllByAnyAsync(role => role.RoleId == roleid);

                if (role == null)
                {
                    return NotFound("specified Id is not present");
                }
                _apiResponse.data = _mapper.Map<RolePrivilegeDto>(role);
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
        [Route("{name:alpha}", Name = "GetAllRolePrivilegesByName")]
        public async Task<ActionResult<ApiResponse>> GetRolePrivilegeNameAsync(string name)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                {
                    return BadRequest();
                }

                var role = await _rolePrivilegeRepository.GetAsync(role => role.RoleprivilegeName.ToLower().Contains(name.ToLower()));

                if (role == null)
                {
                    return NotFound("specified Id is not present");
                }
                _apiResponse.data = _mapper.Map<RolePrivilegeDto>(role);
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
        [Route("UpdatePrivilege")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse>> UpdateRolePrivilegeAsync(RolePrivilegeDto model)
        {
            try
            {
                if (model == null || model.Id <= 0)
                {
                    return BadRequest();
                }                                                                          // using no tracking      
                var existingRole = await _rolePrivilegeRepository.GetAsync(role => role.Id == model.Id, true);
                if (existingRole == null)
                {
                    return BadRequest("not found with given specification");
                }
                var newRole = _mapper.Map<RolePrivilege>(model);
                await _rolePrivilegeRepository.UpdateAsync(newRole);
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
        [Route("Delete/{id}", Name = "DeleteRolePrivilegeById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse>> DeleteRolePrivilegeAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest();
                }                                                                          // using no tracking      
                var role = await _rolePrivilegeRepository.GetAsync(role => role.Id == id);
                if (role == null)
                {
                    return BadRequest("not found with given specification");
                }
                await _rolePrivilegeRepository.DeleteAsync(role);

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

