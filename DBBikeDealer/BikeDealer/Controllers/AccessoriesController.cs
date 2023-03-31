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
        public ActionResult<List<AccessoryDto>> GetAll()
        {
            List<AccessoryDto> accessoryDtos = new List<AccessoryDto>();

            var accessoriesList = _dbbikeDealerContext.Accessories.ToList();

            foreach (var accessory in accessoriesList)
            {
                AccessoryDto accessoryDto = new AccessoryDto()
                {
                    Id = accessory.AccessoriesId,
                    Name = accessory.Name,
                    Price = accessory.Price,
                };
                accessoryDtos.Add(accessoryDto);
            }

            
            return Ok(accessoriesList);
        }

        [HttpGet("{id}")]
        public ActionResult<AccessoryDto> Get(int id)
        {
            if(id == 0)
            {
                return NotFound();
            }
            var accessory = _dbbikeDealerContext.Accessories.FirstOrDefault(x=> x.AccessoriesId == id);
            if(accessory == null)
            {
                return NotFound();
            }

            AccessoryDto accessoryDto = new AccessoryDto()
            {
                Id = accessory.AccessoriesId,
                Name = accessory.Name,
                Price = accessory.Price,
            };

            return Ok(accessoryDto);
        }

        [HttpPost]
        public ActionResult<AccessoryDto> Add(AccessoryDto accessory)
        {
            AccessoryDto addAccessoriesDto = new AccessoryDto()
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
        public IActionResult Edit(AccessoryDto accessory)
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
