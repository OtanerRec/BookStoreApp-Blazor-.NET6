using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStoreApp.Api.Data;
using BookStoreApp.Api.DTO;
using BookStoreApp.Api.DTO.Author;
using BookStoreApp.Api.Repositories;
using BookStoreApp.Api.Static;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorsRepository _authorsRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthorsController> _logger;

        public AuthorsController(
            IMapper mapper,
            ILogger<AuthorsController> logger,
            IAuthorsRepository authorsRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _authorsRepository = authorsRepository;
        }

        //GET: api/Authors/?startindex=0&pagesize=15
        [HttpGet]
        public async Task<ActionResult<VirtualizeResponse<AuthorReadOnlyDTO>>> GetAuthors([FromQuery]QueryParameters queryParameters)
        {
            try
            {
                return Ok(await _authorsRepository.GetAllAsync<AuthorReadOnlyDTO>(queryParameters));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error performing GET in {nameof(GetAuthors)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        //GET: api/Authors/
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<AuthorReadOnlyDTO>>> GetAuthors()
        {
            try
            {
                return Ok(_mapper.Map<IEnumerable<AuthorReadOnlyDTO>>(await _authorsRepository.GetAllAsync()));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error performing GET in {nameof(GetAuthors)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDetailsDTO>> GetAuthor(int id)
        {
            try
            {
                var author = _authorsRepository.GetAuthorDetailsAsync(id);

                if (author == null)
                {
                    _logger.LogWarning($"Record not found: {nameof(GetAuthor)} - Id: {id}");
                    return NotFound();
                }

                return Ok(author);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error performing GET in {nameof(GetAuthor)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutAuthor(int id, AuthorUpdateDTO authorUpdateDTO)
        {
            if (id != authorUpdateDTO.Id)
            {
                _logger.LogWarning($"Record not found: {nameof(PutAuthor)} - Id: {id}");
                return BadRequest();
            } 

            var author = await _authorsRepository.GetAsync(id);

            if (author == null)
            {
                _logger.LogWarning($"Record not found: {nameof(PutAuthor)} - Id: {id}");
                return NotFound();
            }

            _mapper.Map(authorUpdateDTO, author);

            try
            {
                await _authorsRepository.UpdateAsync(author);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!await AuthorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    _logger.LogError(ex, $"Error performing PUT in {nameof(PutAuthor)}");
                    return StatusCode(500, Messages.Error500Message);
                }
            }

            return NoContent();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<AuthorReadOnlyDTO>> PostAuthor(AuthorCreateDTO authorCreateDto)
        {
            try
            {
                var author = _mapper.Map<Author>(authorCreateDto);
                await _authorsRepository.AddAsync(author);

                return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error performing POST in {nameof(PostAuthor)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            try
            {
                var author = await _authorsRepository.GetAsync(id);

                if (author == null)
                {
                    _logger.LogWarning($"Record not found: {nameof(PutAuthor)} - Id: {id}");
                    return NotFound();
                }

                _authorsRepository.DeleteAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error performing DELETE in {nameof(DeleteAuthor)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        private async Task<bool> AuthorExists(int id)
        {
            return await _authorsRepository.Exists(id);
        }
    }
}
