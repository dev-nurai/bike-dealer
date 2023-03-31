using BikeDealer.Dtos.OrderDto;
using BikeDealer.Dtos.QuotationDto;
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

            var orderList = (from order in _dbbikeDealerContext.Orders
                              join cust in _dbbikeDealerContext.Customers
                                  on order.CustId equals cust.CustId
                              join emp in _dbbikeDealerContext.Employees
                                  on order.EmpId equals emp.EmpId
                              join bikemodel in _dbbikeDealerContext.BikeModels
                                  on order.BikeModelId equals bikemodel.BikeModelId
                              select new OrderDto
                              {
                                  OrderId = order.OrderId,
                                  CustomerName = cust.Name,
                                  EmployeeName = emp.Name,
                                  BikesDetailsDto = new BikeDetailsDto
                                  {
                                      BikeName = bikemodel.BikeComp.Name,
                                      BikeModel = bikemodel.ModelName,
                                      Price = bikemodel.Price,
                                  },
                                  OrderDate = order.OrderDate,

                              }).ToList();


            return Ok(orderList);

        }

        [HttpGet("{id}")]
        public ActionResult<GetOrderDto> Get(int id)
        {
            var orderdetail = (from order in _dbbikeDealerContext.Orders
                               join cust in _dbbikeDealerContext.Customers
                                   on order.CustId equals cust.CustId
                               join emp in _dbbikeDealerContext.Employees
                                   on order.EmpId equals emp.EmpId
                               join bikemodel in _dbbikeDealerContext.BikeModels
                                   on order.BikeModelId equals bikemodel.BikeModelId
                               join updateby in _dbbikeDealerContext.Employees
                                on order.UpdatedBy equals updateby.EmpId
                               join orderStatus in _dbbikeDealerContext.Statuses
                                   on order.Status equals orderStatus.Id
                               where order.OrderId == id
                               select new GetOrderDto
                               {
                                   OrderId = order.OrderId,
                                   CustomerName = cust.Name,
                                   EmployeeName = emp.Name,
                                   BikesDetailsDto = new BikeDetailsDto
                                   {
                                       BikeName = bikemodel.BikeComp.Name,
                                       BikeModel = bikemodel.ModelName,
                                       Price = bikemodel.Price,
                                   },
                                   OrderStatus = orderStatus.Status1,
                                   UpdateDate = order.UpdatedDate,
                                   Remarks = order.Remarks,
                                   UpdatedBy = updateby.Name

                               }).FirstOrDefault();
            
            if (orderdetail == null)
            {
                return NotFound();
            };



            // get all accessories by joining from accessories and orderaccessories table and filter on order id. convert it into list.

            var orderAccessories = (from orderAccessory in _dbbikeDealerContext.OrderAccessories
                                    join accessory in _dbbikeDealerContext.Accessories
                                         on orderAccessory.AccessoriesId equals accessory.AccessoriesId
                                    where orderAccessory.OrderId == id
                                    select new Accessory
                                    {
                                        AccessoriesId = (int)orderAccessory.AccessoriesId,
                                        Name = orderAccessory.Accessories.Name,
                                        Price = orderAccessory.Accessories.Price

                                    }).ToList();


            //  orderDetal.Accessories = vaarObjectName;

            orderdetail.Accessories = orderAccessories;

            return Ok(orderdetail);

        }

        [HttpPost]
        public IActionResult Add(GetOrderDto order)
        {
            GetOrderDto addOrderDto = new()
            {
                CustomerId = order.CustomerId,
                EmployeeId = order.EmployeeId,
                BikeModelId = order.BikeModelId,
                Price = order.Price,
                OrderDate = order.OrderDate,
                OrderStatusId = order.OrderStatusId,

            };
            Order newOrder = new Order()
            {
                CustId = addOrderDto.CustomerId,
                EmpId = addOrderDto.EmployeeId,
                BikeModelId = addOrderDto.BikeModelId,
                Price = addOrderDto.Price,
                OrderDate = addOrderDto.OrderDate,
                Status = addOrderDto.OrderStatusId,
            };

            _dbbikeDealerContext.Orders.Add(newOrder);
            _dbbikeDealerContext.SaveChanges();

            return Ok(newOrder);

        }

        [HttpPut]
        public IActionResult Update(GetOrderDto order)
        {
            if(order.OrderId == 0)
            {
                return BadRequest();
            };

            var updateOrder = _dbbikeDealerContext.Orders.FirstOrDefault(x=> x.OrderId == order.OrderId);
            if(updateOrder == null)
            {
                return NotFound();
            }

            updateOrder.Status = order.OrderStatusId;
            updateOrder.UpdatedDate = order.UpdateDate;
            updateOrder.Remarks = order.Remarks;
            updateOrder.UpdatedBy = order.UpdatedbyId;

            _dbbikeDealerContext.Orders.Update(updateOrder);
            _dbbikeDealerContext.SaveChanges();
            return Ok();
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
                AccessoriesId = orderAccessory.AccessoriesId
            };
            _dbbikeDealerContext.OrderAccessories.Add(addAccessories);
            _dbbikeDealerContext.SaveChanges();

            return Ok(addAccessories);
        }


    }
}
