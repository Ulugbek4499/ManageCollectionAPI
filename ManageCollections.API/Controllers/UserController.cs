﻿using ManageCollections.Application.DTOs.Users;
using ManageCollections.Application.Interfaces;
using ManageCollections.Application.Models.ResponseModels;
using ManageCollections.Domain.Entities;
using ManageCollections.Domain.Entities.IdentityEntities;
using Microsoft.AspNetCore.Mvc;

namespace ManageCollections.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ApiController<User>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public UserController(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseCore<UserGetDTO>>> Create(UserCreateDTO userDTO)
        {
            User? user = _mapper.Map<User>(userDTO);
            var validationResult = _validator.Validate(user);

            if (!validationResult.IsValid)
            {
                return BadRequest(new ResponseCore<object>(false, validationResult.Errors));
            }

            user.Roles = new List<Role>();

            foreach (Guid item in userDTO.RoleIds)
            {
                Role? role = await _roleRepository.GetByIdAsync(item);
                if (role != null)
                    user.Roles.Add(role);
                else return BadRequest(new ResponseCore<string>(false, item + " Id not found"));
            }

            user = await _userRepository.CreateAsync(user);
            var res = _mapper.Map<UserGetDTO>(user);

            return Ok(new ResponseCore<object>(res));
        }


        [HttpGet("[action]")]
        //[Authorize(Roles = "GetAllOwner")]
        public async Task<ActionResult<ResponseCore<IEnumerable<UserGetDTO>>>> GetAll()
        {

            IEnumerable<User> owners = await _userRepository.GetAsync(x => true, nameof(Domain.Entities.User.Roles));

            IEnumerable<UserGetDTO> mappedOwners = _mapper.Map<IEnumerable<UserGetDTO>>(owners);

            return Ok(new ResponseCore<IEnumerable<UserGetDTO>>(mappedOwners));
        }

        [HttpGet("[action]")]
        //[Authorize(Roles="GetByIdUser")]
        public async Task<ActionResult<ResponseCore<IQueryable<UserGetDTO>>>> GetByIdUser(Guid Id)
        {
            IEnumerable<User> users = await _userRepository.GetAsync(x => true, nameof(Domain.Entities.User.Roles));
            User? user = users.FirstOrDefault(x => x.Id == Id);
            if (user == null)
            {
                return NotFound(new ResponseCore<Role?>(false, Id + " not found!"));
            }
            UserGetDTO userGetDTO = _mapper.Map<UserGetDTO>(user);
            return Ok(new ResponseCore<UserGetDTO?>(userGetDTO));
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<ResponseCore<UserGetDTO>>> Update([FromBody] UserUpdateDTO user)
        {
            User mappedUser = _mapper.Map<User>(user);
            var validationReusult = _validator.Validate(mappedUser);
            if (!validationReusult.IsValid)
            {
                return BadRequest(new ResponseCore<User>(false, validationReusult.Errors));
            }
            mappedUser.Roles = new List<Role>();

            foreach (Guid item in user.RoleIds)
            {
                Role? role = await _roleRepository.GetByIdAsync(item);

                if (role == null)
                    mappedUser.Roles.Add(role);

                else return BadRequest(new ResponseCore<User>(false, item + "Id not found"));
            }

            mappedUser = await _userRepository.UpdateAsync(mappedUser);

            if (mappedUser != null)
                return Ok(new ResponseCore<UserGetDTO>(_mapper.Map<UserGetDTO>(mappedUser)));

            return BadRequest(new ResponseCore<User>(false, user + " not found"));
        }

        [HttpDelete]
        public async Task<ActionResult<ResponseCore<bool>>> Delete(Guid id)
        {
            return await _userRepository.DeleteAsync(id) ?
                Ok(new ResponseCore<bool>(true))
                : BadRequest(new ResponseCore<bool>(false, "Delete failed!"));
        }
    }
}
