using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoviesAPI.Models;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType (200, Type = typeof(List<Categoria>))]
        [ProducesResponseType (400)]
        public async Task<IActionResult> GetCategorys()
        {
            var list = await _context.Categorias.OrderBy(c => c.Category).ToListAsync();
            return Ok(list);
        }

        [HttpGet("{id:int}", Name = "GetCatery")]
        [ProducesResponseType(200, Type = typeof(Categoria))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public async Task<IActionResult> GetCategory(int id)
        {
            var obj = await _context.Categorias.FirstOrDefaultAsync(c => c.Id == id);

            if(obj == null)
            {
                return NotFound();
            }

            return Ok(obj);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]

        public async Task<IActionResult> CreateCategory([FromBody] Categoria categoria)
        {
            if(categoria == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _context.AddAsync(categoria);
            await _context.SaveChangesAsync();

            return CreatedAtRoute("GetCategory", new { id = categoria.Id }, categoria);
        }
    }
}
