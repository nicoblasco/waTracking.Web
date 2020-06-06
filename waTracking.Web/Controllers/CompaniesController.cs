using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using waTracking.Data;
using waTracking.Entities.Configuration;
using waTracking.Entities.Security;
using waTracking.Web.Models.Configuration.Company;

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
        public IEnumerable<Company> GetCompanies()
        {
            return _context.Companies;
        }

        // GET: api/Companies/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompany([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var company = await _context.Companies.FindAsync(id);

            if (company == null)
            {
                return NotFound();
            }

            return Ok(company);
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
                        i++;
                    }



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