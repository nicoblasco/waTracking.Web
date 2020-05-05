using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using waTracking.Data;
using waTracking.Entities.Vehicle;
using waTracking.Web.Models.Vehicle.Segment;

namespace waTracking.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleSegmentsController : ControllerBase
    {
        private readonly DbContextApp _context;

        public VehicleSegmentsController(DbContextApp context)
        {
            _context = context;
        }

        // GET: api/VehicleSegments
        [HttpGet]
        public IEnumerable<VehicleSegment> GetVehicleSegments()
        {
            return _context.VehicleSegments.Where(x => x.Enabled == true);
        }

        // GET: api/VehicleSegments/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicleSegment([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vehicleSegment = await _context.VehicleSegments.FindAsync(id);

            if (vehicleSegment == null)
            {
                return NotFound();
            }

            return Ok(vehicleSegment);
        }


        // PUT: api/VehicleSegments/Create
        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CreateVehicleSegmentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            VehicleSegment obj = new VehicleSegment
            {
                Description = model.Description,
                Enabled = true
            };


            _context.VehicleSegments.Add(obj);
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

        // POST: api/VehicleSegments/Update
        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] UpdateVehicleSegmentViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.Id <= 0)
            {
                return BadRequest();
            }

            var obj = await _context.VehicleSegments.FirstOrDefaultAsync(x => x.Id == model.Id);

            obj.Description = model.Description;



            _context.Entry(obj).State = EntityState.Modified;

            if (obj == null)
            {
                return NotFound();
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //Guardar Exception
                return BadRequest();
            }

            return Ok();
        }


        // DELETE: api/VehicleSegments/Delete/5
        [HttpPut("[action]")]
        public async Task<IActionResult> Delete([FromBody] DeleteVehicleSegmentViewModel model)
        {


            if (model.Id <= 0)
            {
                return BadRequest();
            }

            var obj = await _context.VehicleSegments.FirstOrDefaultAsync(x => x.Id == model.Id);

            obj.Enabled = false;



            _context.Entry(obj).State = EntityState.Modified;

            if (obj == null)
            {
                return NotFound();
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //Guardar Exception
                return BadRequest();
            }

            return Ok();
        }
    }
}