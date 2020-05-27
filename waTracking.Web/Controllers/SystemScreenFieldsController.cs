using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using waTracking.Data;
using waTracking.Entities.System;
using waTracking.Web.Models.System.Field;

namespace waTracking.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemScreenFieldsController : ControllerBase
    {
        private readonly DbContextApp _context;

        public SystemScreenFieldsController(DbContextApp context)
        {
            _context = context;
        }

        // GET: api/SystemScreenFields
        [HttpGet]
        public IEnumerable<SystemScreenFieldViewModel> GetSystemScreenField()
        {
            var systemScreen = _context.SystemScreenField.ToList();
            List<SystemScreenFieldViewModel> list = new List<SystemScreenFieldViewModel>();

            foreach (var item in systemScreen)
            {
                SystemScreenFieldViewModel vm = new SystemScreenFieldViewModel
                {
                    Id = item.Id,
                    IsNew = false,
                    IsRemoved = false,
                    DefaultValue= item.DefaultValue,
                    Enabled=item.Enabled,
                    FieldName=item.FieldName,
                    Name=item.Name,
                    Required=item.Required,
                    SystemScreenId=item.SystemScreenId,
                    Visible = item.Visible


                };

                list.Add(vm);
            }

            return list;
        }


        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] List<UpdateSystemScreenFieldViewModel> listmodel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<SystemScreenField> screensUpdate = new List<SystemScreenField>();
            List<SystemScreenField> screensAdd = new List<SystemScreenField>();
            List<SystemScreenField> screensRemove = new List<SystemScreenField>();

            //Actualiza masivamente
            foreach (var item in listmodel)
            {
                if (item.Id == null && item.IsNew == true)
                {
                    SystemScreenField systemScreen = new SystemScreenField
                    {
                        DefaultValue = item.DefaultValue,
                        Enabled = item.Enabled,
                        FieldName = item.FieldName,
                        Name = item.Name,
                        Required = item.Required,
                        SystemScreenId = item.SystemScreenId,
                        Visible = item.Visible
                    };
                    screensAdd.Add(systemScreen);
                }
                else
                {
                    //Si no es a borrar, el modelo
                    SystemScreenField systemScreen = _context.SystemScreenField.Find(item.Id);
                    //Ahora me fijo si lo elimino o lo actualizo
                    if (item.IsRemoved)
                        screensRemove.Add(systemScreen);
                    else
                    {
                        systemScreen.DefaultValue = item.DefaultValue;
                        systemScreen.Enabled = item.Enabled;
                        systemScreen.FieldName = item.FieldName;
                        systemScreen.Name = item.Name;
                        systemScreen.Required = item.Required;
                        systemScreen.SystemScreenId = item.SystemScreenId;
                        systemScreen.Visible = item.Visible;
                        screensUpdate.Add(systemScreen);
                    }

                }
            }

            //Agrego
            _context.SystemScreenField.AddRange(screensAdd);
            //Actualizo
            _context.SystemScreenField.UpdateRange(screensUpdate);
            //Borro
            _context.SystemScreenField.RemoveRange(screensRemove);


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