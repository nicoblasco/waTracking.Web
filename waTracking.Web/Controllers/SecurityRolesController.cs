using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using waTracking.Data;
using waTracking.Entities.Security;
using waTracking.Web.Models.Security.Role;

namespace waTracking.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityRolesController : ControllerBase
    {
        private readonly DbContextApp _context;

        public SecurityRolesController(DbContextApp context)
        {
            _context = context;
        }

        // GET: api/SecurityRoles/GetRoles
        [HttpGet("[action]")]
        public async Task<IEnumerable<RolViewModel>> GetRoles()
        {
            var rol = await _context.SecurityRoles.ToListAsync();

            return rol.Select(x => new RolViewModel
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Descripcion = x.Descripcion,
                Condicion = x.Condicion

            });
        }


        // GET: api/Roles/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<GetRolViewModel>> Select()
        {
            var rol = await _context.SecurityRoles.Where(x => x.Condicion == true).ToListAsync();

            return rol.Select(x => new GetRolViewModel
            {
                Id = x.Id,
                Nombre = x.Nombre
            });
        }


        private bool SecurityRoleExists(int id)
        {
            return _context.SecurityRoles.Any(e => e.Id == id);
        }
    }
}