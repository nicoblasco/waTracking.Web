using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using waTracking.Data;
using waTracking.Entities.Vehicle;
using waTracking.Web.Models.Vehicle.Brand;

namespace waTracking.Web.Controllers
{
    [EnableCors("SiteCorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleBrandsController : ControllerBase
    {
        private readonly DbContextApp _context;

        public VehicleBrandsController(DbContextApp context)
        {
            _context = context;
        }

        // GET: api/VehicleBrands
        [HttpGet]
        public IEnumerable<VehicleBrand> GetVehicleBrands()
        {
            return _context.VehicleBrands.Where(x => x.Enabled == true);
        }

        // GET: api/VehicleBrands/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicleBrand([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vehicleBrand = await _context.VehicleBrands.FindAsync(id);

            if (vehicleBrand == null)
            {
                return NotFound();
            }

            return Ok(vehicleBrand);
        }

        // POST: api/VehicleBrands/Create
        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CreateVehicleBrandViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            VehicleBrand vehicleBrand = new VehicleBrand { 
             Description = model.Description,
             Enabled = true
            };


            _context.VehicleBrands.Add(vehicleBrand);
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


        // PUT: api/VehicleBrands/Update/5
        [EnableCors("SiteCorsPolicy")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] UpdateVehicleBrandViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.Id <= 0)
            {
                return BadRequest();
            }

            var brand = await _context.VehicleBrands.FirstOrDefaultAsync(x => x.Id == model.Id);

            brand.Description = model.Description;



            _context.Entry(brand).State = EntityState.Modified;

            if (brand == null)
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


        // PUT: api/VehicleBrands/Delete/5
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Delete ([FromRoute] int Id)
        {


            if (Id <= 0)
            {
                return BadRequest();
            }

            var brand = await _context.VehicleBrands.FirstOrDefaultAsync(x => x.Id == Id);

            brand.Enabled =false;



            _context.Entry(brand).State = EntityState.Modified;

            if (brand == null)
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

        private bool VehicleBrandExists(int id)
        {
            return _context.VehicleBrands.Any(e => e.Id == id);
        }
    }
}