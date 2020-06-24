using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using waTracking.Data;
using waTracking.Entities.Security;
using waTracking.Entities.System;
using waTracking.Web.Models.Security.Role;
using waTracking.Web.Models.Security.RoleAction;
using waTracking.Web.Models.Security.RoleScreen;

namespace waTracking.Web.Controllers
{
    [EnableCors("SiteCorsPolicy")]
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
                Name = x.Name,
                Description = x.Description,
                Enabled = x.Enabled

            });
        }

        // GET: api/SecurityRoles/GetByCompany/5
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetByCompany([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            List<SecurityRolViewModel> listIndex = new List<SecurityRolViewModel>();

            //Busco en el maestro de Pantallas (SystemScreen), y traigo todas las pantallas
            //Las pantallas que estan en ConfigScreen las sobreescribo

            List<SystemRole> listSystemRole = await _context.SystemRole.ToListAsync();
            List<SecurityRole> listSecurityRole = await _context.SecurityRoles.Include(x=>x.SecurityRoleScreens).ThenInclude(x=>x.ConfigScreen).Include(x=>x.SecurityRoleActions).ThenInclude(x=>x.SecurityAction).Where(x => x.CompanyId == id).ToListAsync();

            foreach (var item in listSystemRole)
            {
                var securityRole = listSecurityRole.Where(x => x.SystemRoleId == item.Id).FirstOrDefault();
                SecurityRolViewModel model;

                //Si la pantalla ya esta dada de alta
                if (securityRole != null)
                {
                    model = new SecurityRolViewModel
                    {
                        Id = item.Id,
                        Description = securityRole.Description,
                        Enabled = securityRole.Enabled,
                        Name = securityRole.Name,
                        IsNew = false,
                        IsRemoved = false,
                        SecurityRoleActions = new List<SecurityRoleActionViewModel>(),
                        SecurityRoleScreens = new List<SecurityRoleScreenViewModel>()

                    };

                    foreach (var screen in securityRole.SecurityRoleScreens)
                    {
                        SecurityRoleScreenViewModel securityRoleScreenViewModel = new SecurityRoleScreenViewModel
                        {
                            SystemRoleId=screen.SecurityRole.SystemRoleId,
                            SystemScreenId= screen.ConfigScreen.SystemScreenId
                        };
                        model.SecurityRoleScreens.Add(securityRoleScreenViewModel);
                    }

                    foreach (var action in securityRole.SecurityRoleActions)
                    {
                        SecurityRoleActionViewModel securityRoleActionViewModel = new SecurityRoleActionViewModel
                        {
                            SystemRoleId =action.SecurityRole.SystemRoleId,
                            SystemActionId = action.SecurityAction.SystemActionId
                        };
                        model.SecurityRoleActions.Add(securityRoleActionViewModel);
                    }

                }
                else
                {
                    model = new SecurityRolViewModel
                    {
                        Id = item.Id,
                        Description = item.Description,
                        Enabled = item.Enabled,
                        Name = item.Name,
                        IsNew = false,
                        IsRemoved = false,
                        SecurityRoleActions = new List<SecurityRoleActionViewModel>(),
                        SecurityRoleScreens = new List<SecurityRoleScreenViewModel>()
                    };



                }



                listIndex.Add(model);

            }



            if (listIndex == null)
            {
                return NotFound();
            }

            return Ok(listIndex);
        }


        // GET: api/SecurityRoles/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<GetRolViewModel>> Select()
        {
            var rol = await _context.SecurityRoles.Where(x => x.Enabled == true).ToListAsync();

            return rol.Select(x => new GetRolViewModel
            {
                Id = x.Id,
                Name = x.Name
            });
        }


        private bool SecurityRoleExists(int id)
        {
            return _context.SecurityRoles.Any(e => e.Id == id);
        }
    }
}