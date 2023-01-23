using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestWithAspNetUdemy.Business;
using RestWithAspNetUdemy.Model;
using System.Collections.Generic;

namespace RestWithAspNetUdemy.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Authorize("Bearer")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly ILogger<BooksController> _logger;
        private IBooksBusiness _booksBusiness;

        public BooksController(ILogger<BooksController> logger, IBooksBusiness book) 
        {
            _logger = logger;
            _booksBusiness = book;
        }

        [HttpGet]
        [ProducesResponseType((200), Type = typeof(List<BooksVO>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Get() 
        { 
            return Ok(_booksBusiness.FindAll());
        }

        [HttpGet("{id}")]
        [ProducesResponseType((200), Type = typeof(BooksVO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Get(long id)
        {
            var book = _booksBusiness.FindById(id);

            if (book == null)
                return NotFound();

            return Ok(book);
        }

        [HttpPost]
        [ProducesResponseType((200), Type = typeof(BooksVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Post([FromBody] BooksVO book)
        {
            if(book == null)
                return BadRequest();

            return Ok(_booksBusiness.Create(book));
        }

        [HttpPut]
        [ProducesResponseType((200), Type = typeof(BooksVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Put([FromBody] BooksVO book)
        {
            if (book == null)
                return BadRequest();

            return Ok(_booksBusiness.Update(book));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Delete(long id) 
        {
            _booksBusiness.Delete(id);
            return NoContent();
        }
    }
}
