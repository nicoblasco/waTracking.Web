using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using waTracking.Data;
using waTracking.Entities.System;
using waTracking.Web.Models.System.Screen;

namespace waTracking.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemScreensController : ControllerBase
    {
        private readonly DbContextApp _context;

        public SystemScreensController(DbContextApp context)
        {
            _context = context;
        }

        // GET: api/SystemScreens
        [HttpGet]
        public IEnumerable<SystemScreenViewModel> GetSystemScreen()
        {
            // return _context.SystemScreen;
            var systemScreen =  _context.SystemScreen.ToList();
            List<SystemScreenViewModel> list = new List<SystemScreenViewModel>();

            foreach (var item in systemScreen)
            {
                SystemScreenViewModel vm = new SystemScreenViewModel
                {
                    Id= item.Id,
                    Description = item.Description,
                    Enabled = item.Enabled,
                    Entity = item.Entity,
                    Icon = item.Icon,
                    IsDefault = item.IsDefault,
                    IsNew = false,
                    IsRemoved= false,
                    Orden = item.Orden,
                    ParentId = item.ParentId,
                    Path= item.Path

                };

                list.Add(vm);
            }

            return list;

        }

        // GET: api/SystemScreens/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSystemScreen([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var systemScreen = await _context.SystemScreen.FindAsync(id);

            if (systemScreen == null)
            {
                return NotFound();
            }

            return Ok(systemScreen);
        }



        // GET: api/SystemScreens/GetMenues/
        [HttpGet("[action]")]
        public IEnumerable<SystemScreen> GetMenues()
        {
            return _context.SystemScreen.Where(x => x.ParentId == null).OrderByDescending(x => x.Orden).ToList();

        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CreateSystemScreenViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SystemScreen systemScreen = new SystemScreen
            {
                Description= model.Description,
                Enabled = model.Enabled,
                Entity= model.Entity,
                Icon= model.Icon,
                IsDefault= model.IsDefault,
                Orden = model.Orden,
                ParentId=model.ParentId,
                Path= model.Path
                
            };


            _context.SystemScreen.Add(systemScreen);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch 
            {

                return BadRequest();
            }

            return Ok(systemScreen.Id);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] List<UpdateSystemScreenViewModel> listmodel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<SystemScreen> screensUpdate = new List<SystemScreen>();
            List<SystemScreen> screensAdd = new List<SystemScreen>();
            List<SystemScreen> screensRemove = new List<SystemScreen>();

            //Actualiza masivamente
            foreach (var item in listmodel)
            {
                if (item.Id == null && item.IsNew == true)
                {
                    SystemScreen systemScreen = new SystemScreen
                    {
                        Description = item.Description,
                        Enabled = item.Enabled,
                        Entity = item.Entity,
                        Icon = item.Icon,
                        IsDefault = item.IsDefault,
                        Orden = item.Orden ?? 99,
                        ParentId = item.ParentId,
                        Path = item.Path
                    };
                    screensAdd.Add(systemScreen);
                }
                else
                {
                    //Si no es a borrar, el modelo
                    //SystemScreen systemScreen = _context.SystemScreen.Find(item.Id);


                    //Ahora me fijo si lo elimino o lo actualizo
                    if (item.IsRemoved)
                    {
                        SystemScreen systemScreen = _context.SystemScreen.Where(x => x.Id == item.Id).Include(x => x.SystemScreenFields).Include(x => x.SystemActions).FirstOrDefault();
                        screensRemove.Add(systemScreen);
                    }
                        
                    else
                    {

                        SystemScreen systemScreen = _context.SystemScreen.Find(item.Id);//Es mas rapido
                        systemScreen.Description = item.Description;
                        systemScreen.Enabled = item.Enabled;
                        systemScreen.Entity = item.Entity;
                        systemScreen.Icon = item.Icon;
                        systemScreen.IsDefault = item.IsDefault;
                        systemScreen.Orden = item.Orden ?? 99;
                        systemScreen.ParentId = item.ParentId;
                        systemScreen.Path = item.Path;
                        screensUpdate.Add(systemScreen);
                    }

                }
            }

           //Agrego
            _context.SystemScreen.AddRange(screensAdd);
            //Actualizo
            _context.SystemScreen.UpdateRange(screensUpdate);
            //Borro
            //Primero borro las dependencias
            foreach (var item in screensRemove)
            {
                if (item.SystemScreenFields!=null)
                _context.SystemScreenField.RemoveRange(item.SystemScreenFields);
                if (item.SystemActions != null)
                    _context.SystemAction.RemoveRange(item.SystemActions);
            }
            _context.SystemScreen.RemoveRange(screensRemove);


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