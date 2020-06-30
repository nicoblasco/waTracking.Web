using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using waTracking.Data;
using waTracking.Entities.System;
using waTracking.Web.Helpers;
using waTracking.Web.Models.System.Logs;

namespace waTracking.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogErrorsController : ControllerBase
    {
        private readonly DbContextApp _context;

        public LogErrorsController(DbContextApp context)
        {
            _context = context;
        }

        // POST: api/LogErrors/Create
        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CreationLogErrorViewModel model)
        {
            try
            {
                LogError log = new LogError
                {
                    CompanyId= model.CompanyId,
                    CreationDate= DateTime.Now,
                    Path = model.Path,
                    SecurityUserId= model.SecurityUserId,
                    Error= StringExt.Truncate(model.Error,7999)
                };

                _context.LogErrors.Add(log);

                await _context.SaveChangesAsync();
                return Ok(log.Id);
            }
            catch (Exception ex)
            {

                return BadRequest();
            }

            
        }


    }
}