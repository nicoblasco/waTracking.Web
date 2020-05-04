using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using waTracking.Data;
using waTracking.Entities.Security;
using waTracking.Web.Models.Security.User;

namespace waTracking.Web.Controllers
{
    [EnableCors("SiteCorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityUsersController : ControllerBase
    {
        private readonly DbContextApp _context;
        private readonly IConfiguration _config;

        public SecurityUsersController(DbContextApp context, IConfiguration config)
        {
            _context = context;
            _config = config;

        }

        // GET: api/SecurityUsers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Int64 id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.SecurityUsers.Where(x => x.Id == id).FirstOrDefaultAsync();
            //var user = await _context.SecurityUsers.Where(x => x.Id == id).Include(x => x.Company.ConfigScreen).ThenInclude(c=>c.ConfigScreenFields).FirstOrDefaultAsync();


            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // GET: api/SecurityUser/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<UserViewModel>> Listar()
        {
            var usuario = await _context.SecurityUsers.Include(x => x.Rol).ToListAsync();

            return usuario.Select(x => new UserViewModel
            {
                Id = x.Id,
                SecurityRoleId = x.SecurityRoleId,
                Rol = x.Rol.Name,
                Nombre = x.Nombre,
                Condicion = x.Condicion,
                Direccion = x.Direccion,
                Email = x.Email,
                Num_documento = x.Num_documento,
                Password_hash = x.Password_hash,
                Telefono = x.Telefono,
                Tipo_documento = x.Tipo_documento


            });
        }

        // POST: api/SecurityUsers/Crear
        [HttpPost("[action]")]
        public async Task<ActionResult> Crear([FromBody] CreateUserViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var email = model.Email.ToLower();
            if (await _context.SecurityUsers.AnyAsync(x => x.Email == email))
            {
                return BadRequest("El email ya existe");
            }

            CrearPasswordHash(model.Password, out byte[] passwordHash, out byte[] passwordSalt);

            SecurityUser usuario = new SecurityUser
            {
                SecurityRoleId = model.SecurityRoleId,
                Nombre = model.Nombre,
                Tipo_documento = model.Tipo_documento,
                Num_documento = model.Num_documento,
                Direccion = model.Direccion,
                Telefono = model.Telefono,
                Email = model.Email.ToLower(),
                Password_hash = passwordHash,
                Password_salt = passwordSalt,
                Condicion = true
            };


            _context.SecurityUsers.Add(usuario);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                return BadRequest();
            }

            return Ok();
        }


        // PUT: api/SecurityUsers/Actualizar/5
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] UpdateUserViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.Id <= 0)
            {
                return BadRequest();
            }

            var usuario = await _context.SecurityUsers.FirstOrDefaultAsync(x => x.Id == model.Id);

            usuario.SecurityRoleId = model.SecurityRoleId;
            usuario.Nombre = model.Nombre;
            usuario.Tipo_documento = model.Tipo_documento;
            usuario.Num_documento = model.Num_documento;
            usuario.Direccion = model.Direccion;
            usuario.Telefono = model.Telefono;
            usuario.Email = model.Email.ToLower();

            if (model.Act_password == true)
            {
                CrearPasswordHash(model.Password, out byte[] passwordHash, out byte[] passwordSalt);
                usuario.Password_hash = passwordHash;
                usuario.Password_salt = passwordSalt;
            }


            //_context.Entry(categoria).State = EntityState.Modified;

            if (usuario == null)
            {
                return NotFound();
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //Guardar Exception
                return BadRequest();
            }

            return Ok();
        }

        private void CrearPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

        }

        // PUT: api/SecurityUsers/Desactivar/5
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {


            if (id <= 0)
            {
                return BadRequest();
            }

            var usuario = await _context.SecurityUsers.FirstOrDefaultAsync(x => x.Id == id);

            if (usuario == null)
            {
                return NotFound();
            }


            usuario.Condicion = false;
            //_context.Entry(categoria).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //Guardar Exception
                return BadRequest();
            }

            return Ok();
        }


        // PUT: api/SecurityUsers/Activar/5
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {


            if (id <= 0)
            {
                return BadRequest();
            }

            var usuario = await _context.SecurityUsers.FirstOrDefaultAsync(x => x.Id == id);

            if (usuario == null)
            {
                return NotFound();
            }


            usuario.Condicion = true;
            //_context.Entry(categoria).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //Guardar Exception
                return BadRequest();
            }

            return Ok();
        }


        // PUT: api/SecurityUsers/Login
        //[AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var email = model.Email.ToLower();
            var usuario = await _context.SecurityUsers.Where(x => x.Condicion == true).Include(x => x.Rol).FirstOrDefaultAsync(x => x.Email == email);

            if (usuario == null)
            {
                return NotFound();
            }

            if (!VerificarPasswordHash(model.Password, usuario.Password_hash, usuario.Password_salt))
            {
                return NotFound();

            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Email,email),
                new Claim(ClaimTypes.Role ,usuario.Rol.Name),
                new Claim(ClaimTypes.Name ,usuario.Nombre),
               // new Claim(ClaimTypes.UserData ,usuario.CompanyId.ToString()),
                //new Claim("Id", usuario.Id.ToString()),
                new Claim("Rol",usuario.SecurityRoleId.ToString()),
                //new Claim("Nombre",usuario.Nombre),
                new Claim("CompanyId",usuario.CompanyId.ToString())

            };

            return Ok(
                new { token = GenerarToken(claims) }
            );

        }

        private bool VerificarPasswordHash(string password, byte[] passwordHashAlmacenado, byte[] passwordSalt)
        {

            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var passwordHashNuevo = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return new ReadOnlySpan<byte>(passwordHashAlmacenado).SequenceEqual(new ReadOnlySpan<byte>(passwordHashNuevo));
            }

        }

        private string GenerarToken(List<Claim> claims)
        {

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Clave personalizada"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _config["Jwt.Issuer"],
                _config["Jwt.Issuer"],
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds,
                claims: claims);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        private bool SecurityUserExists(int id)
        {
            return _context.SecurityUsers.Any(e => e.Id == id);
        }
    }
}