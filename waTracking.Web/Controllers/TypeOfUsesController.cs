using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using waTracking.Data;
using waTracking.Entities.Vehicle;
using waTracking.Web.Models.Vehicle.TypeOfUse;

namespace waTracking.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeOfUsesController : ControllerBase
    {
        private readonly DbContextApp _context;

        public TypeOfUsesController(DbContextApp context)
        {
            _context = context;
        }

        // GET: api/TypeOfUses
        [HttpGet]
        public IEnumerable<TypeOfUse> GetTypeOfUses()
        {
            return _context.TypeOfUses.Where(x => x.Enabled == true);
        }

        // GET: api/TypeOfUses/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTypeOfUse([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var typeOfUse = await _context.TypeOfUses.FindAsync(id);

            if (typeOfUse == null)
            {
                return NotFound();
            }

            return Ok(typeOfUse);
        }


        // PUT: api/TypeOfUses/Create
        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CreateTypeOfUseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TypeOfUse obj = new TypeOfUse
            {
                Description = model.Description,
                Enabled = true
            };


            _context.TypeOfUses.Add(obj);
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

        // POST: api/TypeOfUses/Update
        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] UpdateTypeOfUseViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.Id <= 0)
            {
                return BadRequest();
            }

            var obj = await _context.TypeOfUses.FirstOrDefaultAsync(x => x.Id == model.Id);

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


        // DELETE: api/TypeOfUses/Delete/5
        [HttpPut("[action]")]
        public async Task<IActionResult> Delete([FromBody] DeleteTypeOfUseViewModel model)
        {


            if (model.Id <= 0)
            {
                return BadRequest();
            }

            var obj = await _context.TypeOfUses.FirstOrDefaultAsync(x => x.Id == model.Id);

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