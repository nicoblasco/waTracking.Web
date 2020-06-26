using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using waTracking.Data;
using waTracking.Entities.Security;

namespace waTracking.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityRoleScreensController : ControllerBase
    {
        private readonly DbContextApp _context;

        public SecurityRoleScreensController(DbContextApp context)
        {
            _context = context;
        }


        // GET: api/SecurityRoleScreens/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSecurityRoleScreen([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var securityRoleScreen = await _context.SecurityRoleScreens.FindAsync(id);

            if (securityRoleScreen == null)
            {
                return NotFound();
            }

            return Ok(securityRoleScreen);
        }


    }
}