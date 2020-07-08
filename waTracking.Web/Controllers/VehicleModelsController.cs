using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using waTracking.Data;
using waTracking.Entities.Vehicle;
using waTracking.Web.Models.Vehicle.Model;

namespace waTracking.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleModelsController : ControllerBase
    {
        private readonly DbContextApp _context;

        public VehicleModelsController(DbContextApp context)
        {
            _context = context;
        }

        // GET: api/VehicleModels
        [HttpGet("{companyId}")]
        public IEnumerable<IndexVehicleModelViewModel> GetVehicleModels([FromRoute] int companyId)
        {

            List<VehicleModel> vehicleModels = _context.VehicleModels.Where(x => x.Enabled == true && x.VehicleBrand.CompanyId== companyId).Include(x => x.VehicleBrand).ToList();
            List<IndexVehicleModelViewModel> list = new List<IndexVehicleModelViewModel>();
            foreach (var item in vehicleModels)
            {
                IndexVehicleModelViewModel indexVehicleModelView = new IndexVehicleModelViewModel
                {
                    VehicleBrandId = item.VehicleBrandId,
                    Brand = item.VehicleBrand?.Description,
                    Description = item.Description,
                    Enabled = item.Enabled,
                    Id = item.Id

                };
                list.Add(indexVehicleModelView);
            }

            return list;
        }

        // GET: api/VehicleModels/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicleModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vehicleModel = await _context.VehicleModels.Where(x => x.Id == id).Include(x => x.VehicleBrand).FirstOrDefaultAsync();
            IndexVehicleModelViewModel indexVehicleModelView = new IndexVehicleModelViewModel
            {
                VehicleBrandId = vehicleModel.VehicleBrandId,
                Brand = vehicleModel.VehicleBrand?.Description,
                Description= vehicleModel.Description,
                Enabled = vehicleModel.Enabled,
                Id = vehicleModel.Id

            };


            if (vehicleModel == null)
            {
                return NotFound();
            }

            return Ok(indexVehicleModelView);
        }

        // PUT: api/VehicleModels/Create
        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CreateVehicleModelViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            VehicleModel vehicle = new VehicleModel
            {
                Description = model.Description,
                Enabled = true,
                VehicleBrandId= model.VehicleBrandId                
            };


            _context.VehicleModels.Add(vehicle);
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

        // POST: api/VehicleModels/Update
        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] UpdateVehicleModelViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.Id <= 0)
            {
                return BadRequest();
            }

            var vehicle = await _context.VehicleModels.FirstOrDefaultAsync(x => x.Id == model.Id);

            vehicle.Description = model.Description;
            vehicle.VehicleBrandId = model.VehicleBrandId;


            _context.Entry(vehicle).State = EntityState.Modified;

            if (vehicle == null)
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


        // DELETE: api/VehicleModels/Delete/5
        [HttpPut("[action]")]
        public async Task<IActionResult> Delete([FromBody] DeleteVehicleModelViewModel model)
        {


            if (model.Id <= 0)
            {
                return BadRequest();
            }

            var vehicle = await _context.VehicleModels.FirstOrDefaultAsync(x => x.Id == model.Id);

            vehicle.Enabled = false;



            _context.Entry(vehicle).State = EntityState.Modified;

            if (vehicle == null)
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
    }
}