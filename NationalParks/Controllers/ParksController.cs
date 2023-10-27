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
  public class NationalParksController : ControllerBase
  {
    private readonly NationalParksContext _db;

    public NationalParksController(NationalParksContext db)
    {
      _db = db;
    }

    [HttpGet]
    public async Task<List<Park>> Get(string name, string state, string description, int annualVisitors, int page = 1, int pageSize = 2)
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
  }
}