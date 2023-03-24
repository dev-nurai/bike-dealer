using BikeDealer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BikeDealer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderAccessoryController : ControllerBase
    {
        private readonly DbbikeDealerContext _dbbikeDealerContext;

        public OrderAccessoryController(DbbikeDealerContext dbbikeDealerContext)
        {
            _dbbikeDealerContext = dbbikeDealerContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            
            var orderaccessoriesList = _dbbikeDealerContext.OrderAccessories
                .Include(s => s.Accessories)
                .Include(x => x.Order)
                .ToList();
              
            return Ok(orderaccessoriesList);

        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            var orderAccessories = _dbbikeDealerContext.OrderAccessories
                .Include(s => s.Accessories)
                .Include(x => x.Order)
                .Where(z=> z.OrderId == id)
                .ToList();

            return Ok(orderAccessories);
        }

        [HttpPost]
        public IActionResult Add(OrderAccessory orderAccessory)
        {
            if (orderAccessory == null)
            {
                return BadRequest();
            }
            var addAccessories = new OrderAccessory
            {
                OrderId = orderAccessory.OrderId,
                AccessoriesId = orderAccessory.AccessoriesId,
            };
            _dbbikeDealerContext.OrderAccessories.Add(addAccessories);
            _dbbikeDealerContext.SaveChanges();

            return Ok();
        }



    }
}
