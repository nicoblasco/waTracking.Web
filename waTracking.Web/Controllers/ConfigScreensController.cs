using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using waTracking.Data;
using waTracking.Entities.Configuration;
using waTracking.Entities.System;
using waTracking.Web.Models.Configuration.Screen;
using waTracking.Web.Models.Configuration.ScreenField;
using waTracking.Web.Models.Security.Action;

namespace waTracking.Web.Controllers
{
    [EnableCors("SiteCorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigScreensController : ControllerBase
    {
        private readonly DbContextApp _context;

        public ConfigScreensController(DbContextApp context)
        {
            _context = context;
        }

        // GET: api/ConfigScreens
        [HttpGet]
        public IEnumerable<ConfigScreen> GetConfigScreens()
        {
            return _context.ConfigScreens;
        }

        // GET: api/ConfigScreens/GetConfigScreenByCompany/5
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetConfigScreenByCompany([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            List<IndexScreenFieldViewModel> listIndex = new List<IndexScreenFieldViewModel>();
            

            var configScreen = await _context.ConfigScreens.Where(x => x.CompanyId == id && x.Enabled ==true).Include(x=>x.ConfigScreenFields).Include(x=>x.SystemScreen).Select(x=> new { x.Id, x.Description, x.SystemScreen.Entity,  x.ConfigScreenFields }).ToListAsync();
           
            foreach (var item in configScreen)
            {
                IndexScreenFieldViewModel index = new IndexScreenFieldViewModel();
                index.Id = item.Id;
                index.Name = item.Description;
                index.Entity = item.Entity;
                index.CompanyId = id;
                index.ConfigScreenFields = new List<ConfigScreenField>();

                foreach (var field in item.ConfigScreenFields)
                {
                    ConfigScreenField configScreenField = new ConfigScreenField
                    {
                        ConfigScreenId= field.ConfigScreenId,
                        DefaultValue = field.DefaultValue,
                        FieldName = field.FieldName,
                        Id=field.Id,
                        Name = field.Name,
                        Required= field.Required,
                        Visible=field.Visible
                    };
                    index.ConfigScreenFields.Add(field);
                }
                listIndex.Add(index);
            }

            if (listIndex == null)
            {
                return NotFound();
            }

            return Ok(listIndex);
        }



        // GET: api/ConfigScreens/GetConfigScreenByCompany/5
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetConfigSystemScreenByCompany([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            List<ConfigScreenViewModel> listIndex = new List<ConfigScreenViewModel>();

            //Busco en el maestro de Pantallas (SystemScreen), y traigo todas las pantallas
            //Las pantallas que estan en ConfigScreen las sobreescribo

            List<SystemScreen>  listSystemScreen = await _context.SystemScreen.Include(x=>x.SystemActions).Include(x=>x.SystemScreenFields).ToListAsync();
            List<ConfigScreen>  listConfigScreens = await _context.ConfigScreens.Include(x=>x.SecurityActions).Include(x => x.ConfigScreenFields).Where(x => x.CompanyId == id).ToListAsync();

            foreach (var item in listSystemScreen)
            {
                var configScreen = listConfigScreens.Where(x => x.SystemScreenId == item.Id).FirstOrDefault();
                ConfigScreenViewModel model;

                //Si la pantalla ya esta dada de alta
                if (configScreen!=null)
                {
                    model = new ConfigScreenViewModel
                    {
                        Id = item.Id,
                        CompanyId = id,
                        Description = configScreen.Description,
                        Enabled = configScreen.Enabled,
                        Icon = configScreen.Icon,
                        Orden = configScreen.Orden,
                        SystemScreenId = configScreen.Id,
                        IsDefault = true,
                        IsNew = false,
                        IsRemoved = false,
                        ParentId = item.ParentId,
                        Path = item.Path,
                        ConfigScreenFields = new List<ConfigScreenFieldViewModel>(),
                        SecurityActions = new List<SecurityActionViewModel>()

                    };
                    
                }
                else
                {
                     model = new ConfigScreenViewModel
                    {
                        CompanyId = id,
                        Description = item.Description,
                        Id= item.Id,
                        Enabled = false,
                        Icon = item.Icon,
                        Orden = item.Orden,
                        SystemScreenId = item.Id,
                         IsDefault = true,
                         IsNew = false,
                         IsRemoved = false,
                         ParentId = item.ParentId,
                         Path = item.Path,
                         ConfigScreenFields = new List<ConfigScreenFieldViewModel>(),
                         SecurityActions = new List<SecurityActionViewModel>()

                     };


                    
                }
                //Por cada pantalla, me fijo si esa pantalla tiene atributos
                    //Si tiene atributos en el maestro o en la empresa
                if (item.SystemScreenFields!=null)
                {
                    //Si la pantalla tiene atributos en el maestro
                    foreach (var sysfield in item.SystemScreenFields)
                    {
                        ConfigScreenFieldViewModel configScreenFieldView;
                        //Me fijo si ese atributo esta en el de configuracion
                        var configScreenFields = configScreen?.ConfigScreenFields?.Where(x => x.SystemScreenFieldId == sysfield.Id).FirstOrDefault();
                        //Si ya esta cargado
                        if (configScreenFields != null)
                        {
                            configScreenFieldView = new ConfigScreenFieldViewModel
                            {
                                Id = sysfield.Id,
                                DefaultValue = configScreenFields.DefaultValue,
                                Enabled = configScreenFields.Enabled,
                                FieldName = configScreenFields.FieldName,
                                Name = configScreenFields.Name,
                                Required = sysfield.Required,
                                SystemScreenId = item.Id,
                                Visible = sysfield.Visible,
                                IsNew=false,
                                IsRemoved=false


                            };

                        }

                        else
                        {
                            configScreenFieldView = new ConfigScreenFieldViewModel
                            {
                                Id = sysfield.Id,
                                DefaultValue =  sysfield.DefaultValue,
                                Enabled = false,
                                FieldName = sysfield.FieldName,
                                Name = sysfield.Name,
                                Required = sysfield.Required,
                                SystemScreenId = item.Id,
                                Visible = sysfield.Visible,
                                IsNew = false,
                                IsRemoved = false

                            };
                        }
                        model.ConfigScreenFields.Add(configScreenFieldView);


                    }
                    
                }


                //Por cada pantalla, me fijo si esa pantalla tiene acciones
                //Si tiene acciones en el maestro o en la empresa
                if (item.SystemActions != null)
                {
                    //Si la pantalla tiene acciones en el maestro
                    foreach (var sysaction in item.SystemActions)
                    {
                        SecurityActionViewModel securityActionViewModel;

                        //Me fijo si esa accion esta en el de configuracion
                        var securityAction = configScreen?.SecurityActions?.Where(x => x.SystemActionId == sysaction.Id).FirstOrDefault();
                        //Si ya esta cargado
                        if (securityAction != null)
                        {
                            securityActionViewModel = new SecurityActionViewModel
                            {
                                Id = sysaction.Id,
                                Code = sysaction.Code,
                                Description= securityAction.Description,
                                Enabled= securityAction.Enabled,
                                SystemScreenId= sysaction.SystemScreenId,                                
                                IsNew = false,
                                IsRemoved = false


                            };

                        }

                        else
                        {
                            securityActionViewModel = new SecurityActionViewModel
                            {
                                Id = sysaction.Id,
                                Code = sysaction.Code,
                                Description = sysaction.Description,
                                Enabled = sysaction.Enabled,
                                SystemScreenId = sysaction.SystemScreenId,
                                IsNew = false,
                                IsRemoved = false

                            };
                        }
                        model.SecurityActions.Add(securityActionViewModel);


                    }

                }


                listIndex.Add(model);

            }



            if (listIndex == null)
            {
                return NotFound();
            }

            return Ok(listIndex);
        }

        // GET: api/ConfigScreens/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetConfigScreen([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var configScreen = await _context.ConfigScreens.FindAsync(id);

            if (configScreen == null)
            {
                return NotFound();
            }

            return Ok(configScreen);
        }

        // PUT: api/ConfigScreens/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConfigScreen([FromRoute] int id, [FromBody] ConfigScreen configScreen)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != configScreen.Id)
            {
                return BadRequest();
            }

            _context.Entry(configScreen).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConfigScreenExists(id))
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

        // POST: api/ConfigScreens
        [HttpPost]
        public async Task<IActionResult> PostConfigScreen([FromBody] ConfigScreen configScreen)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ConfigScreens.Add(configScreen);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConfigScreen", new { id = configScreen.Id }, configScreen);
        }

        // DELETE: api/ConfigScreens/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConfigScreen([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var configScreen = await _context.ConfigScreens.FindAsync(id);
            if (configScreen == null)
            {
                return NotFound();
            }

            _context.ConfigScreens.Remove(configScreen);
            await _context.SaveChangesAsync();

            return Ok(configScreen);
        }

        private bool ConfigScreenExists(int id)
        {
            return _context.ConfigScreens.Any(e => e.Id == id);
        }
    }
}