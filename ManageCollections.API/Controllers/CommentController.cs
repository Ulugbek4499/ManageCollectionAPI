using ManageCollections.Application.DTOs.Comments;
using ManageCollections.Application.Interfaces;
using ManageCollections.Application.Models.ResponseModels;
using ManageCollections.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ManageCollections.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ApiController<Comment>
    {
        private readonly ICommentRepository _commentRepository;

        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        [HttpPost("[action]")]
        //[Authorize(Roles = "CreateRole")]
        public async Task<ActionResult<ResponseCore<CommentGetDTO>>> Create(CommentCreateDTO commentCreateDTO)
        {
            Comment comment = _mapper.Map<Comment>(commentCreateDTO);
            var validationResult = _validator.Validate(comment);

            if (!validationResult.IsValid)
            {
                return BadRequest(new ResponseCore<object>(false, validationResult.Errors));
            }

            comment = await _commentRepository.CreateAsync(comment);
            var res = _mapper.Map<CommentGetDTO>(comment);

            return Ok(new ResponseCore<object>(res));
        }

        [HttpGet("[action]")]
        //[Authorize(Roles = "GetAllRole")]
        public async Task<ActionResult<ResponseCore<IEnumerable<CommentGetDTO>>>> GetAll()
        {
            IEnumerable<Comment> comments = await _commentRepository.GetAsync(x => true);

            IEnumerable<CommentGetDTO> commentGetDTOs = _mapper.Map<IEnumerable<CommentGetDTO>>(comments);

            return Ok(new ResponseCore<IEnumerable<CommentGetDTO>>(commentGetDTOs));
        }

        [HttpGet("[action]")]
        //[Authorize(Roles = "GetByIdRole")]
        public async Task<ActionResult<ResponseCore<CommentGetDTO>>> GetById(Guid id)
        {
            IEnumerable<Comment> comments = await _commentRepository.GetAsync(x => true);
            Comment? comment = comments.FirstOrDefault(x => x.Id == id);
            if (comment == null)
            {
                return NotFound(new ResponseCore<Comment?>(false, id + " not found!"));
            }
            CommentGetDTO mappedComment = _mapper.Map<CommentGetDTO>(comment);
            return Ok(new ResponseCore<CommentGetDTO?>(mappedComment));
        }

        [HttpPut("[action]")]
        //[Authorize(Roles = "UpdateRole")]
        public async Task<ActionResult<ResponseCore<CommentGetDTO>>> Update([FromBody] CommentUpdateDTO commentUpdateDTO)
        {
            Comment? comment = _mapper.Map<Comment>(commentUpdateDTO);
            var validationResult = _validator.Validate(comment);

            if (!validationResult.IsValid)
            {
                return BadRequest(new ResponseCore<Comment>(false, validationResult.Errors));
            }

            comment = await _commentRepository.UpdateAsync(comment);

            if (comment != null)
                return Ok(new ResponseCore<CommentGetDTO>(_mapper.Map<CommentGetDTO>(comment)));

            return BadRequest(new ResponseCore<Comment>(false, commentUpdateDTO + " not found"));
        }


        [HttpDelete("[action]")]
        //[Authorize(Roles = "DeleteRole")]
        public async Task<ActionResult<ResponseCore<bool>>> Delete(Guid id)
        {
            return await _commentRepository.DeleteAsync(id) ?
                   Ok(new ResponseCore<bool>(true))
                   : BadRequest(new ResponseCore<bool>(false, "Delete failed!"));
        }
    }
}
