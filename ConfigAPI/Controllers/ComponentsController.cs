using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConfigAPI.Model;
using Microsoft.Extensions.Primitives;

namespace ConfigAPI.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class ComponentsController : ControllerBase
    {
        private readonly MyContext _context;

        public ComponentsController(MyContext context)
        {
            _context = context;
        }

        //This method returns all components.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Component>>> GetComponents()
        {
            List<Component> list = new List<Component>();
            try
            {
                if (!Authenticate())
                {
                    return Unauthorized();
                }

                list = _context.Components.ToList();


            }
            catch (Exception e)
            {

                System.Diagnostics.Debug.WriteLine($"ERROR: {e.Message}");
                return BadRequest();
            }

            return list;
        }


        //This method returns a component by its id.
        [HttpGet("{id}")]
        public async Task<ActionResult<Component>> GetComponent(int id)
        {
            try
            {
                if (!Authenticate())
                {
                    return Unauthorized();
                }

                var component = await _context.Components.FindAsync(id);
                if (component == null)
                {
                    return NotFound();
                }

                return component;
            }
            catch (Exception e)
            {

                System.Diagnostics.Debug.WriteLine($"ERROR: {e.Message}");
                return BadRequest();
            }
            
            
        }

        //This method create a new component.
        [HttpPost]
        public async Task<ActionResult<Component>> Create(Component component)
        {
            try
            {
                if (!Authenticate())
                {
                    return Unauthorized();
                }
                _context.Components.Add(component);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {

                System.Diagnostics.Debug.WriteLine($"ERROR: {e.Message}");
                return BadRequest();
            }

            return Ok();
        }


        //This method edit a component.
        [HttpPut("{id}")]
        public async Task<ActionResult<Component>> Edit(int id, [FromBody] Component component)
        {
            try
            {

                if (!Authenticate())
                {
                    return Unauthorized();
                }

                component.Id = id;
                _context.Update(component);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"ERROR: {e.Message}");
                return BadRequest();
            }
            
            return Ok();
        }

        //This method delete a component. 
        [HttpDelete("{id}")]
        public async Task<ActionResult<Component>> Delete(int id)
        {
            try
            {
                if (!Authenticate())
                {
                    return Unauthorized();
                }

                var component = await _context.Components.FindAsync(id);
                if (component == null)
                {
                    return NotFound();
                }
                _context.Components.Remove(component);
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
