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
    public class DepartmentsController : ControllerBase
    {
        private readonly DbContextApp _context;

        public DepartmentsController(DbContextApp context)
        {
            _context = context;
        }

        // GET: api/Departments
        [HttpGet]
        public IEnumerable<Department> GetDepartments()
        {
            return _context.Departments.Where(x => x.Enabled == true);
        }

        // GET: api/Departments/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartment([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var department = await _context.Departments.FindAsync(id);

            if (department == null)
            {
                return NotFound();
            }

            return Ok(department);
        }

        // PUT: api/Departments/Create
        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CreateDepartmentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Department obj = new Department
            {
                Description = model.Description,
                Enabled = true
            };


            _context.Departments.Add(obj);
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

        // POST: api/Departments/Update
        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] UpdateDepartmentViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.Id <= 0)
            {
                return BadRequest();
            }

            var obj = await _context.Departments.FirstOrDefaultAsync(x => x.Id == model.Id);

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


        // PUT: api/Departments/Delete/5
        [HttpPut("[action]")]
        public async Task<IActionResult> Delete([FromBody] DeleteDepartmentViewModel model)
        {


            if (model.Id <= 0)
            {
                return BadRequest();
            }

            var obj = await _context.Departments.FirstOrDefaultAsync(x => x.Id == model.Id);

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