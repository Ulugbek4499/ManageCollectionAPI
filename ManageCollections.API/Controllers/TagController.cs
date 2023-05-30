using ManageCollections.Application.DTOs.Tags;
using ManageCollections.Application.Interfaces;
using ManageCollections.Application.Models.ResponseModels;
using ManageCollections.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ManageCollections.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ApiController<Tag>
    {
        private readonly ITagRepository _tagRepository;
        private readonly IItemRepository _itemRepository;

        public TagController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        [HttpPost("[action]")]
        //[Authorize(Roles = "CreateRole")]
        public async Task<ActionResult<ResponseCore<TagGetDTO>>> Create(TagCreateDTO tagCreateDTO)
        {
            Tag tag = _mapper.Map<Tag>(tagCreateDTO);
            var validationResult = _validator.Validate(tag);

            if (!validationResult.IsValid)
            {
                return BadRequest(new ResponseCore<object>(false, validationResult.Errors));
            }

            tag.Items = new List<Item>();

            foreach (Guid item in tagCreateDTO.ItemIds)
            {
                Item? items = await _itemRepository.GetByIdAsync(item);
                if (items != null)
                    tag.Items.Add(items);
                else return BadRequest(new ResponseCore<string>(false, item + " Id not found"));
            }

            tag = await _tagRepository.CreateAsync(tag);
            TagGetDTO res = _mapper.Map<TagGetDTO>(tag);

            return Ok(new ResponseCore<object>(res));
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<ResponseCore<IEnumerable<TagGetDTO>>>> GetAll()
        {
            IEnumerable<Tag> tags = await _tagRepository.GetAsync(x => true, nameof(Tag.Items));
            IEnumerable<TagGetDTO> tagGetDTOs = _mapper.Map<IEnumerable<TagGetDTO>>(tags);

            return Ok(new ResponseCore<IEnumerable<TagGetDTO>>(tagGetDTOs));
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<ResponseCore<TagGetDTO>>> GetById(Guid Id)
        {
            IEnumerable<Tag> tags = await _tagRepository.GetAsync(x => true, nameof(Tag.Items));
            Tag? tag = tags.FirstOrDefault(x => x.Id == Id);
            if (tag == null)
            {
                return NotFound(new ResponseCore<Tag?>(false, Id + " not found!"));
            }
            TagGetDTO mappedTag = _mapper.Map<TagGetDTO>(tag);
            return Ok(new ResponseCore<TagGetDTO?>(mappedTag));
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<ResponseCore<TagGetDTO>>> Update(TagUpdateDTO user)
        {
            Tag mappedTag = _mapper.Map<Tag>(user);
            var validationReusult = _validator.Validate(mappedTag);
            if (!validationReusult.IsValid)
            {
                return BadRequest(new ResponseCore<Tag>(false, validationReusult.Errors));
            }
            mappedTag.Items = new List<Item>();

            foreach (Guid item in user.ItemIds)
            {
                Item? items = await _itemRepository.GetByIdAsync(item);

                if (items == null)
                    mappedTag.Items.Add(items);

                else return BadRequest(new ResponseCore<Tag>(false, item + "Id not found"));
            }

            mappedTag = await _tagRepository.UpdateAsync(mappedTag);

            if (mappedTag != null)
                return Ok(new ResponseCore<TagGetDTO>(_mapper.Map<TagGetDTO>(mappedTag)));

            return BadRequest(new ResponseCore<Tag>(false, user + " not found"));
        }

        [HttpDelete]
        public async Task<ActionResult<ResponseCore<bool>>> Delete(Guid id)
        {
            return await _tagRepository.DeleteAsync(id) ?
                Ok(new ResponseCore<bool>(true))
                : BadRequest(new ResponseCore<bool>(false, "Delete failed!"));
        }
    }
}
