using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using waTracking.Data;
using waTracking.Entities.Configuration;
using waTracking.Web.Models.Configuration.ScreenField;

namespace waTracking.Web.Controllers
{
    [EnableCors("SiteCorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigScreensController : ControllerBase
    {
        private readonly DbContextApp _context;

        public ConfigScreensController(DbContextApp context)
        {
            _context = context;
        }

        // GET: api/ConfigScreens
        [HttpGet]
        public IEnumerable<ConfigScreen> GetConfigScreens()
        {
            return _context.ConfigScreens;
        }

        // GET: api/ConfigScreens/GetConfigScreenByCompany/5
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetConfigScreenByCompany([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            List<IndexScreenFieldViewModel> listIndex = new List<IndexScreenFieldViewModel>();
            

            var configScreen = await _context.ConfigScreens.Where(x => x.CompanyId == id && x.Enabled ==true).Include(x=>x.ConfigScreenFields).Include(x=>x.SystemScreen).Select(x=> new { x.Id, x.Description, x.SystemScreen.Entity,  x.ConfigScreenFields }).ToListAsync();
           
            foreach (var item in configScreen)
            {
                IndexScreenFieldViewModel index = new IndexScreenFieldViewModel();
                index.Id = item.Id;
                index.Name = item.Description;
                index.Entity = item.Entity;
                index.CompanyId = id;
                index.ConfigScreenFields = new List<ConfigScreenField>();

                foreach (var field in item.ConfigScreenFields)
                {
                    ConfigScreenField configScreenField = new ConfigScreenField
                    {
                        ConfigScreenId= field.ConfigScreenId,
                        DefaultValue = field.DefaultValue,
                        FieldName = field.FieldName,
                        Id=field.Id,
                        Name = field.Name,
                        Required= field.Required,
                        Visible=field.Visible
                    };
                    index.ConfigScreenFields.Add(field);
                }
                listIndex.Add(index);
            }

            if (listIndex == null)
            {
                return NotFound();
            }

            return Ok(listIndex);
        }


        // GET: api/ConfigScreens/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetConfigScreen([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var configScreen = await _context.ConfigScreens.FindAsync(id);

            if (configScreen == null)
            {
                return NotFound();
            }

            return Ok(configScreen);
        }

        // PUT: api/ConfigScreens/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConfigScreen([FromRoute] int id, [FromBody] ConfigScreen configScreen)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != configScreen.Id)
            {
                return BadRequest();
            }

            _context.Entry(configScreen).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConfigScreenExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ConfigScreens
        [HttpPost]
        public async Task<IActionResult> PostConfigScreen([FromBody] ConfigScreen configScreen)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ConfigScreens.Add(configScreen);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConfigScreen", new { id = configScreen.Id }, configScreen);
        }

        // DELETE: api/ConfigScreens/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConfigScreen([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var configScreen = await _context.ConfigScreens.FindAsync(id);
            if (configScreen == null)
            {
                return NotFound();
            }

            _context.ConfigScreens.Remove(configScreen);
            await _context.SaveChangesAsync();

            return Ok(configScreen);
        }

        private bool ConfigScreenExists(int id)
        {
            return _context.ConfigScreens.Any(e => e.Id == id);
        }
    }
}