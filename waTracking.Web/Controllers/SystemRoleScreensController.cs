using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Remotion.Linq.Parsing.Structure.IntermediateModel;
using waTracking.Data;
using waTracking.Entities.System;
using waTracking.Web.Models.System.RoleScreen;

namespace waTracking.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemRoleScreensController : ControllerBase
    {
        private readonly DbContextApp _context;

        public SystemRoleScreensController(DbContextApp context)
        {
            _context = context;
        }

        // GET: api/SystemRoleScreens
        [HttpGet]
        public IEnumerable<SystemRoleScreenViewModel> GetSystemRoleScreen()
        {
            List<SystemRole> listRole = _context.SystemRole.ToList();
            List<SystemScreen> listScreen = _context.SystemScreen.ToList();
            List<SystemRoleScreen> listRoleScreen = _context.SystemRoleScreen.ToList();
            List<SystemRoleScreenViewModel> list = new List<SystemRoleScreenViewModel>();

            foreach (var role in listRole)
            {
                foreach (var screen in listScreen)
                {

                        SystemRoleScreenViewModel model = new SystemRoleScreenViewModel
                        {
                            Enabled = listRoleScreen.Where(x=>x.SystemRoleId==role.Id && x.SystemScreenId==screen.Id).Any(),
                            SystemScreenId = screen.Id,
                            SystemRoleId = role.Id,
                            ParentId= screen.ParentId,
                            ScreenName= screen.Description
                        };
                        list.Add(model);
                    
                }

            }

            return list;
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] List<UpdateSystemRoleScreenViewModel> listmodel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            List<SystemRoleScreen> listsystemRoleScreens = _context.SystemRoleScreen.ToList();
            List<SystemRoleScreen> listBorrar = new List<SystemRoleScreen>();
            List<SystemRoleScreen> listGrabar = new List<SystemRoleScreen>();

            try
            {
                foreach (var item in listmodel)
                {
                    
                    SystemRoleScreen systemRoleScreen = listsystemRoleScreens.Where(x => x.SystemRoleId == item.SystemRoleId && x.SystemScreenId == item.SystemScreenId).FirstOrDefault();
                    //Si viene desabilitado, me fijo que no este dado de alta
                    if (!item.Enabled)
                    {
                        if (systemRoleScreen != null)
                            listBorrar.Add(systemRoleScreen);
                        //Sino no esta no hago nada
                    }
                    else
                    {
                        if (systemRoleScreen == null)
                        {
                            //Si no esta lo doy de alta
                            systemRoleScreen = new SystemRoleScreen();
                            systemRoleScreen.SystemRoleId = item.SystemRoleId;
                            systemRoleScreen.SystemScreenId = item.SystemScreenId;                            

                            listGrabar.Add(systemRoleScreen);
                        }
                            
                    }

                }

                //Agrego
                _context.SystemRoleScreen.AddRange(listGrabar);
                //Borro
                _context.SystemRoleScreen.RemoveRange(listBorrar);

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