using BikeDealer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;

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
        public ActionResult<List<Customer>> Get()
        {
            return Ok(_dbbikeDealerContext.Customers.ToList());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Customer> Get(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            var customerbyId = _dbbikeDealerContext.Customers.FirstOrDefault(x => x.CustId == id);
            if(customerbyId == null)
            {
                return NotFound();
            }
            
            return Ok(customerbyId);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<Customer> Create(Customer customer)
        {
            _dbbikeDealerContext.Customers.Add(customer);
            _dbbikeDealerContext.SaveChanges();
            return Ok(customer);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var delCustomer = _dbbikeDealerContext.Customers.FirstOrDefault(x => x.CustId == id);
            if(id == 0 || delCustomer == null)
            {
                return NotFound();
            }
            _dbbikeDealerContext.Customers.Remove(delCustomer);
            _dbbikeDealerContext.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Edit(int id, Customer customer)
        {
            var editCustomer = _dbbikeDealerContext.Customers.FirstOrDefault(x=> x.CustId == id);
            if(editCustomer == null || id == 0)
            {
                return NotFound();
            }
            else
            {
                editCustomer.Name = customer.Name;
                editCustomer.Email = customer.Email;
                editCustomer.Number = customer.Number;
                editCustomer.DateOfQuotation = customer.DateOfQuotation;
            }

            _dbbikeDealerContext.Customers.Update(editCustomer);
            _dbbikeDealerContext.SaveChanges();
            return NoContent();
        }
        

        

        //---- Quotation

        [HttpGet("/api/quote/get")]
        public ActionResult<Quotation> GetQuote(int id)
        {
            if(id == 0)
            {
                return NotFound();
            }

            var getQuote = _dbbikeDealerContext.Quotations.FirstOrDefault(x=> x.QuoteId == id);
            if(getQuote == null)
            {
                return NotFound();
            }
            return Ok(getQuote);
        }
    }
}
