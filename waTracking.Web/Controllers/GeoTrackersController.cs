using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using waTracking.Data;
using waTracking.Entities.GeoTracker;
using waTracking.Web.Models.GeoTracker;

namespace waTracking.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeoTrackersController : ControllerBase
    {
        private readonly DbContextApp _context;

        public GeoTrackersController(DbContextApp context)
        {
            _context = context;
        }

        // GET: api/GeoTrackers
        [HttpGet]
        public IEnumerable<GeoTracker> GetGeoTrackers()
        {
            return _context.GeoTrackers;
        }

        // GET: api/GeoTrackers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGeoTracker([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var geoTracker = await _context.GeoTrackers.FindAsync(id);

            if (geoTracker == null)
            {
                return NotFound();
            }

            return Ok(geoTracker);
        }

        //GET  api/GeoTrackers/GetByIdentifier/string
        [HttpGet("[action]/{strIdentifier}")]
        public async Task<IActionResult> GetByIdentifier([FromRoute] string strIdentifier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var geoTracker = await _context.GeoTrackers.Where(x => x.Identifier == strIdentifier).FirstOrDefaultAsync();

            if (geoTracker == null)
            {
                return NotFound();
            }

            return Ok(geoTracker);
        }



        // POST: api/GeoTrackers
        [HttpPost("[action]")]
        public async Task<IActionResult> Create ([FromBody] CreateGeoTrackerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            GeoTracker geoTracker = new GeoTracker
            {
                Enabled = true,
                GpsModel= model.GpsModel,
                Identifier= model.Identifier,
                CreationDate = DateTime.Now                
            };

            _context.GeoTrackers.Add(geoTracker);
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


        private bool GeoTrackerExists(int id)
        {
            return _context.GeoTrackers.Any(e => e.Id == id);
        }
    }
}