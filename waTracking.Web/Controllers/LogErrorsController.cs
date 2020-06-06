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

        public  void LogError(string controller, string method, string error, string comment = null)
        {
            LogError log = new LogError
            {
                Controller = controller,
                Method= method,
                Comment= comment,
                Error= error
            };

            _context.LogErrors.Add(log);
             _context.SaveChangesAsync();
        }
    }
}