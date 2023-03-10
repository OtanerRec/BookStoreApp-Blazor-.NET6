using AutoMapper;
using BookStoreApp.Api.Data;
using BookStoreApp.Api.DTO.Book;
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
    public class BooksController : ControllerBase
    {
        private readonly IBooksRepository _booksRespository;
        private readonly IMapper _mapper;
        private readonly ILogger<BooksController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BooksController(
            IMapper mapper,
            ILogger<BooksController> logger,
            IWebHostEnvironment webHostEnvironment,
            IBooksRepository booksRespository)
        {
            _mapper = mapper;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            _booksRespository = booksRespository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookReadOnlyDTO>>> GetBooks()
        {
            try
            {
                return Ok(await _booksRespository.GetAllBooksAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error performing GET in {nameof(GetBooks)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDetailsDTO>> GetBook(int id)
        {
            var bookDto = _booksRespository.GetBookAsync(id);

            if (bookDto == null)
            {
                _logger.LogWarning($"Record not found: {nameof(GetBook)} - Id: {id}");
                return NotFound();
            }

            return Ok(bookDto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutBook(int id, BookUpdateDTO bookDto)
        {
            if (id != bookDto.Id)
            {
                _logger.LogWarning($"Record not found: {nameof(PutBook)} - Id: {id}");
                return BadRequest();
            }

            var book = await _booksRespository.GetAsync(id);

            if (book == null)
            {
                _logger.LogWarning($"Record not found: {nameof(PutBook)} - Id: {id}");
                return NotFound();
            }

            if (string.IsNullOrEmpty(bookDto.ImageData) == false)
            {
                bookDto.Image = CreateFile(bookDto.ImageData, bookDto.OriginalImageName);

                var picName = Path.GetFileName(book.Image);
                var path = $"{_webHostEnvironment.WebRootPath}\\bookCoverImages\\{picName}";
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }

            _mapper.Map(bookDto, book);

            try
            {
                await _booksRespository.UpdateAsync(book);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!await BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    _logger.LogError(ex, $"Error performing PUT in {nameof(PutBook)}");
                    return StatusCode(500, Messages.Error500Message);
                }
            }

            return NoContent();
        }

        [HttpPost()]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<BookCreateDTO>> PostBook(BookCreateDTO bookDto)
        {
            try
            {                
                var book = _mapper.Map<Book>(bookDto);
                book.Image = CreateFile(bookDto.ImageData, bookDto.OriginalImageName);

                await _booksRespository.AddAsync(book);

                return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error performing POST in {nameof(PostBook)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                var book = await _booksRespository.GetAsync(id);
                if (book == null)
                {
                    _logger.LogWarning($"Record not found: {nameof(DeleteBook)} - Id: {id}");
                    return NotFound();
                }

                await _booksRespository.DeleteAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error performing DELETE in {nameof(DeleteBook)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        private string CreateFile(string imageBase64, string imageName)
        {
            var url = HttpContext.Request.Host.Value;
            var ext = Path.GetExtension(imageName);
            var fileName = $"{Guid.NewGuid()}{ext}";
            var path = $"{_webHostEnvironment.WebRootPath}\\bookCoverImages\\{fileName}";

            byte[] image = Convert.FromBase64String(imageBase64);

            var fileStream = System.IO.File.Create(path);
            fileStream.Write(image, 0, image.Length);
            fileStream.Close();

            return $"https://{url}/bookCoverImages/{fileName}";
        } 

        private async Task<bool> BookExists(int id)
        {
            return await _booksRespository.Exists(id);
        }
    }
}
