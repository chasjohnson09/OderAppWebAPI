using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OderAppWebAPI.Data;
using OderAppWebAPI.Models;

namespace OderAppWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesPersonsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SalesPersonsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/SalesPersons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalesPerson>>> GetSalesPerson()
        {
            return await _context.SalesPerson.ToListAsync();
        }

        // GET: api/SalesPersons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SalesPerson>> GetSalesPerson(int id)
        {
            var salesPerson = await _context.SalesPerson.FindAsync(id);

            if (salesPerson == null)
            {
                return NotFound();
            }

            return salesPerson;
        }

        // PUT: api/SalesPersons/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalesPerson(int id, SalesPerson salesPerson)
        {
            if (id != salesPerson.Id)
            {
                return BadRequest();
            }

            _context.Entry(salesPerson).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalesPersonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/SalesPersons
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SalesPerson>> PostSalesPerson(SalesPerson salesPerson)
        {
            _context.SalesPerson.Add(salesPerson);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSalesPerson", new { id = salesPerson.Id }, salesPerson);
        }

        // DELETE: api/SalesPersons/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SalesPerson>> DeleteSalesPerson(int id)
        {
            var salesPerson = await _context.SalesPerson.FindAsync(id);
            if (salesPerson == null)
            {
                return NotFound();
            }

            _context.SalesPerson.Remove(salesPerson);
            await _context.SaveChangesAsync();

            return salesPerson;
        }

        private bool SalesPersonExists(int id)
        {
            return _context.SalesPerson.Any(e => e.Id == id);
        }
    }
}
