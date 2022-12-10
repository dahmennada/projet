using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebService.Data;
using WebService.Data.Model;

namespace WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CongéController : ControllerBase
    {
        private readonly DataContext _context;

        public CongéController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Congé
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Congé>>> GetCongés()
        {
            return await _context.Congés.ToListAsync();
        }

        // GET: api/Congé/5
        [Authorize]

        [HttpGet("{id}")]
        public async Task<ActionResult<Congé>> GetCongé(int id)
        {
            var congé = await _context.Congés.FindAsync(id);

            if (congé == null)
            {
                return NotFound();
            }

            return congé;
        }

        // PUT: api/Congé/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCongé(int id, Congé congé)
        {
            if (id != congé.Id)
            {
                return BadRequest();
            }

            _context.Entry(congé).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CongéExists(id))
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

        // POST: api/Congé
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Congé>> PostCongé(Congé congé)
        {
            int id = Convert.ToInt32(HttpContext.User.FindFirstValue("userID"));
            int solde = Convert.ToInt32(HttpContext.User.FindFirstValue("userSolde"));
            int TempsTravail = Convert.ToInt32(HttpContext.User.FindFirstValue("userTemps"));


            congé.AjoutPar = id;
            if ((congé.type == "Payé" && solde > 30) || (congé.type == "maternité" && solde > 61) || (congé.type == "paternité" && solde > 3) || (congé.type == "RTT" && TempsTravail<35) || (congé.type == "RTT" && solde > 15))
            {
                return BadRequest("vous ne pouvez pas profiter de ce congé");
                
            }
            else
            {
                _context.Congés.Add(congé);
                await _context.SaveChangesAsync();
            }

            
            
            return CreatedAtAction("GetCongé", new { id = congé.Id }, congé);
        }

        // DELETE: api/Congé/5
        [Authorize]

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCongé(int id)
        {
            var congé = await _context.Congés.FindAsync(id);
            if (congé == null)
            {
                return NotFound();
            }

            _context.Congés.Remove(congé);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CongéExists(int id)
        {
            return _context.Congés.Any(e => e.Id == id);
        }


        [HttpGet]
        [Route("Users")]
        public async Task<IActionResult> GetUsers()
        {
            return Ok("you have this");
        }

        [HttpGet]
        [Route("Users/{id}")]
        public async Task<IActionResult> GetUserId(int id)
        {
            return Ok(new { userID = id });
        }

        [Authorize]
        [HttpGet]
        [Route("Users/current")]
        public async Task<IActionResult> GetUserLogged()
        {
            int id = Convert.ToInt32(HttpContext.User.FindFirstValue("userID"));
            return Ok(new { userID = id });
        }

        [Authorize]
        [HttpGet]
        [Route("Users/currentSolde")]
        public async Task<IActionResult> GetUserSolde()
        {
            int id = Convert.ToInt32(HttpContext.User.FindFirstValue("userSolde"));
            return Ok(new { userSolde = id });
        }



        [Authorize]
        [HttpGet]
        [Route("Users/currentTemps")]
        public async Task<IActionResult> GetUserTemps()
        {
            int id = Convert.ToInt32(HttpContext.User.FindFirstValue("userTemps"));
            return Ok(new { userTemps = id });
        }
    }
}
