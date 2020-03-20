using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using waTracking.Data;
using waTracking.Entities.TrackerLog;
using waTracking.Web.Models.TrackerLog;

namespace waTracking.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackerLogsController : ControllerBase
    {
        private readonly DbContextApp _context;

        public TrackerLogsController(DbContextApp context)
        {
            _context = context;
        }

        // GET: api/TrackerLogs
        [HttpGet]
        public IEnumerable<TrackerLog> GetTrackerLogs()
        {
            return _context.TrackerLogs;
        }

        //[HttpGet]
        //public ActionResult<IEnumerable<string>> Get()
        //{
        //    return new string[] { "value1", "value2" };//_context.TrackerLogs;
        //}


        // GET: api/TrackerLogs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrackerLog([FromRoute] Int64 id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trackerLog = await _context.TrackerLogs.FindAsync(id);

            if (trackerLog == null)
            {
                return NotFound();
            }

            return Ok(trackerLog);
        }

        // PUT: api/TrackerLogs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrackerLog([FromRoute] Int64 id, [FromBody] TrackerLog trackerLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != trackerLog.Id)
            {
                return BadRequest();
            }

            _context.Entry(trackerLog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrackerLogExists(id))
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

        // POST: api/TrackerLogs/Create
        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CreateTrackerLogViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TrackerLog trackerLog = new TrackerLog
            {
                Message= model.Message,
                CreationDate = DateTime.Now
            };

            _context.TrackerLogs.Add(trackerLog);
            try
            {
                 await _context.SaveChangesAsync();
                Int64 id = trackerLog.Id;

            }
            catch (Exception ex)
            {

                return BadRequest();
            }

            return Ok(trackerLog);
        }

        // DELETE: api/TrackerLogs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrackerLog([FromRoute] Int64 id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trackerLog = await _context.TrackerLogs.FindAsync(id);
            if (trackerLog == null)
            {
                return NotFound();
            }

            _context.TrackerLogs.Remove(trackerLog);
            await _context.SaveChangesAsync();

            return Ok(trackerLog);
        }

        private bool TrackerLogExists(Int64 id)
        {
            return _context.TrackerLogs.Any(e => e.Id == id);
        }
    }
}