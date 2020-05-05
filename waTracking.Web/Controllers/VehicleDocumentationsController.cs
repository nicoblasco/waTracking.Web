using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using waTracking.Data;
using waTracking.Entities.Vehicle;
using waTracking.Web.Models.Vehicle.Documentation;

namespace waTracking.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleDocumentationsController : ControllerBase
    {
        private readonly DbContextApp _context;

        public VehicleDocumentationsController(DbContextApp context)
        {
            _context = context;
        }

        // GET: api/VehicleDocumentations
        [HttpGet]
        public IEnumerable<VehicleDocumentation> GetVehicleDocumentations()
        {
            return _context.VehicleDocumentations.Where(x => x.Enabled == true);
        }

        // GET: api/VehicleDocumentations/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicleDocumentation([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vehicleDocumentation = await _context.VehicleDocumentations.FindAsync(id);

            if (vehicleDocumentation == null)
            {
                return NotFound();
            }

            return Ok(vehicleDocumentation);
        }

        // PUT: api/VehicleDocumentations/Create
        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CreateVehicleDocumentationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            VehicleDocumentation obj = new VehicleDocumentation
            {
                Description = model.Description,
                Enabled = true
            };


            _context.VehicleDocumentations.Add(obj);
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

        // POST: api/VehicleDocumentations/Update
        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] UpdateVehicleDocumentationViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.Id <= 0)
            {
                return BadRequest();
            }

            var obj = await _context.VehicleDocumentations.FirstOrDefaultAsync(x => x.Id == model.Id);

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


        // DELETE: api/VehicleDocumentations/Delete/5
        [HttpPut("[action]")]
        public async Task<IActionResult> Delete([FromBody] DeleteVehicleDocumentationViewModel model)
        {


            if (model.Id <= 0)
            {
                return BadRequest();
            }

            var obj = await _context.VehicleDocumentations.FirstOrDefaultAsync(x => x.Id == model.Id);

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