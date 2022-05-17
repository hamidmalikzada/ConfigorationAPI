using ConfigAPI.Model;
using ConfigAPI.Models;
using ConfigAPI.Models.ParameterModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.Primitives;

namespace ConfigAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly MyContext _context;
        public ItemsController(MyContext context)
        {
            _context = context;
        }


        //This Method returns all items.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems()
        {
            try
            {
                if (!Authenticate())
                {
                    return Unauthorized();
                }

                return await _context.Items.ToListAsync();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"ERROR: {e.Message}");
                return BadRequest();
            }
        }

        //This method returns an item by its id.
        [HttpGet("{id}")]
        public async Task<ActionResult<Object>> GetItem(int id)
        {
            var obj = new {item = new Item(), components = new List<Object>() };

            try
            {
                if (!Authenticate())
                {
                    return Unauthorized();
                }
                var item = await _context.Items.FindAsync(id);

                var relationList = _context.ItemsComponents.Where(i => i.ItemId == item.Id).ToList();

                List<Object> componentList = new List<Object>();

                foreach (var r in relationList)
                {
                    var c = new { component = new Component(), quantity = 0 };
                    var component  = _context.Components.Find(r.ComponentId);


                    c = new { component = component, quantity = r.Quantity };
                    
                    componentList.Add(c);
                }



                obj = new { item = item, components = componentList };

            }
            catch (Exception e)
            {

                System.Diagnostics.Debug.WriteLine($"ERROR: {e.Message}");
                return BadRequest();
            }

            return obj;
        }



        //This method create a new item.
        [HttpPost]
        public async Task<ActionResult<Item>> Create(CreateItemModel itemModel)
        {
      
            try
            {
                if (!Authenticate())
                {
                    return Unauthorized();
                }
                double totalPrice = 0;
                //Adding the total price of all components and stores it in the Item
                foreach (var c in itemModel.Components)
                {
                    totalPrice += (c.Quantity * c.Price);
                }

                itemModel.Item.Price = Math.Round(totalPrice, 2);

                var item = await _context.Items.AddAsync(itemModel.Item);
                _context.SaveChanges();

                foreach (var c in itemModel.Components)
                {
                    ItemsComponents ic = new ItemsComponents();
                    ic.ItemId = item.Entity.Id;
                    ic.ComponentId = c.Id;
                    ic.Quantity = c.Quantity;

                    
                    var res = await _context.ItemsComponents.AddAsync(ic);

                }

                _context.SaveChanges();
            }
            catch (Exception e)
            {

                System.Diagnostics.Debug.WriteLine($"ERROR: {e}");
                return BadRequest();
            }


            return Ok();
            //return CreatedAtAction ("GetItem", new { id = item.Entity.Id }, item);
        }

        //This method update an item.
        [HttpPut("{id}")]
        public async Task<ActionResult<Item>> Edit (int id, [FromBody] CreateItemModel itemModel)
         {

            try
            {
                if (!Authenticate())
                {
                    return Unauthorized();
                }

                double totalPrice = 0;
                //Adding the total price of all components and stores it in the Item
                foreach (var c in itemModel.Components)
                {
                    totalPrice += (c.Quantity * c.Price);
                }

                itemModel.Item.Price = Math.Round(totalPrice, 2);

                var relationList = _context.ItemsComponents.Where(i => i.ItemId == id).ToList();

                if(relationList != null)
                {
                    foreach (var r in relationList)
                    {
                        var temp = _context.Remove(r);
                    }
                }
                

                foreach (var component in itemModel.Components)
                {
                    ItemsComponents ic = new ItemsComponents();
                    ic.ItemId = id;
                    ic.ComponentId = component.Id;
                    ic.Quantity = component.Quantity;

                    _context.Add(ic);
                }


                itemModel.Item.Id = id;
                _context.Update(itemModel.Item);

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"ERROR: {e.Message}");
                return BadRequest();
            }
            
            return Ok();
        }

        //This method delete an item.
        [HttpDelete("{id}")]
        public async Task<ActionResult<Item>> Delete (int id)
        {
            try
            {
                if (!Authenticate())
                {
                    return Unauthorized();
                }

                var item = await _context.Items.FindAsync(id);
                if (item == null)
                {
                    return NotFound();
                }
                _context.Items.Remove(item);
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
