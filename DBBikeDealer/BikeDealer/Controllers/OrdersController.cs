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
            var orderList = from order in _dbbikeDealerContext.Orders
                            join cust in _dbbikeDealerContext.Customers
                                on order.CustId equals cust.CustId
                            join emp in _dbbikeDealerContext.Employees
                                on order.EmpId equals emp.EmpId
                            join bikemodel in _dbbikeDealerContext.BikeModels
                                on order.BikeModelId equals bikemodel.BikeModelId
                            select new Order
                            {
                                OrderId = order.OrderId,
                                Cust = new Customer
                                {
                                    CustId = cust.CustId,
                                    Name = cust.Name,
                                    Number = cust.Number,
                                },
                                BikeModel = new BikeModel
                                {
                                    BikeComp = bikemodel.BikeComp,
                                    ModelName = bikemodel.ModelName,
                                    ModelYear = bikemodel.ModelYear,
                                },
                                Emp = new Employee
                                {
                                    EmpId = emp.EmpId,
                                    Name = emp.Name,
                                },
                                
                                //Price = order.Price,
                                OrderDate= order.OrderDate,
                                
                            };

            return Ok(orderList.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<Order> Get(int id)
        {
            var orderdetail = from order in _dbbikeDealerContext.Orders
                            join cust in _dbbikeDealerContext.Customers
                                on order.CustId equals cust.CustId
                            join emp in _dbbikeDealerContext.Employees
                                on order.EmpId equals emp.EmpId
                            join bikemodel in _dbbikeDealerContext.BikeModels
                                on order.BikeModelId equals bikemodel.BikeModelId
                            where order.OrderId == id
                            select new Order
                            {
                                OrderId = order.OrderId,
                                Cust = new Customer
                                {
                                    CustId = cust.CustId,
                                    Name = cust.Name,
                                    Number = cust.Number,
                                },
                                BikeModel = new BikeModel
                                {
                                    BikeComp = bikemodel.BikeComp,
                                    ModelName = bikemodel.ModelName,
                                    ModelYear = bikemodel.ModelYear,
                                },
                                Emp = new Employee
                                {
                                    EmpId = emp.EmpId,
                                    Name = emp.Name,
                                },

                                //Price = order.Price,
                                OrderDate = order.OrderDate,

                            };

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



    }
}
