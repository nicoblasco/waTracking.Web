using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using waTracking.Data;
using waTracking.Entities.Security;
using waTracking.Web.Models.Security.RoleAction;

namespace waTracking.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityRoleActionsController : ControllerBase
    {
        private readonly DbContextApp _context;

        public SecurityRoleActionsController(DbContextApp context)
        {
            _context = context;
        }

        // GET: api/SecurityRoleActions
        [HttpGet]
        public IEnumerable<SecurityRoleAction> GetSecurityRoleActions()
        {
            return _context.SecurityRoleActions;
        }


        [HttpGet("[action]/{roleId}/{screenId}")]
        public async Task<IActionResult> GetConfigScreenByCompanyByScreen([FromRoute] int roleId, int screenId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            List<RoleActionViewModel> listIndex = new List<RoleActionViewModel>();


            var configScreen = await _context.SecurityRoleActions.Where(x => x.SecurityRoleId == roleId).Include(x => x.SecurityAction ).ThenInclude(x=>x.SystemAction).Select(x => new { x.Id, x.SecurityActionId, x.SecurityAction.SystemAction.Code, x.SecurityRoleId, x.SecurityAction.ConfigScreenId }).ToListAsync();
            
            foreach (var item in configScreen.Where(x=>x.ConfigScreenId== screenId) )
            {
                RoleActionViewModel viewModel = new RoleActionViewModel();
                viewModel.Id = item.Id;
                viewModel.ActionId = item.SecurityActionId;
                viewModel.ActionCode = item.Code;
                viewModel.SecurityRoleId = item.SecurityRoleId;

                listIndex.Add(viewModel);
            };



            if (listIndex == null)
            {
                return NotFound();
            }

            return Ok(listIndex);
        }

        // GET: api/SecurityRoleActions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSecurityRoleAction([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var securityRoleAction = await _context.SecurityRoleActions.FindAsync(id);

            if (securityRoleAction == null)
            {
                return NotFound();
            }

            return Ok(securityRoleAction);
        }

        // PUT: api/SecurityRoleActions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSecurityRoleAction([FromRoute] int id, [FromBody] SecurityRoleAction securityRoleAction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != securityRoleAction.Id)
            {
                return BadRequest();
            }

            _context.Entry(securityRoleAction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SecurityRoleActionExists(id))
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

        // POST: api/SecurityRoleActions
        [HttpPost]
        public async Task<IActionResult> PostSecurityRoleAction([FromBody] SecurityRoleAction securityRoleAction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.SecurityRoleActions.Add(securityRoleAction);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSecurityRoleAction", new { id = securityRoleAction.Id }, securityRoleAction);
        }

        // DELETE: api/SecurityRoleActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSecurityRoleAction([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var securityRoleAction = await _context.SecurityRoleActions.FindAsync(id);
            if (securityRoleAction == null)
            {
                return NotFound();
            }

            _context.SecurityRoleActions.Remove(securityRoleAction);
            await _context.SaveChangesAsync();

            return Ok(securityRoleAction);
        }

        private bool SecurityRoleActionExists(int id)
        {
            return _context.SecurityRoleActions.Any(e => e.Id == id);
        }
    }
}