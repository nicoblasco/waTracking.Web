using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using waTracking.Data;
using waTracking.Entities.Department;
using waTracking.Web.Models.Deparment;

namespace waTracking.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentChildsController : ControllerBase
    {
        private readonly DbContextApp _context;

        public DepartmentChildsController(DbContextApp context)
        {
            _context = context;
        }

        // GET: api/DepartmentChilds
        [HttpGet]
        public IEnumerable<DepartmentChild> GetDepartmentChilds()
        {
            return _context.DepartmentChilds.Where(x => x.Enabled == true);
        }

        // GET: api/DepartmentChilds/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartmentChild([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var departmentChild = await _context.DepartmentChilds.FindAsync(id);

            if (departmentChild == null)
            {
                return NotFound();
            }

            return Ok(departmentChild);
        }
       
        
        
        // PUT: api/DepartmentChilds/Create
        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CreateDepartmentChildViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DepartmentChild obj = new DepartmentChild
            {
                Description = model.Description,
                Enabled = true
            };


            _context.DepartmentChilds.Add(obj);
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

        // POST: api/DepartmentChilds/Update
        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] UpdateDepartmentChildViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.Id <= 0)
            {
                return BadRequest();
            }

            var obj = await _context.DepartmentChilds.FirstOrDefaultAsync(x => x.Id == model.Id);

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


        // DELETE: api/DepartmentChilds/Delete/5
        [HttpPut("[action]")]
        public async Task<IActionResult> Delete([FromBody] DeleteDepartmentChildViewModel model)
        {


            if (model.Id <= 0)
            {
                return BadRequest();
            }

            var obj = await _context.DepartmentChilds.FirstOrDefaultAsync(x => x.Id == model.Id);

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