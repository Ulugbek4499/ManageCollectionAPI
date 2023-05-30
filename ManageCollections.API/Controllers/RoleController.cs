using ManageCollections.Application.DTOs.Roles;
using ManageCollections.Application.Interfaces;
using ManageCollections.Application.Models.ResponseModels;
using ManageCollections.Domain.Entities.IdentityEntities;
using Microsoft.AspNetCore.Mvc;

namespace ManageCollections.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ApiController<Role>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IPermissionRepository _permissionRepository;

        public RoleController(IRoleRepository roleRepository, IPermissionRepository permissionRepository)
        {
            _roleRepository = roleRepository;
            _permissionRepository = permissionRepository;
        }

        [HttpPost("[action]")]
        //[Authorize(Roles = "CreateRole")]
        public async Task<ActionResult<ResponseCore<RoleGetDTO>>> Create(RoleCreateDTO roleCreateDTO)
        {
            Role role = _mapper.Map<Role>(roleCreateDTO);
            var validationResult = _validator.Validate(role);

            if (!validationResult.IsValid)
            {
                return BadRequest(new ResponseCore<object>(false, validationResult.Errors));
            }

            role.Permissions = new List<Permission>();

            foreach (Guid item in roleCreateDTO.Permissions)
            {
                Permission? permission = await _permissionRepository.GetByIdAsync(item);
                if (permission != null)
                    role.Permissions.Add(permission);
                else return BadRequest(new ResponseCore<string>(false, item + " Id not found"));
            }

            role = await _roleRepository.CreateAsync(role);
            RoleGetDTO res = _mapper.Map<RoleGetDTO>(role);

            return Ok(new ResponseCore<object>(res));
        }

        [HttpGet("[action]")]
        //[Authorize(Roles = "GetAllRole")]
        public async Task<ActionResult<ResponseCore<IEnumerable<RoleGetDTO>>>> GetAll()
        {
            IEnumerable<Role> roles = await _roleRepository.GetAsync(x => true, nameof(Role.Permissions));

            IEnumerable<RoleGetDTO> roleGetDTOs = _mapper.Map<IEnumerable<RoleGetDTO>>(roles);

            return Ok(new ResponseCore<IEnumerable<RoleGetDTO>>(roleGetDTOs));
        }

        [HttpGet("[action]")]
        //[Authorize(Roles = "GetByIdRole")]
        public async Task<ActionResult<ResponseCore<RoleGetDTO>>> GetById(Guid id)
        {
            IEnumerable<Role> roles = await _roleRepository.GetAsync(x => true, nameof(Role.Permissions));
            Role? role = roles.FirstOrDefault(x => x.Id == id);
            if (role == null)
            {
                return NotFound(new ResponseCore<Role?>(false, id + " not found!"));
            }
            RoleGetDTO mappedRole = _mapper.Map<RoleGetDTO>(role);
            return Ok(new ResponseCore<RoleGetDTO?>(mappedRole));
        }

        [HttpPut("[action]")]
        //[Authorize(Roles = "UpdateRole")]
        public async Task<ActionResult<ResponseCore<RoleGetDTO>>> Update(RoleUpdateDTO roleUpdateDTO)
        {
            Role? role = _mapper.Map<Role>(roleUpdateDTO);
            var validationResult = _validator.Validate(role);

            if (!validationResult.IsValid)
            {
                return BadRequest(new ResponseCore<Role>(false, validationResult.Errors));
            }

            role.Permissions = new List<Permission>();

            foreach (var item in roleUpdateDTO.PermissionIds)
            {
                Permission? permission = await _permissionRepository.GetByIdAsync(item);
                if (permission != null)
                    role.Permissions.Add(permission);

                else return BadRequest(new ResponseCore<Role>(false, item + " Id not found"));
            }

            role = await _roleRepository.UpdateAsync(role);

            if (role != null)
                return Ok(new ResponseCore<RoleGetDTO>(_mapper.Map<RoleGetDTO>(role)));

            return BadRequest(new ResponseCore<Role>(false, roleUpdateDTO + " not found"));
        }


        [HttpDelete("[action]")]
        //[Authorize(Roles = "DeleteRole")]
        public async Task<ActionResult<ResponseCore<bool>>> Delete(Guid id)
        {
            return await _roleRepository.DeleteAsync(id) ?
                   Ok(new ResponseCore<bool>(true))
                   : BadRequest(new ResponseCore<bool>(false, "Delete failed!"));
        }

    }
}
