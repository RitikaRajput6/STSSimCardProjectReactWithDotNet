using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using STSSimCardProjectReactWithDotNet.Data;
using STSSimCardProjectReactWithDotNet.Models;

namespace STSSimCardProjectReactWithDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimCardsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SimCardsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/SimCards
        [HttpGet]
        
        public ActionResult GetsimCards()
        {


           
            var result = (from simCard in _context.SimCards
                          join user in _context.Customers on simCard.CustomersId equals user.Id
                          join provider in _context.Providers on simCard.ProviderId equals provider.Id
                          join device in _context.Devicess on simCard.DevicesId equals device.Id
                          select new
                          {
                              Number = simCard.Number,
                              IsActiveUser = simCard.IsActiveUser,
                              FirstName = userModel.FirstName,
                              LastName = userModel.LastName,
                              ProviderName = provider.ProviderName,
                              DeviceName = device.DeviceName
                          }).ToList();


            return Ok(result);
        }

        // GET: api/SimCards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SimCard>> GetSimCard(int id)
        {
          if (_context.SimCards == null)
          {
              return NotFound();
          }
            var simCard = await _context.SimCards.FindAsync(id);

            if (simCard == null)
            {
                return NotFound();
            }

            return simCard;
        }

        // PUT: api/SimCards/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSimCard(int id, SimCard simCard)
        {
            if (id != simCard.Id)
            {
                return BadRequest();
            }

            _context.Entry(simCard).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SimCardExists(id))
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

        // POST: api/SimCards
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SimCard>> PostSimCard(SimCard simCard)
        {
          if (_context.SimCards == null)
          {
              return Problem("Entity set 'ApplicationDbContext.SimCards'  is null.");
          }
            _context.SimCards.Add(simCard);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSimCard", new { id = simCard.Id }, simCard);
        }

        // DELETE: api/SimCards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSimCard(int id)
        {
            if (_context.SimCards == null)
            {
                return NotFound();
            }
            var simCard = await _context.SimCards.FindAsync(id);
            if (simCard == null)
            {
                return NotFound();
            }

            _context.SimCards.Remove(simCard);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SimCardExists(int id)
        {
            return (_context.SimCards?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
