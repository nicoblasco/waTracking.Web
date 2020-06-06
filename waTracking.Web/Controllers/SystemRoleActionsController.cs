using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using waTracking.Data;
using waTracking.Entities.System;
using waTracking.Web.Models.System.RoleAction;

namespace waTracking.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemRoleActionsController : ControllerBase
    {
        private readonly DbContextApp _context;

        public SystemRoleActionsController(DbContextApp context)
        {
            _context = context;
        }

        // GET: api/SystemRoleActions
        [HttpGet]        

        public IEnumerable<RoleActionViewModel> GetSystemRoleAction()
        {
            List<SystemAction> listactions = _context.SystemAction.Where(x=>x.Enabled==true).ToList();
            List<SystemRole> listroles = _context.SystemRole.ToList();
            List<SystemRoleAction> listroleaction = _context.SystemRoleAction.ToList();
            
            List<RoleActionViewModel> listvm = new List<RoleActionViewModel>();
            foreach (var rol in listroles)
            {
                    foreach (var action in listactions)
                    {
                        //Busco si tiene algun rol asociad
                        RoleActionViewModel model = new RoleActionViewModel
                        {

                            ActionCode = action.Code,
                            ActionDescription = action.Description,
                            SystemActionId = action.Id,
                            SystemRoleId = rol.Id,
                            SystemScreenId = action.SystemScreenId,
                            Enabled = listroleaction.Where(x => x.SystemActionId == action.Id && x.SystemRoleId == rol.Id).Any()
                        };
                        listvm.Add(model);
                    }
                

            }
            
            return listvm;
        }


        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] List<UpdateRoleActionViewModel> listmodel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            List<SystemRoleAction> listsystemRoleAction = _context.SystemRoleAction.ToList();
            List<SystemRoleAction> listBorrar = new List<SystemRoleAction>();
            List<SystemRoleAction> listGrabar = new List<SystemRoleAction>();

            try
            {
                foreach (var item in listmodel)
                {
                    
                    SystemRoleAction systemRoleAction = listsystemRoleAction.Where(x => x.SystemRoleId == item.SystemRoleId && x.SystemActionId == item.SystemActionId).FirstOrDefault();
                    //Si viene desabilitado, me fijo que no este dado de alta
                    if (!item.Enabled)
                    {
                        if (systemRoleAction != null)
                            listBorrar.Add(systemRoleAction);
                        //Sino no esta no hago nada
                    }
                    else
                    {
                        if (systemRoleAction == null)
                        {
                            systemRoleAction = new SystemRoleAction();
                            //Si no esta lo doy de alta
                            systemRoleAction.SystemRoleId = item.SystemRoleId;
                            systemRoleAction.SystemActionId = item.SystemActionId;

                            listGrabar.Add(systemRoleAction);
                        }

                    }

                }

                //Agrego
                _context.SystemRoleAction.AddRange(listGrabar);
                //Borro
                _context.SystemRoleAction.RemoveRange(listBorrar);

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