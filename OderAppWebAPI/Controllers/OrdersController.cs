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
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Orders/proposed
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetProposedOrders(Order order)
        {
            return await _context.Order.Include(c => c.Customer).Where(o => o.OrderStatus == "PROPOSED").ToListAsync(); // payattention to the where clause inside of the statement
        }

        // PUT: api/orders/Edit/5
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> SetOrderStatusToEdit(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            order.OrderStatus = "EDIT";
            return await PutOrder(order.Id, order); 
        }


        // PUT: api/orders/Proposed/5
        [HttpPut("proposed/{id}")]
        public async Task<IActionResult> SetOrderStatusToPropose(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            order.OrderStatus = (order.OrderTotal <= 100) ? "FINAL" : "PROPOSED";   //ternary operator (if the bool is true then the first quote is used)
            return await PutOrder(order.Id, order);                                 // if it is false it will use the second quote
        }

        // PUT: api/orders/Final/5
        [HttpPut("final/{id}")]
        public async Task<IActionResult> SetOrderStatusToFinal(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            order.OrderStatus = "FINAL";
            return await PutOrder(order.Id, order);
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrder()
        {
            return await _context.Order.Include(c => c.Customer).ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Order
                .Include(s => s.SalesPerson)
                .Include(c => c.Customer)               // PLEASE PAY ATTENTION TO THIS!!!!
                .Include(l => l.Orderlines)             // This is how we got all the info how we wanted
                .ThenInclude(i => i.Item)
                .SingleOrDefaultAsync(o=> o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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

        // POST: api/Orders
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            _context.Order.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Order>> DeleteOrder(int id)
        {
            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Order.Remove(order);
            await _context.SaveChangesAsync();

            return order;
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.Id == id);
        }
    }
}
