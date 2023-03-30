using BikeDealer.Dtos.AccessoriesDto;
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
        public ActionResult<List<Accessory>> GetAll()
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
        public ActionResult<AddAccessoriesDto> Add(AddAccessoriesDto accessory)
        {
            AddAccessoriesDto addAccessoriesDto = new AddAccessoriesDto()
            {
                Name = accessory.Name,
                Price = accessory.Price,
            };

            Accessory newaccessory = new Accessory()
            {
                Name = addAccessoriesDto.Name,
                Price = addAccessoriesDto.Price,
            };

            _dbbikeDealerContext.Accessories.Add(newaccessory);
            _dbbikeDealerContext.SaveChanges();
            return Ok(accessory);
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
            _dbbikeDealerContext.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Edit(EditAccessoriesDto accessory)
        {
            var editAccessory = _dbbikeDealerContext.Accessories.FirstOrDefault(x=> x.AccessoriesId == accessory.Id);
            if(editAccessory == null || accessory.Id == 0)
            {
                return NotFound();
            }
            
                editAccessory.Name = accessory.Name;
                editAccessory.Price = accessory.Price;
            
            _dbbikeDealerContext.Accessories.Update(editAccessory);
            _dbbikeDealerContext.SaveChanges();
            return Ok();
        }




    }
}
