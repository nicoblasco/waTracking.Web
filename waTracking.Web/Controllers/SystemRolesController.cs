using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using waTracking.Data;
using waTracking.Entities.System;
using waTracking.Web.Models.System.Role;

namespace waTracking.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemRolesController : ControllerBase
    {
        private readonly DbContextApp _context;

        public SystemRolesController(DbContextApp context)
        {
            _context = context;
        }

        // GET: api/SystemRoles
        [HttpGet]
        public IEnumerable<SystemRoleViewModel > GetSystemRole()
        {
            var systemScreen = _context.SystemRole.ToList();
            List<SystemRoleViewModel> list = new List<SystemRoleViewModel>();

            foreach (var item in systemScreen)
            {
                SystemRoleViewModel vm = new SystemRoleViewModel
                {
                    Id = item.Id,
                    IsNew = false,
                    IsRemoved = false,
                    Description = item.Description,
                    Enabled = item.Enabled,
                    Name= item.Name



                };

                list.Add(vm);
            }

            return list;
        }


        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] List<UpdateSystemRoleViewModel> listmodel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<SystemRole> screensUpdate = new List<SystemRole>();
            List<SystemRole> screensAdd = new List<SystemRole>();
            List<SystemRole> screensRemove = new List<SystemRole>();

            //Actualiza masivamente
            foreach (var item in listmodel)
            {
                if (item.Id == null && item.IsNew == true)
                {
                    SystemRole systemScreen = new SystemRole
                    {
                        Description = item.Description,
                        Enabled = item.Enabled,
                        Name = item.Name
                        

                    };
                    screensAdd.Add(systemScreen);
                }
                else
                {
                    //Si no es a borrar, el modelo
                    SystemRole systemScreen = _context.SystemRole.Find(item.Id);
                    //Ahora me fijo si lo elimino o lo actualizo
                    if (item.IsRemoved)
                        screensRemove.Add(systemScreen);
                    else
                    {
                        systemScreen.Name = item.Name;
                        systemScreen.Description = item.Description;
                        systemScreen.Enabled = item.Enabled;
                        screensUpdate.Add(systemScreen);
                    }

                }
            }

            //Agrego
            _context.SystemRole.AddRange(screensAdd);
            //Actualizo
            _context.SystemRole.UpdateRange(screensUpdate);
            //Borro
            _context.SystemRole.RemoveRange(screensRemove);


            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                return BadRequest();
            }

            return Ok();
        }

    }
}