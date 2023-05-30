using ManageCollections.Application.DTOs.Collections;
using ManageCollections.Application.Interfaces;
using ManageCollections.Application.Models.ResponseModels;
using ManageCollections.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ManageCollections.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollectionController : ApiController<Collection>
    {
        private readonly ICollectionRepository _collectionRepository;

        public CollectionController(ICollectionRepository collectionRepository)
        {
            _collectionRepository = collectionRepository;
        }

        [HttpPost("[action]")]
        //[Authorize(Roles = "CreateRole")]
        public async Task<ActionResult<ResponseCore<CollectionGetDTO>>> Create(CollectionCreateDTO collectionCreateDTO)
        {
            Collection collection = _mapper.Map<Collection>(collectionCreateDTO);
            var validationResult = _validator.Validate(collection);

            if (!validationResult.IsValid)
            {
                return BadRequest(new ResponseCore<object>(false, validationResult.Errors));
            }

            collection = await _collectionRepository.CreateAsync(collection);
            var res = _mapper.Map<CollectionGetDTO>(collection);
            return Ok(new ResponseCore<object>(res));
        }

        [HttpGet("[action]")]
        //[Authorize(Roles = "GetAllRole")]
        public async Task<ActionResult<ResponseCore<IEnumerable<CollectionGetDTO>>>> GetAll()
        {
            IEnumerable<Collection> collections = await _collectionRepository.GetAsync(x => true, nameof(Collection.Items));

            IEnumerable<CollectionGetDTO> collectionGetDTOs = _mapper.Map<IEnumerable<CollectionGetDTO>>(collections);

            return Ok(new ResponseCore<IEnumerable<CollectionGetDTO>>(collectionGetDTOs));
        }

        [HttpGet("[action]")]
        //[Authorize(Roles = "GetByIdRole")]
        public async Task<ActionResult<ResponseCore<CollectionGetDTO>>> GetById(Guid Id)
        {
            IEnumerable<Collection> collections = await _collectionRepository.GetAsync(x => true, nameof(Collection.Items));
            Collection? collection = collections.FirstOrDefault(x => x.Id == Id);
            if (collection == null)
            {
                return NotFound(new ResponseCore<Collection?>(false, Id + " not found!"));
            }
            CollectionGetDTO collectionGetDTO = _mapper.Map<CollectionGetDTO>(collection);
            return Ok(new ResponseCore<CollectionGetDTO?>(collectionGetDTO));
        }

        [HttpPut("[action]")]
        //[Authorize(Roles = "UpdateRole")]
        public async Task<ActionResult<ResponseCore<CollectionGetDTO>>> Update(CollectionUpdateDTO collectionUpdateDTO)
        {
            Collection? collection = _mapper.Map<Collection>(collectionUpdateDTO);
            var validationResult = _validator.Validate(collection);

            if (!validationResult.IsValid)
            {
                return BadRequest(new ResponseCore<Collection>(false, validationResult.Errors));
            }

            collection = await _collectionRepository.UpdateAsync(collection);

            if (collection != null)
                return Ok(new ResponseCore<CollectionGetDTO>(_mapper.Map<CollectionGetDTO>(collection)));

            return BadRequest(new ResponseCore<Collection>(false, collectionUpdateDTO + " not found"));
        }

        [HttpDelete("[action]")]
        //[Authorize(Roles = "DeleteRole")]
        public async Task<ActionResult<ResponseCore<bool>>> Delete(Guid id)
        {
            return await _collectionRepository.DeleteAsync(id) ?
                   Ok(new ResponseCore<bool>(true))
                   : BadRequest(new ResponseCore<bool>(false, "Delete failed!"));
        }

    }
}
