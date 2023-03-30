using BikeDealer.Dtos.OrderDto;
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

            //var orderList = _dbbikeDealerContext.Orders
            //    .Include(x => x.Cust)
            //    .Include(v => v.BikeModel)
            //        .ThenInclude(z => z.BikeComp)
            //    .Include(w => w.StatusNavigation)
            //    .Include(v => v.UpdatedByNavigation)
            //    .ToList();


            var orderList = from order in _dbbikeDealerContext.Orders
                              join cust in _dbbikeDealerContext.Customers
                                  on order.CustId equals cust.CustId
                              join emp in _dbbikeDealerContext.Employees
                                  on order.EmpId equals emp.EmpId
                              join bikemodel in _dbbikeDealerContext.BikeModels
                                  on order.BikeModelId equals bikemodel.BikeModelId
                              //where order.OrderId == id
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

                                  Price = order.Price,
                                  OrderDate = order.OrderDate,
                                  Remarks = order.Remarks,
                                  UpdatedBy = order.UpdatedBy,

                              };

            List<OrderDto> ordersDTO = new();

            foreach (var order in orderList)
            {
                Bike getBikeDetails = new()
                {
                    BikeName = order.BikeModel.BikeComp.Name,
                    BikeModel = order.BikeModel.ModelName,
                    Price = order.Price,
                };
                OrderDto orderDto = new()
                {
                    OrderId = order.OrderId,
                    CustomerName = order.Cust.Name,
                    EmployeeName = order.Emp.Name,
                    OrderDate = order.OrderDate,
                    BikeDetails = getBikeDetails,
                };

                ordersDTO.Add(orderDto);

            };

            return Ok(ordersDTO);


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
                               join orderStatus in _dbbikeDealerContext.Statuses
                                   on order.Status equals orderStatus.Id
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

                                   Price = order.Price,
                                   OrderDate = order.OrderDate,
                                   Remarks = order.Remarks,
                                   UpdatedBy = order.UpdatedBy,
                                   StatusNavigation = new Status
                                   {
                                       Id = orderStatus.Id,
                                       Status1 = orderStatus.Status1
                                   }


                               }).FirstOrDefault();

            if (orderdetail == null)
            {
                return NotFound();
            };

            OrderBikeDetails orderBikeDetails = new OrderBikeDetails()
            {
                BikeName = orderdetail.BikeModel.BikeComp.Name,
                BikeModel = orderdetail.BikeModel.ModelName,
                Price = orderdetail.Price,
            };

            GetOrderDto getOrderDto = new GetOrderDto()
            {
                OrderId = orderdetail.OrderId,
                CustomerName = orderdetail.Cust.Name,
                EmployeeName = orderdetail.Emp.Name,
                OrderDate = orderdetail.OrderDate,
                BikeDetails = orderBikeDetails,
                OrderStatus = orderdetail.StatusNavigation.Status1,
                UpdateDate = orderdetail.UpdatedDate,
                Remarks = orderdetail.Remarks,
                UpdatedBy = orderdetail.Emp.Name,

                //Accessories list??
            };

            return Ok(getOrderDto);
        }

        [HttpPost]
        public IActionResult Add(AddOrderDto order)
        {
            AddOrderDto addOrderDto = new AddOrderDto()
            {
                CustomerId = order.CustomerId,
                EmployeeId = order.EmployeeId,
                BikeModelId = order.BikeModelId,
                Price = order.Price,
                OrderDate = order.OrderDate,

            };
            Order newOrder = new Order()
            {
                CustId = addOrderDto.CustomerId,
                EmpId = addOrderDto.EmployeeId,
                BikeModelId = addOrderDto.BikeModelId,
                Price = addOrderDto.Price,
                OrderDate = addOrderDto.OrderDate,
            };

            _dbbikeDealerContext.Orders.Add(newOrder);
            _dbbikeDealerContext.SaveChanges();

            return Ok(newOrder);

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
