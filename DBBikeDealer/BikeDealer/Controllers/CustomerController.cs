using BikeDealer.Dtos.CustomerDto;
using BikeDealer.Models;
using Microsoft.AspNetCore.Mvc;

namespace BikeDealer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly DbbikeDealerContext _dbbikeDealerContext;

        public CustomerController(DbbikeDealerContext dbbikeDealerContext)
        {
            _dbbikeDealerContext = dbbikeDealerContext;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<CustomerDto>> GetAll()
        {

            List<CustomerDto> customersDto = new List<CustomerDto>();
            var customers = _dbbikeDealerContext.Customers.ToList();

            foreach (var customer in customers)
            {
                CustomerDto customerDto = new()
                {
                    CustId = customer.CustId,
                    Name = customer.Name,
                    Number = customer.Number,
                    Email = customer.Email,
                };

                customersDto.Add(customerDto);

            }
            return Ok(customersDto);

        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<CustomerDto> Get(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            var customer = _dbbikeDealerContext.Customers.FirstOrDefault(x => x.CustId == id);
            if(customer == null)
            {
                return NotFound();
            }

            
            CustomerDto customerDto = new()
            {
                CustId = customer.CustId,
                Name = customer.Name,
                Number = customer.Number,
                Email = customer.Email,
            };
            
            //OrderList ----
            
            return Ok(customerDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<CustomerDto> Create(CustomerDto customer)
        {
            CustomerDto CustomerDto = new()
            {
                Name = customer.Name,
                Number = customer.Number,
                Email = customer.Email,
            };
            Customer addCustomer = new()
            {
                Name = CustomerDto.Name,
                Number = CustomerDto.Number,
                Email = CustomerDto.Email,

            };
            _dbbikeDealerContext.Customers.Add(addCustomer);
            _dbbikeDealerContext.SaveChanges();
            return Ok(CustomerDto);
        }

        //Customer Block -----------------

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Edit(CustomerDto customer)
        {
            var editCustomer = _dbbikeDealerContext.Customers.FirstOrDefault(x=> x.CustId == customer.CustId);
            if(editCustomer == null || customer.CustId == 0)
            {
                return NotFound();
            }
            else
            {
                editCustomer.Name = customer.Name;
                editCustomer.Email = customer.Email;
                editCustomer.Number = customer.Number;
            }

            _dbbikeDealerContext.Customers.Update(editCustomer);
            _dbbikeDealerContext.SaveChanges();
            return NoContent();
        }

    }
}
