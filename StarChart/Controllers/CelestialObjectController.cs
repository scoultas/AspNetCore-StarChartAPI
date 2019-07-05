using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StarChart.Data;

namespace StarChart.Controllers
{
    [Route("")]
    [ApiController]
    public class CelestialObjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CelestialObjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var result = _context.CelestialObjects.FirstOrDefault(x => x.Id == id);
            if (result == null)
            {
                return NotFound();
            }

            result.Satellites = _context.CelestialObjects.Where(x => x.OrbitedObjectId == id).ToList();

            return Ok(result);
        }

        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            var result = _context.CelestialObjects.FirstOrDefault(x => x.Name == name);
            if (result == null)
            {
                return NotFound();
            }

            result.Satellites = _context.CelestialObjects.Where(x => x.OrbitedObjectId == result.Id).ToList();

            return Ok(result);

        }

        [HttpGet()]
        public IActionResult GetAll()
        {
            var result = _context.CelestialObjects.ToList();

            foreach (var item in result)
            {
                item.Satellites = _context.CelestialObjects.Where(x => x.OrbitedObjectId == item.Id).ToList();
            }

            return Ok(result);
        }
    }
}
