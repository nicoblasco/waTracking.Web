using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using waTracking.Data;
using waTracking.Entities.System;
using waTracking.Web.Models.System.Action;

namespace waTracking.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemActionsController : ControllerBase
    {
        private readonly DbContextApp _context;

        public SystemActionsController(DbContextApp context)
        {
            _context = context;
        }

        // GET: api/SystemActions
        [HttpGet]
        public IEnumerable<SystemActionViewModel> GetSystemAction()
        {
            var systemScreen = _context.SystemAction.ToList();
            List<SystemActionViewModel> list = new List<SystemActionViewModel>();

            foreach (var item in systemScreen)
            {
                SystemActionViewModel vm = new SystemActionViewModel
                {
                    Id = item.Id,
                    IsNew = false,
                    IsRemoved = false,                    
                    Enabled = item.Enabled,
                    SystemScreenId = item.SystemScreenId,
                    Code = item.Code,
                    Description=item.Description


                };

                list.Add(vm);
            }

            return list;
        }


        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] List<UpdateSystemActionViewModel> listmodel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<SystemAction> screensUpdate = new List<SystemAction>();
            List<SystemAction> screensAdd = new List<SystemAction>();
            List<SystemAction> screensRemove = new List<SystemAction>();

            //Actualiza masivamente
            foreach (var item in listmodel)
            {
                if (item.Id == null && item.IsNew == true)
                {
                    SystemAction systemScreen = new SystemAction
                    {
                        Enabled = item.Enabled,
                        SystemScreenId = item.SystemScreenId,
                        Code = item.Code,
                        Description = item.Description
                    };
                    screensAdd.Add(systemScreen);
                }
                else
                {
                    //Si no es a borrar, el modelo
                    SystemAction systemScreen = _context.SystemAction.Find(item.Id);
                    //Ahora me fijo si lo elimino o lo actualizo
                    if (item.IsRemoved)
                        screensRemove.Add(systemScreen);
                    else
                    {
                        
                        systemScreen.Enabled = item.Enabled;
                        systemScreen.Code = item.Code;
                        systemScreen.Description = item.Description;
                        systemScreen.SystemScreenId = item.SystemScreenId;
                        
                        screensUpdate.Add(systemScreen);
                    }

                }
            }

            //Agrego
            _context.SystemAction.AddRange(screensAdd);
            //Actualizo
            _context.SystemAction.UpdateRange(screensUpdate);
            //Borro
            _context.SystemAction.RemoveRange(screensRemove);


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