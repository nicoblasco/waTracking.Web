using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using waTracking.Data;
using waTracking.Entities.Vehicle;
using waTracking.Web.Models.Vehicle.Type;

namespace waTracking.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleTypesController : ControllerBase
    {
        private readonly DbContextApp _context;

        public VehicleTypesController(DbContextApp context)
        {
            _context = context;
        }

        // GET: api/VehicleTypes
        [HttpGet]
        public IEnumerable<VehicleType> GetVehicleTypes()
        {
            return _context.VehicleTypes.Where(x => x.Enabled == true);
        }

        // GET: api/VehicleTypes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicleType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vehicleType = await _context.VehicleTypes.FindAsync(id);

            if (vehicleType == null)
            {
                return NotFound();
            }

            return Ok(vehicleType);
        }


        // PUT: api/VehicleTypes/Create
        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CreateVehicleTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            VehicleType obj = new VehicleType
            {
                Description = model.Description,
                Enabled = true
            };


            _context.VehicleTypes.Add(obj);
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

        // POST: api/VehicleTypes/Update
        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] UpdateVehicleTypeViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.Id <= 0)
            {
                return BadRequest();
            }

            var obj = await _context.VehicleTypes.FirstOrDefaultAsync(x => x.Id == model.Id);

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


        // DELETE: api/VehicleTypes/Delete/5
        [HttpPut("[action]")]
        public async Task<IActionResult> Delete([FromBody] DeleteVehicleTypeViewModel model)
        {


            if (model.Id <= 0)
            {
                return BadRequest();
            }

            var obj = await _context.VehicleTypes.FirstOrDefaultAsync(x => x.Id == model.Id);

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