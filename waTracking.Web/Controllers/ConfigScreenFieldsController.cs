using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using waTracking.Data;
using waTracking.Entities.Configuration;
using waTracking.Entities.System;
using waTracking.Web.Models.Configuration.ScreenField;

namespace waTracking.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigScreenFieldsController : ControllerBase
    {
        private readonly DbContextApp _context;

        public ConfigScreenFieldsController(DbContextApp context)
        {
            _context = context;
        }

        // GET: api/ConfigScreenFields
        [HttpGet]
        public IEnumerable<ConfigScreenField> GetConfigScreenFields()
        {
            return _context.ConfigScreenFields;
        }


    }
}