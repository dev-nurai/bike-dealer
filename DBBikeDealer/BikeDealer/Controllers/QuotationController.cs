using BikeDealer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BikeDealer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotationController : ControllerBase
    {
        private readonly DbbikeDealerContext _dbbikeDealerContext;

        public QuotationController(DbbikeDealerContext dbbikeDealerContext)
        {
            _dbbikeDealerContext = dbbikeDealerContext;
        }

        [HttpGet]
        public ActionResult<List<Quotation>> Get()
        {
            return Ok(_dbbikeDealerContext.Quotations.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<Quotation> Get(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            var quotationbyId = _dbbikeDealerContext.Quotations.FirstOrDefault(x=> x.QuoteId== id);
            if(quotationbyId == null)
            {
                return NotFound();
            }
            return Ok(quotationbyId);

        }

        [HttpPost]
        public ActionResult Add(Quotation quotation)
        {
            _dbbikeDealerContext.Quotations.Add(quotation);
            _dbbikeDealerContext.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var delQuotation = _dbbikeDealerContext.Quotations.FirstOrDefault(x=> x.QuoteId== id);
            if(delQuotation == null)
            {
                return NotFound();
            }

            _dbbikeDealerContext.Quotations.Remove(delQuotation);
            _dbbikeDealerContext.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, Quotation quotation)
        {
            var editQuotation = _dbbikeDealerContext.Quotations.FirstOrDefault(x=> x.QuoteId== id);
            if (editQuotation == null || id == 0)
            {
                return BadRequest();
            }
            else
            {
                editQuotation.QuoteDetails = quotation.QuoteDetails;
                editQuotation.QuotationDate = quotation.QuotationDate;
                //custid, empid, bikemodelid are foreign key

            }
            _dbbikeDealerContext.Quotations.Update(editQuotation);
            _dbbikeDealerContext.SaveChanges();
            return NoContent();
        }


    }
}
