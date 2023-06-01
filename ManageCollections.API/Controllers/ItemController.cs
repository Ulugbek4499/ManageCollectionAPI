using ManageCollections.API.Filters;
using ManageCollections.Application.DTOs.Items;
using ManageCollections.Application.Interfaces;
using ManageCollections.Application.Models.ResponseModels;
using ManageCollections.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ManageCollections.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ApiController<Item>
    {
        private readonly IItemRepository _itemRepository;

        public ItemController(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        [HttpPost("[action]")]
        //[Authorize(Roles = "CreateRole")]
        public async Task<ActionResult<ResponseCore<ItemGetDTO>>> Create(ItemCreateDTO itemCreateDTO)
        {
            Item item = _mapper.Map<Item>(itemCreateDTO);
            var validationResult = _validator.Validate(item);

            if (!validationResult.IsValid)
            {
                return BadRequest(new ResponseCore<object>(false, validationResult.Errors));
            }

            item = await _itemRepository.CreateAsync(item);
            var res = _mapper.Map<ItemGetDTO>(item);
            return Ok(new ResponseCore<object>(res));
        }

        [HttpGet("[action]")]
        //[Authorize(Roles = "GetAllRole")]
        public async Task<ActionResult<ResponseCore<IEnumerable<ItemGetDTO>>>> GetAll()
        {
            IEnumerable<Item> items = await _itemRepository.GetAsync(x => true, nameof(Item.Comments), nameof(Item.Tags));

            IEnumerable<ItemGetDTO> itemGetDTOs = _mapper.Map<IEnumerable<ItemGetDTO>>(items);

            return Ok(new ResponseCore<IEnumerable<ItemGetDTO>>(itemGetDTOs));
        }

        [HttpGet("[action]")]
        //[Authorize(Roles = "GetByIdRole")]
        public async Task<ActionResult<ResponseCore<ItemGetDTO>>> GetById(Guid id)
        {
            IEnumerable<Item> items = await _itemRepository.GetAsync(x => true, nameof(Item.Comments), nameof(Item.Tags));
            Item? item = items.FirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                return NotFound(new ResponseCore<Item?>(false, id + " not found!"));
            }
            ItemGetDTO mappedItem = _mapper.Map<ItemGetDTO>(item);
            return Ok(new ResponseCore<ItemGetDTO?>(mappedItem));
        }

        [HttpPut("[action]")]
        //[Authorize(Roles = "UpdateRole")]
        public async Task<ActionResult<ResponseCore<ItemGetDTO>>> Update(ItemUpdateDTO itemUpdateDTO)
        {
            Item? item = _mapper.Map<Item>(itemUpdateDTO);
            var validationResult = _validator.Validate(item);

            if (!validationResult.IsValid)
            {
                return BadRequest(new ResponseCore<Item>(false, validationResult.Errors));
            }

            item = await _itemRepository.UpdateAsync(item);

            if (item != null)
                return Ok(new ResponseCore<ItemGetDTO>(_mapper.Map<ItemGetDTO>(item)));

            return BadRequest(new ResponseCore<Item>(false, itemUpdateDTO + " not found"));
        }


        [HttpDelete("[action]")]
        //[Authorize(Roles = "DeleteRole")]
        public async Task<ActionResult<ResponseCore<bool>>> Delete(Guid id)
        {
            return await _itemRepository.DeleteAsync(id) ?
                   Ok(new ResponseCore<bool>(true))
                   : BadRequest(new ResponseCore<bool>(false, "Delete failed!"));
        }
    }
}
