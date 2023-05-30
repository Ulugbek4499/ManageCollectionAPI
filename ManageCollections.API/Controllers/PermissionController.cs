using System.Diagnostics.Metrics;
using ManageCollections.Application.DTOs.Permissions;
using ManageCollections.Application.Interfaces;
using ManageCollections.Application.Models.ResponseModels;
using ManageCollections.Domain.Entities.IdentityEntities;
using Microsoft.AspNetCore.Mvc;

namespace ManageCollections.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ApiController<Permission>
    {

        private readonly IPermissionRepository _permissionRepository;
        private readonly IPermissionRepository permissionRepository2;
        private readonly IPermissionRepository permissionRepository3;

/*        public PermissionController(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
      
        }*/

        public PermissionController(IPermissionRepository permissionRepository, 
                                    IPermissionRepository permissionRepository2, 
                                    IPermissionRepository permissionRepository3)
        {
            _permissionRepository = permissionRepository;
            this.permissionRepository2 = permissionRepository2;
            this.permissionRepository3 = permissionRepository3;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<ResponseCore<PermissionGetDTO>>> Create(PermissionCreateDTO permissionDTO)
        {
            Permission permission = _mapper.Map<Permission>(permissionDTO);
            var validationResult = _validator.Validate(permission);

            if (!validationResult.IsValid)
            {
                return BadRequest(new ResponseCore<object>(false, validationResult.Errors));
            }

            await _permissionRepository.CreateAsync(permission);

            var result = _mapper.Map<PermissionGetDTO>(permission);

            return Ok(new ResponseCore<object>(result));
        }


        [HttpGet("[Action]")]
        public async Task<ActionResult<IQueryable<PermissionGetDTO>>> GetAll()
        {
            /*  string[] strings = { "GetAllPermission", "GetAllRole", "GetAllOwner", "GetByIdRole", "GetByIdOwner",
              "UpdateRole", "CreateRole","DeleteRole",
              "UpdateOwner", "CreateOwner", "DeleteOwner"};
              foreach (string s in strings)
              {

                  await _permissionRepository.CreateAsync(new Permission() { PermissionName = s });
              }
  */
            IEnumerable<Permission> permissions = await _permissionRepository.GetAsync(x => true);
            IEnumerable<PermissionGetDTO> mappedPermissions = _mapper.Map<IEnumerable<PermissionGetDTO>>(permissions);

            return Ok(new ResponseCore<IEnumerable<PermissionGetDTO>>(mappedPermissions));
        }
    }
}
