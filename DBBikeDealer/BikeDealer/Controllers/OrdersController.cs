using BikeDealer.Dtos;
using BikeDealer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public ActionResult<List<OrderDto>> Get()
        {
            // Need Cust name, Emp Name, Bike Details, Price, OrderDate, Accessory

            var orderList = _dbbikeDealerContext.Orders
                .Include(x => x.Cust)
                .Include(v => v.BikeModel)
                    .ThenInclude(z=> z.BikeComp)
                .Include(w => w.StatusNavigation)
                .Include(v => v.UpdatedByNavigation)
                .ToList();

            List<OrderDto> orderDtos = new();

            foreach( var order in orderList)
            {
                OrderDto orderDto = new();

                orderDto.OrderId = order.OrderId;
                orderDto.OrderBy = order.Cust.Name;
                orderDto.BikeModel = order.BikeModel.ModelName;
                orderDto.SoldBy = order.Emp.Name;
                orderDto.OrderDate = order.OrderDate;
                orderDto.Price = order.Price;
                
                orderDtos.Add(orderDto);
            }
    
            return Ok(orderDtos);
            
        }

        [HttpGet("{id}")]
        public ActionResult<    Order> Get(int id)
        {
            var orderdetail = (from order in _dbbikeDealerContext.Orders
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
                                       //CustId = cust.CustId,
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
                                   Status = order.Status,
                                   Remarks = order.Remarks,
                                   UpdatedBy = order.UpdatedBy,
                                   StatusNavigation = order.StatusNavigation,

                               }).FirstOrDefault();

            return Ok(orderdetail);
        }

        //Order Accessory - Edit
        [HttpPost("OrderAccessory")]
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
