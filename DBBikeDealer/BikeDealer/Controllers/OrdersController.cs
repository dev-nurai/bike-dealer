using BikeDealer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BikeDealer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly DbbikeDealerContext _dbbikeDealerContext;

        public OrdersController(DbbikeDealerContext dbbikeDealerContext)
        {
            _dbbikeDealerContext = dbbikeDealerContext;
        }

        [HttpGet]
        public ActionResult<List<Order>> Get()
        {
            return Ok(_dbbikeDealerContext.Orders.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<Order> Get(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            var getOrder = _dbbikeDealerContext.Orders.FirstOrDefault(x=> x.OrderId == id);
            if(getOrder == null)
            {
                return NotFound();
            }
            return Ok(getOrder);

        }
        [HttpPost]
        public ActionResult Add([FromBody] Order order)
        {
            _dbbikeDealerContext.Orders.Add(order);
            _dbbikeDealerContext.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            var delOrder = _dbbikeDealerContext.Orders.FirstOrDefault(x=> x.OrderId == id);
            if(delOrder == null)
            {
                return NotFound();
            }
            _dbbikeDealerContext.Remove(delOrder);
            _dbbikeDealerContext.SaveChanges();
            return Ok();

        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, Order order)
        {
            var editOrder = _dbbikeDealerContext.Orders.FirstOrDefault(x=> x.OrderId == id);
            if (editOrder == null || id == 0)
            {
                return BadRequest();
            }
            else
            {
                editOrder.Price = order.Price;
                editOrder.OrderDate = order.OrderDate;
            }
            _dbbikeDealerContext.Orders.Update(editOrder);
            _dbbikeDealerContext.SaveChanges();
            return NoContent();
        }

        [HttpGet("/getOrderAccessories/{id}")]
        public ActionResult<OrderAccessory> getAccessories(int id)
        {
            if(id == 0) 
            { 
                return BadRequest(); 
            }
            var getAccess = _dbbikeDealerContext.OrderAccessories.FirstOrDefault(x=> x.OrderId == id);
            return Ok(getAccess);

        }

    }
}
