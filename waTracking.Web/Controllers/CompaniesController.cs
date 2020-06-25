using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using waTracking.Data;
using waTracking.Entities.Configuration;
using waTracking.Entities.Security;
using waTracking.Web.Models.Configuration.Company;
using static System.Net.Mime.MediaTypeNames;

namespace waTracking.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CompaniesController : ControllerBase
    {
        private readonly DbContextApp _context;
        private readonly IConfiguration _config;


        public CompaniesController(DbContextApp context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // GET: api/Companies
        [HttpGet]
        public IEnumerable<CompaniesViewModel> GetCompanies()
        {
            List<CompaniesViewModel> list = new List<CompaniesViewModel>();
            foreach (var item in _context.Companies.ToList())
            {
                CompaniesViewModel company = new CompaniesViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    ContactLastName = item.ContactLastName,
                    ContactName = item.ContactName,
                    Email = item.Email,
                    Enabled = item.Enabled,
                    InitialDate = item.InitialDate?.ToString("dd/MM/yyyy"),
                    Phone = item.Phone,
                    Website = item.Website
                };
                if (!string.IsNullOrEmpty(item.Logo))
                {
                    byte[] imageArray = System.IO.File.ReadAllBytes(@item.Logo);
                    string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                    company.Logo = "data:image/png;base64,"+ base64ImageRepresentation;
                }
                

                list.Add(company);
            }
            return list;
        }

        // GET: api/Companies/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompany([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            //var company = await _context.Companies.FindAsync(id);
            var company = await _context.Companies.Include(x=>x.CompanySectors).Where(x=>x.Id==id).FirstOrDefaultAsync();
            CompaniesViewModel viewModel = new CompaniesViewModel
            {
                Id = company.Id,
                Name = company.Name,
                ContactName = company.ContactName,
                ContactLastName = company.ContactLastName,
                Enabled = company.Enabled,
                InitialDate = company.InitialDate?.ToString("dd/MM/yyyy"),
                Email = company.Email,
                Phone = company.Phone,
                City = company.City,
                State = company.State,
                Country = company.Country,
                Schedule = company.Schedule,                
                Comment = company.Comment,
                Address= company.Address,
                Postal = company.Postal,
                Website = company.Website,                
                Sectors = company.CompanySectors.Select(x=>x.SectorId).ToList()
                
            };
            if (!string.IsNullOrEmpty(company.Logo))
            {
                byte[] imageArray = System.IO.File.ReadAllBytes(@company.Logo);
                viewModel.LogoName = Path.GetFileName(@company.Logo);
                string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                viewModel.Logo = "data:image/png;base64," + base64ImageRepresentation;
            }


            if (viewModel == null)
            {
                return NotFound();
            }

            return Ok(viewModel);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CreateCompanyViewModel model)
        {
            string userPassword = CreatePassword(6);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                
                string strRuta = _config["AvatarFiles"];
                Company company = new Company
                {
                   Name = model.Name,
                   ContactName= model.ContactName,
                   ContactLastName = model.ContactLastName,
                   InitialDate = model.InitialDate,
                   Email = model.Email,
                   Phone = model.Phone,
                   Website = model.Website,
                   Address = model.Address,
                   Postal = model.Postal,
                   City = model.City,
                   State = model.State,
                   Country = model.Country,
                   Schedule = model.Schedule,
                   Comment=model.Comment,
                   CreationDate = DateTime.Now,
                   Enabled=true
                };

                
                //Agrego los sectores
                if (model.CompanySectors!=null)
                {
                    company.CompanySectors = new List<CompanySector>();
                    foreach (var sector in model.CompanySectors)
                    {
                        CompanySector companySector = new CompanySector
                        {
                            CompanyId = company.Id,
                            SectorId = sector.SectorId
                        };
                        company.CompanySectors.Add(companySector);
                    }
                }


                //Agrego las pantallas
                if (model.ConfigScreens != null)
                {
                    company.ConfigScreen = new List<ConfigScreen>();                                        
                    foreach (var item in model.ConfigScreens)
                    {
                        ConfigScreen configScreen = new ConfigScreen
                        {
                            CompanyId=company.Id,
                            Description = item.Description,
                            Enabled= item.Enabled,
                            Orden = item.Orden,
                            Icon = item.Icon,
                            SystemScreenId= item.SystemScreenId                            
                        };

                        //Dentro de las pantallas agrego los Atributos
                        if (item.ConfigScreenFields != null) {
                            configScreen.ConfigScreenFields = new List<ConfigScreenField>();
                            foreach (var field in item.ConfigScreenFields)
                            {

                                ConfigScreenField configScreenField = new ConfigScreenField
                                {
                                    ConfigScreenId= item.Id,
                                    Name= field.Name,
                                    Required= field.Required,
                                    Visible= field.Visible,
                                    DefaultValue= field.DefaultValue,
                                    Enabled= field.Enabled,
                                    FieldName = field.FieldName,
                                   SystemScreenFieldId = field.SystemScreenFieldId,

                                };
                                configScreen.ConfigScreenFields.Add(configScreenField);
                            }
                        }

                        //Dentro de las pantallas agrego las Acciones
                        if (item.SecurityActions != null)
                        {
                            configScreen.SecurityActions = new List<SecurityAction>();
                            foreach (var action in item.SecurityActions)
                            {
                                SecurityAction securityAction = new SecurityAction
                                {
                                    ConfigScreenId= item.Id,
                                    Description = action.Description,
                                    Enabled = action.Enabled,
                                    SystemActionId = action.SystemActionId,
                                    
                                    
                                };
                                configScreen.SecurityActions.Add(securityAction);
                            }
                        }

                        company.ConfigScreen.Add(configScreen);

                    }
                }



                _context.Companies.Add(company);
                await _context.SaveChangesAsync();




                //Agrego los roles
                ////Agrego las pantallas que aplican para ese rol
                company.SecurityRoles = new List<SecurityRole>();

                foreach (var rol in model.SecurityRoles)
                {

                    int i = 0;
                    SecurityRole securityRole = new SecurityRole
                    {
                        Name = rol.Name,
                        CompanyId= company.Id,
                        Description= rol.Description,
                        Enabled= true,
                        SystemRoleId= rol.SystemRoleId
                        
                    };


                    if (i == 0) {
                        //Genero el usuario
                        securityRole.Usuarios = new List<SecurityUser>();


                        CrearPasswordHash(userPassword, out byte[] passwordHash, out byte[] passwordSalt);

                        SecurityUser user = new SecurityUser
                        {
                            CompanyId = company.Id,
                            Condicion = true,
                            Direccion = company.Address,
                            Email = company.Email,
                            Nombre = company.Name,
                            SecurityRoleId = securityRole.Id,
                            Password_hash = passwordHash,
                            Password_salt = passwordSalt
                        };

                        securityRole.Usuarios.Add(user);
                       
                    }
                    i++;


                    securityRole.SecurityRoleScreens = new List<SecurityRoleScreen>();
                    securityRole.SecurityRoleActions = new List<SecurityRoleAction>();
                    foreach (var screen in company.ConfigScreen)
                    {
                        //Si la pantalla esta en el rol, la agrego
                        if (rol.SecurityRoleScreens.Where(x=>x.SystemScreenId==screen.SystemScreenId).Any() )
                        {
                            SecurityRoleScreen securityRoleScreen = new SecurityRoleScreen
                            {
                                ConfigScreenId = screen.Id,
                                SecurityRoleId = securityRole.Id
                            };
                            securityRole.SecurityRoleScreens.Add(securityRoleScreen);


                            foreach (var action in screen.SecurityActions)
                            {
                                if ( rol.SecurityRoleActions.Where(x=>x.SystemActionId==action.SystemActionId).Any() )
                                {
                                    SecurityRoleAction securityRoleAction = new SecurityRoleAction
                                    {
                                        SecurityActionId=action.Id,
                                        SecurityRoleId= securityRole.Id
                                    };
                                    securityRole.SecurityRoleActions.Add(securityRoleAction);
                                    
                                }
                            }
                        }
                        
                    }


                    company.SecurityRoles.Add(securityRole);                    
                }

                //Guardo el avatar
                if (company.Id>0)
                {
                    if (!(string.IsNullOrEmpty(model.LogoName)) && (!string.IsNullOrEmpty(model.Logo)))
                    {
                        strRuta = strRuta + "//" + company.Id.ToString() + "//" + model.LogoName;
                        System.IO.FileInfo file = new System.IO.FileInfo(strRuta);
                        file.Directory.Create();
                        System.IO.File.WriteAllBytes(strRuta, Convert.FromBase64String(model.Logo.Substring(model.Logo.LastIndexOf(',') + 1)));
                        company.Logo = strRuta;
                    }
                }

                _context.Entry(company).State = EntityState.Modified;
                await _context.SaveChangesAsync();


            }
            catch (Exception ex)
            {
                
                return BadRequest();
            }

            return Ok(userPassword);
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Update([FromBody] UpdateCompanyViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                string strRuta = _config["AvatarFiles"];

                Company company = await _context.Companies.Include(x => x.CompanySectors).Include(x => x.SecurityRoles).ThenInclude(x=>x.SecurityRoleScreens).Include(x => x.SecurityRoles).ThenInclude(x => x.SecurityRoleActions).Include(x => x.ConfigScreen).ThenInclude(x => x.SecurityActions).Include(x => x.ConfigScreen).ThenInclude(x=> x.ConfigScreenFields ).Where(x => x.Id == model.Id).FirstOrDefaultAsync();
                var listRoles = company.SecurityRoles;
                if (company==null)
                    return  BadRequest("Compania inexistente");

                //Actualizo la compania
                company.Name = model.Name;
                company.ContactName = model.ContactName;
                company.ContactLastName = model.ContactLastName;
                company.InitialDate = model.InitialDate;
                company.Email = model.Email;
                company.Phone = model.Phone;
                company.Website = model.Website;
                company.Address = model.Address;
                company.Postal = model.Postal;
                company.City = model.City;
                company.State = model.State;
                company.Country = model.Country;
                company.Schedule = model.Schedule;
                company.Comment = model.Comment;

                ////Guardo el avatar
                if (!(string.IsNullOrEmpty(model.LogoName)) && (!string.IsNullOrEmpty(model.Logo)))
                {
                  strRuta = strRuta + "//" + company.Id.ToString() + "//" + model.LogoName;
                  System.IO.FileInfo file = new System.IO.FileInfo(strRuta);
                  file.Directory.Create();
                  System.IO.File.WriteAllBytes(strRuta, Convert.FromBase64String(model.Logo.Substring(model.Logo.LastIndexOf(',') + 1)));
                  company.Logo = strRuta;
                }


                //Sectores
                var sectorToBeAdded = model.CompanySectors.Where(a => company.CompanySectors.All(
                     b => b.SectorId != a.SectorId));

                var sectorToBeDeleted = company.CompanySectors.Where(a => model.CompanySectors.All(
                     b => b.SectorId != a.SectorId));

                foreach (var sector in sectorToBeDeleted)
                {
                    company.CompanySectors.Remove(sector);
                }

                foreach (var sector in sectorToBeAdded)
                {
                    company.CompanySectors.Add(sector);
                }

                //Screens
                //Solo vienen las activas
                var screenToBeAdded = model.ConfigScreens.Where(a => company.ConfigScreen.All(
                    b => b.SystemScreenId != a.SystemScreenId));

                var screenToBeDeleted = company.ConfigScreen.Where(a => model.ConfigScreens.All(
                    b => b.SystemScreenId != a.SystemScreenId));


                //Si no se dan de alta o se eliminan, los actualizo
                //son todos los que no se graban + los que no se eliminan
                List<ConfigScreen> screenNotbeUpdated = new List<ConfigScreen>();
                List<ConfigScreen> screenEnabled = new List<ConfigScreen>();
                screenNotbeUpdated.AddRange(screenToBeAdded);
                screenNotbeUpdated.AddRange(screenToBeDeleted);
                var screenToBeUpdated = model.ConfigScreens.Where(a => screenNotbeUpdated.All(
                    b => b.SystemScreenId != a.SystemScreenId));

                screenEnabled.AddRange(screenToBeAdded);
                screenEnabled.AddRange(screenToBeUpdated);

                //Elimino las pantallas del paso 2
                foreach (var screenDtl in screenToBeDeleted.ToList())
                {
                    //Borro los childs
                    ConfigScreen configScreen = company.ConfigScreen.Where(x => x.SystemScreenId == screenDtl.SystemScreenId).FirstOrDefault();
                    //Seteo las dependencias
                    var securityactionsToDelete = configScreen.SecurityActions;
                    var configScreenFieldToDelete= configScreen.ConfigScreenFields;
                  

                    //Borro las acciones
                    foreach (var action in securityactionsToDelete.ToList())
                    {
                        configScreen.SecurityActions.Remove(action);
                    }

                    //Borro los atributos
                    foreach (var attr in configScreenFieldToDelete.ToList())
                    {
                        configScreen.ConfigScreenFields.Remove(attr);
                    }

                    var listRolesToDelete = listRoles;

                    //Borro las pantallas de los roles
                    foreach (var item in listRolesToDelete.ToList())
                    {                        
                        var listRolesActionToDelete = item.SecurityRoleActions.Where(x => x.SecurityAction.ConfigScreen.SystemScreenId == screenDtl.SystemScreenId).ToList();
                        foreach (var action in listRolesActionToDelete)
                        {
                            item.SecurityRoleActions.Remove(action);
                        }

                        var listRolesScreenToDelete = item.SecurityRoleScreens.Where(x => x.ConfigScreen.SystemScreenId == screenDtl.SystemScreenId).ToList();
                        foreach (var screen in listRolesScreenToDelete)
                        {
                            item.SecurityRoleScreens.Remove(screen);
                        }
                    }

                    company.ConfigScreen.Remove(configScreen);
                }

                //Modifico
                foreach (var screen in company.ConfigScreen)
                {
                    ConfigScreen configScreen = screenToBeUpdated.Where(x => x.SystemScreenId == screen.SystemScreenId).FirstOrDefault();
                    if (configScreen!=null)
                    {

                        screen.Description = configScreen.Description;
                        screen.Enabled = configScreen.Enabled;
                        screen.Icon = configScreen.Icon;
                        screen.Orden = configScreen.Orden;

                    }

                    //Actualizo los fields
                    if (configScreen.ConfigScreenFields != null) {
                        foreach (var item1 in screen.ConfigScreenFields)
                        {
                            foreach (var item2 in configScreen.ConfigScreenFields)
                            {
                                if (item1.SystemScreenFieldId == item2.SystemScreenFieldId)
                                {
                                    item1.Enabled = item2.Enabled;
                                    item1.DefaultValue = item2.DefaultValue;
                                    item1.FieldName = item2.FieldName;
                                    item1.Name = item2.Name;
                                    item1.Required = item2.Required;
                                    item2.Visible = item2.Visible;
                                }
                            } 
                        }
                    }

                    //Actualizo las acciones
                    if (configScreen.SecurityActions != null)
                    {
                        foreach (var item1 in screen.SecurityActions)
                        {
                            foreach (var item2 in configScreen.SecurityActions)
                            {
                                if (item1.SystemActionId == item2.SystemActionId)
                                {
                                    item1.Enabled = item2.Enabled;
                                    item1.Description = item2.Description;
                                }
                            }
                        }
                    }

                }

                
                //Agrego
                foreach (var screenAdd in screenToBeAdded.ToList())
                {
                    ConfigScreen configScreenAdd = new ConfigScreen
                    {
                        CompanyId= model.Id,
                        Description = screenAdd.Description,
                        Enabled= screenAdd.Enabled,
                        Orden = screenAdd.Orden,
                        Icon=screenAdd.Icon,
                        SystemScreenId = screenAdd.SystemScreenId
                    };

                    //tambien agrego los atributos

                    //Dentro de las pantallas agrego los Atributos
                    if (screenAdd.ConfigScreenFields != null)
                    {
                        configScreenAdd.ConfigScreenFields = new List<ConfigScreenField>();
                        foreach (var field in screenAdd.ConfigScreenFields)
                        {

                            ConfigScreenField configScreenField = new ConfigScreenField
                            {
                                ConfigScreenId = screenAdd.Id,
                                Name = field.Name,
                                Required = field.Required,
                                Visible = field.Visible,
                                DefaultValue = field.DefaultValue,
                                Enabled = field.Enabled,
                                FieldName = field.FieldName,
                                SystemScreenFieldId = field.SystemScreenFieldId,

                            };
                            screenAdd.ConfigScreenFields.Add(configScreenField);
                        }
                    }

                    //Dentro de las pantallas agrego las Acciones
                    if (screenAdd.SecurityActions != null)
                    {
                        configScreenAdd.SecurityActions = new List<SecurityAction>();
                        foreach (var action in screenAdd.SecurityActions)
                        {
                            SecurityAction securityAction = new SecurityAction
                            {
                                ConfigScreenId = screenAdd.Id,
                                Description = action.Description,
                                Enabled = action.Enabled,
                                SystemActionId = action.SystemActionId,


                            };
                            configScreenAdd.SecurityActions.Add(securityAction);
                        }
                    }



                    company.ConfigScreen.Add(configScreenAdd);
                }

                //SecurityRoles
                //Vienen todos los roles               
                foreach  (var modelRol in model.SecurityRoles.ToList())
                {
                    //Paso 5
                    SecurityRole securityRole = company.SecurityRoles.Where(x => x.SystemRoleId == modelRol.SystemRoleId).FirstOrDefault();

                    if (securityRole != null)
                    {
                        securityRole.Description = modelRol.Description;
                        securityRole.Name = modelRol.Name;
                        securityRole.Enabled = modelRol.Enabled;

                        var screenRolToBeAdded = modelRol.SecurityRoleScreens.Where(a => securityRole.SecurityRoleScreens.All(
                        b => b.ConfigScreen.SystemScreenId != a.SystemScreenId));

                        var screenRolToBeDeleted = securityRole.SecurityRoleScreens.Where(a => modelRol.SecurityRoleScreens.All(
                              b => b.SystemScreenId != a.ConfigScreen.SystemScreenId));

                        var actionRolToBeAdded = modelRol.SecurityRoleActions.Where(a => securityRole.SecurityRoleActions.All(
                            b => b.SecurityAction.SystemActionId != a.SystemActionId));

                        var actionRolToBeDeleted = securityRole.SecurityRoleActions.Where(a => modelRol.SecurityRoleActions.All(
                              b => b.SystemActionId != a.SecurityAction.SystemActionId));

                        //SCREENS
                        foreach (var item in screenRolToBeDeleted.ToList())
                        {
                            securityRole.SecurityRoleScreens.Remove(item);
                        }

                        foreach (var item2 in screenRolToBeAdded.ToList())
                        {

                            SecurityRoleScreen securityRoleScreen = new SecurityRoleScreen
                            {
                                SecurityRoleId = item2.SystemRoleId,
                                ConfigScreenId = company.ConfigScreen.Where(x => x.SystemScreenId == item2.SystemScreenId).Select(x => x.Id).FirstOrDefault()
                            };

                            securityRole.SecurityRoleScreens.Add(securityRoleScreen);
                        }

                        //ACCIONES
                        foreach (var item in actionRolToBeDeleted.ToList())
                        {
                            securityRole.SecurityRoleActions.Remove(item);
                        }

                        foreach (var item2 in actionRolToBeAdded.ToList())
                        {                           

                            var configScreen = company.ConfigScreen.Where(x => x.SystemScreenId == item2.SystemScreenId).FirstOrDefault();
                            if (configScreen != null)
                            {


                                foreach (var action in configScreen.SecurityActions)
                                {
                                    if (item2.SystemActionId == action.SystemActionId)
                                    {
                                        SecurityRoleAction securityRoleAction = new SecurityRoleAction
                                        {
                                            SecurityRoleId = item2.SystemRoleId
                                        };
                                        securityRoleAction.SecurityActionId = action.Id;
                                        securityRole.SecurityRoleActions.Add(securityRoleAction);
                                        break;
                                    }
                                }


                            }

                        }
                    }                                                                        
                }


                _context.Entry(company).State = EntityState.Modified;
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                return BadRequest();
            }

            return Ok();
        }

        // GET: api/Companies/UpdateState/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateState([FromBody] ChangeStateCompanyViewModel vm)
        {
            bool habilitar = true;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                


                var model = await _context.Companies.FindAsync(vm.Id);

                if (model == null)
                {
                    return NotFound();
                }
                if (model.Enabled)
                    habilitar = false;
                else
                    habilitar = true;

                model.Enabled = habilitar;
                _context.Entry(model).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }


            return Ok();
        }


        public string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        private void CrearPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

        }


    }
}