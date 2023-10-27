using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using NationalParks.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace NationalParks.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize]
  public class ParksController : ControllerBase
  {
    private readonly NationalParksContext _db;

    public ParksController(NationalParksContext db)
    {
      _db = db;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<List<Park>> Get(string name, string state, string description, int annualVisitors, int page = 1, int pageSize = 6)
    {
      IQueryable<Park> query = _db.Parks.AsQueryable();

      if (name != null)
      {
        query = query.Where(entry => entry.Name == name);
      }

      if (state != null)
      {
        query = query.Where(entry => entry.State == state);
      }

      int skip = (page - 1) * pageSize;

      List<Park> result = await query
        .Skip(skip)
        .Take(pageSize)
        .ToListAsync();

      return result;
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult<Park>> GetPark(int id)
    {
      Park park = await _db.Parks.FindAsync(id);

      if (park == null)
      {
        return NotFound();
      }

      return park;
    }

    [HttpPost]
    public async Task<ActionResult<Park>> Post(Park park)
    {
      _db.Parks.Add(park);
      await _db.SaveChangesAsync();
      return CreatedAtAction(nameof(GetPark), new { id = park.ParkId }, park);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Park park)
    {
      if (id != park.ParkId)
      {
        return BadRequest();
      }

      _db.Parks.Update(park);

      try
      {
        await _db.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!ParkExists(id))
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

    private bool ParkExists(int id)
    {
      return _db.Parks.Any(e => e.ParkId == id);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePark(int id)
    {
      Park park = await _db.Parks.FindAsync(id);
      if (park == null)
      {
        return NotFound();
      }

      _db.Parks.Remove(park);
      await _db.SaveChangesAsync();

      return NoContent();
    }

    [AllowAnonymous]
    [HttpGet("random")]
    public async Task<ActionResult<Park>> GetRandomPark()
    {
      // query database to 'count' the number of parks
      int count = await _db.Parks.CountAsync();
      // generate 'random' number to the instance
      Random randomPark = new Random();
      // random number will be used as the index to select random park, 'next' generates the random number each time it's called
      int index = randomPark.Next(count);
      // 'skip' the first index and selects the first park after skipping
      Park park = await _db.Parks.Skip(index).FirstOrDefaultAsync();
      // if no park is found at the specified index, FirstOrDefaultAsync() returns null

      if (park == null)
      {
        return NotFound();
      }

      return park;
    }

    [AllowAnonymous]
    [HttpGet("search")]
    public async Task<ActionResult<List<Park>>> SearchPark([FromQuery] string state, [FromQuery] string name, [FromQuery] int? annualVisitors)
    {
      IQueryable<Park> parkQuery = _db.Parks.AsQueryable();

      if (!string.IsNullOrEmpty(state))
      {
        parkQuery = parkQuery.Where(entry => entry.State.Contains(state));
      }

      if (!string.IsNullOrEmpty(name))
      {
        parkQuery = parkQuery.Where(entry => entry.Name.Contains(name));
      }

      if (annualVisitors.HasValue)
      {
        parkQuery = parkQuery.Where(entry => entry.AnnualVisitors == annualVisitors);
      }

      return await parkQuery.ToListAsync();
    }
  }
}

