using BikeDealer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BikeDealer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessoriesController : ControllerBase
    {
        private readonly DbbikeDealerContext _dbbikeDealerContext;

        public AccessoriesController(DbbikeDealerContext dbbikeDealerContext)
        {
            _dbbikeDealerContext = dbbikeDealerContext;
        }

        [HttpGet]
        public ActionResult<List<Accessory>> Get()
        {
            return Ok(_dbbikeDealerContext.Accessories.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<Accessory> Get(int id)
        {
            if(id == 0)
            {
                return NotFound();
            }
            var accessorybyId = _dbbikeDealerContext.Accessories.FirstOrDefault(x=> x.AccessoriesId == id);
            if(accessorybyId == null)
            {
                return NotFound();
            }

            return Ok(accessorybyId);
        }

        [HttpPost]
        public ActionResult<Accessory> Add(Accessory accessory)
        {
            _dbbikeDealerContext.Accessories.Add(accessory);
            _dbbikeDealerContext.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var delAccessory = _dbbikeDealerContext.Accessories.FirstOrDefault(x=> x.AccessoriesId == id);
            if(delAccessory == null || id == 0)
            {
                return NotFound();
            }
            _dbbikeDealerContext.Accessories.Remove(delAccessory);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, Accessory accessory)
        {
            var editAccessory = _dbbikeDealerContext.Accessories.FirstOrDefault(x=> x.AccessoriesId == id);
            if(editAccessory == null || id == 0)
            {
                return NotFound();
            }
            else
            {
                editAccessory.Name = accessory.Name;
                editAccessory.Price = accessory.Price;
            }
            _dbbikeDealerContext.Accessories.Update(editAccessory);
            _dbbikeDealerContext.SaveChanges();
            return NoContent();
        }




    }
}
