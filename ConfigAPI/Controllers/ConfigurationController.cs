using ConfigAPI.Model;
using ConfigAPI.Models;
using ConfigAPI.Models.ParameterModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {

        private readonly MyContext _context;

        public ConfigurationController(MyContext context)
        {
            _context = context;
        }
        // GET: api/<ConfigurationController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Configuration>>> GetAllConfigurations()
        {
            try
            {
                if (!Authenticate())
                {
                    return Unauthorized();
                }
                return await _context.Configurations.ToListAsync();

            }
            catch (Exception e)
            {

                System.Diagnostics.Debug.WriteLine($"ERROR1: {e.Message}");
                return BadRequest();
            }
            

        }

        // GET api/<ConfigurationController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Object>> GetConfiguration(int id)
        {
            var obj = new { configuration = new Configuration(), items = new List<Item>() };

            try
            {

                if (!Authenticate())
                {
                    return Unauthorized();
                }

                var configuration = await _context.Configurations.FindAsync(id);

                var relationList = _context.ConfigurationsItems.Where(i => i.ConfigurationId == id).ToList();

                List<Item> itemList = new List<Item>();

                foreach (var r in relationList)
                {
                    var item = _context.Items.Find(r.ItemId);
                    itemList.Add(item);
                }



                obj = new { configuration = configuration, items = itemList };

            }
            catch (Exception e)
            {

                System.Diagnostics.Debug.WriteLine($"ERROR: {e.Message}");
                return BadRequest();
            }

            return obj;
        }

        // POST api/<ConfigurationController>
        [HttpPost]
        public async Task<ActionResult<Item>> Create(CreateConfigurationModel configModel)
        {


            try
            {
                if (!Authenticate())
                {
                    return Unauthorized();
                }

                var configuration = await _context.Configurations.AddAsync(configModel.Configuration);
                _context.SaveChanges();

                foreach (var i in configModel.Items)
                {
                    ConfigurationsItems ci = new ConfigurationsItems();
                    ci.ConfigurationId = configuration.Entity.Id;
                    ci.ItemId = i.Id;
                    var res = await _context.ConfigurationsItems.AddAsync(ci);


                }

                _context.SaveChanges();
            }
            catch (Exception e)
            {

                System.Diagnostics.Debug.WriteLine($"ERROR1: {e.Message}");
                return BadRequest();

            }


            return Ok();
        }

        //PUT api/<ConfigurationController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Configuration>> UpdateAConfiguration( int id,[FromBody] CreateConfigurationModel model)
        {
            try
            {

                if (!Authenticate())
                {
                    return Unauthorized();
                }

                var relationList = _context.ConfigurationsItems.Where(i => i.ConfigurationId == id).ToList();

                if (relationList != null)
                {
                    foreach (var r in relationList)
                    {
                        var temp = _context.Remove(r);
                    }
                }


                foreach (var item in model.Items)
                {
                    ConfigurationsItems ci = new ConfigurationsItems();
                    ci.ConfigurationId = id;
                    ci.ItemId = item.Id;

                    _context.Add(ci);
                }

                model.Configuration.Id = id;
                _context.Update(model.Configuration);

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"ERROR: {e.Message}");
                return BadRequest();
            }

            return Ok();

        }

        //DELETE api/<ConfigurationController>/5
            [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteConfiguration(int Id)
        {
            try
            {
                if (!Authenticate())
                {
                    return Unauthorized();
                }

                var configuration = await _context.Configurations.FindAsync(Id);
                if (configuration == null)
                {
                    return NotFound();
                }
                _context.Configurations.Remove(configuration);
                await _context.SaveChangesAsync();
                
            }
            catch (Exception e)
            {

                System.Diagnostics.Debug.WriteLine($"ERROR: {e.Message}");
                return BadRequest();
            }

            return Ok();
        }

        bool Authenticate()
        {
            var allowedKeys = new[] { "tNL1Jrv6pEEO5h50RCrB" };
            StringValues key = Request.Headers["Key"];
            int count = (from t in allowedKeys where t == key select t).Count();
            return count == 0 ? false : true;
        }
    }
}
