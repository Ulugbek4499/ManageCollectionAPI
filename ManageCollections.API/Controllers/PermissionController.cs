using System.Diagnostics.Metrics;
using ManageCollections.API.Filters;
using ManageCollections.Application.DTOs.Permissions;
using ManageCollections.Application.Interfaces;
using ManageCollections.Application.Models;
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

        public PermissionController(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
      
        }

        [LoggingFilter]
        [CustomHeader("Added name to the header", "Added Value to the header")]
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
        public async Task<ActionResult<PaginatedList<PermissionGetDTO>>> GetAll()
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

        [HttpGet]
        public async Task<ActionResult<ResponseCore<PaginatedList<Permission>>>> GetAllProducts(int page = 1, int pageSize = 10)
        {
            IQueryable<Permission> Products = await _permissionRepository.GetAsync(x => true);

            PaginatedList<Permission> products = await PaginatedList<Permission>.CreateAsync(Products, page, pageSize);

            ResponseCore<PaginatedList<Permission>> res = new()
            {
                Result = products
            };
            return Ok(res);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<ResponseCore<PaginatedList<Permission>>>> Search(string text, int page = 1, int pageSize = 10)
        {
            IQueryable<Permission> AllPermissions = await _permissionRepository.GetAsync(x=>true);

            IQueryable<Permission> Permissions = AllPermissions.Where(x => x.PermissionName.Contains(text));

            PaginatedList<Permission> permissions = await PaginatedList<Permission>.CreateAsync(Permissions, page, pageSize);

            ResponseCore<PaginatedList<Permission>> res = new()
            {
                Result = permissions
            };
            return Ok(res);
        }

    }
}
