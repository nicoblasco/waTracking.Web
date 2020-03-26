using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using waTracking.Data;
using waTracking.Entities.Georeference;
using waTracking.Web.Models.Georeference;

namespace waTracking.Web.Controllers
{
    [EnableCors("SiteCorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class GeoreferencesController : ControllerBase
    {
        private readonly DbContextApp _context;

        public GeoreferencesController(DbContextApp context)
        {
            _context = context;
        }

        // GET: api/Georeferences
        [HttpGet]
        public IEnumerable<Georeference> GetGeoreferences()
        {
            return _context.Georeferences;
        }

        // GET: api/Georeferences/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGeoreference([FromRoute] Int64 id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var georeference = await _context.Georeferences.FindAsync(id);

            if (georeference == null)
            {
                return NotFound();
            }

            return Ok(georeference);
        }


        // GET: api/Georeferences/GetCurrentGeoreferences/
        [HttpGet("[action]")]
        public IEnumerable<Georeference> GetCurrentGeoreferences()
        {
            return _context.Georeferences.GroupBy(x=>x.GeoTrackerId).SelectMany(g => g.OrderByDescending(pm => pm.CreationDate).Take(1));
            //.Where(x => x.GeoTracker.Enabled == true).OrderByDescending(x => x.CreationDate).GroupBy(x=>x.GeoTrackerId).FirstOrDefault();



        }

        // POST: api/Georeferences/Create
        [HttpPost("[action]")]
        public async Task<IActionResult> Create ([FromBody] CreateGeoreferenceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Georeference georeference = new Georeference
            {
                Identifier= model.Identifier,
                CreationDate = DateTime.Now,
                Enabled = true,
                GeoTrackerId= model.GeoTrackerId,
                Latitude=model.Latitude,
                Longitude=model.Longitude,
                TrackerLogId = model.TrackerLogId

            };

            _context.Georeferences.Add(georeference);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                return BadRequest();
            }

            return Ok();
        }



        private bool GeoreferenceExists(int id)
        {
            return _context.Georeferences.Any(e => e.Id == id);
        }
    }
}