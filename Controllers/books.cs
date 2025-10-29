using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TodoApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BloggingContext _db;

        // ← AGREGAR: Constructor para recibir DI
        public BooksController(BloggingContext db)
        {
            _db = db;
        }

        [HttpGet(Name = "GetBooks")]
        public async Task<ActionResult<List<Book>>> GetBooks()
        {
            // ← CAMBIAR: Quitar "using var db = new BloggingContext();"
            return await _db.Books.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            // ← CAMBIAR: Quitar "using var db = new BloggingContext();"
            _db.Books.Add(book);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBooks), new { id = book.Id }, book);
        }
    }
}
