using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using waTracking.Data;
using waTracking.Entities.Vehicle;
using waTracking.Web.Models.Vehicle.Status;

namespace waTracking.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly DbContextApp _context;

        public StatusController(DbContextApp context)
        {
            _context = context;
        }

        // GET: api/Status
        [HttpGet]
        public IEnumerable<Status> GetStatus()
        {
            return _context.Status.Where(x => x.Enabled == true);
        }

        // GET: api/Status/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStatus([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var status = await _context.Status.FindAsync(id);

            if (status == null)
            {
                return NotFound();
            }

            return Ok(status);
        }

        // PUT: api/Status/Create
        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CreateStatusViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Status obj = new Status
            {
                Description = model.Description,
                Enabled = true
            };


            _context.Status.Add(obj);
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

        // POST: api/Status/Update
        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] UpdateStatusViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.Id <= 0)
            {
                return BadRequest();
            }

            var obj = await _context.Status.FirstOrDefaultAsync(x => x.Id == model.Id);

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


        // DELETE: api/Status/Delete/5
        [HttpPut("[action]")]
        public async Task<IActionResult> Delete([FromBody] DeleteStatusViewModel model)
        {


            if (model.Id <= 0)
            {
                return BadRequest();
            }

            var obj = await _context.Status.FirstOrDefaultAsync(x => x.Id == model.Id);

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