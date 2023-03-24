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
        public ActionResult<List<Quotation>> GetAll()
        {
            var quotation = from quote in _dbbikeDealerContext.Quotations
                            join cust in _dbbikeDealerContext.Customers on quote.CustId equals cust.CustId
                            join emp in _dbbikeDealerContext.Employees on quote.EmpId equals emp.EmpId
                            join bikemodel in _dbbikeDealerContext.BikeModels on quote.BikeModelId equals bikemodel.BikeModelId
                            select new Quotation
                            {
                                QuoteId = quote.QuoteId,
                                QuoteDetails = quote.QuoteDetails,
                                QuotationDate = quote.QuotationDate,

                                Cust = new Customer
                                {
                                    Name = cust.Name,
                                    Number = cust.Number,
                                },
                                Emp = new Employee
                                {
                                    Name = emp.Name,
                                    EmpId = emp.EmpId,
                                },
                                BikeModel = new BikeModel
                                {
                                    ModelName = bikemodel.ModelName,
                                    BikeComp = bikemodel.BikeComp,
                                    ModelYear = bikemodel.ModelYear,
                                    Price = bikemodel.Price,
                                }

                            };

            return Ok(quotation.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<Quotation> Get(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var quotation = from quote in _dbbikeDealerContext.Quotations
                            join cust in _dbbikeDealerContext.Customers on quote.CustId equals cust.CustId
                            join emp in _dbbikeDealerContext.Employees on quote.EmpId equals emp.EmpId
                            join bikemodel in _dbbikeDealerContext.BikeModels on quote.BikeModelId equals bikemodel.BikeModelId
                            where quote.QuoteId == id
                            select new Quotation
                            {
                                QuoteId = quote.QuoteId,
                                QuoteDetails = quote.QuoteDetails,
                                QuotationDate = quote.QuotationDate,

                                Cust = new Customer
                                {
                                    Name = cust.Name,
                                    Number = cust.Number,
                                },
                                Emp = new Employee
                                {
                                    Name = emp.Name,
                                    EmpId = emp.EmpId,
                                },
                                BikeModel = new BikeModel
                                {
                                    ModelName = bikemodel.ModelName,
                                    BikeComp = bikemodel.BikeComp,
                                    ModelYear = bikemodel.ModelYear,
                                    Price = bikemodel.Price,
                                }

                            };

            return Ok(quotation);

        }

        [HttpPost]
        public ActionResult Add(Quotation quotation)
        {

            Quotation quote = new Quotation()
            {
                QuoteDetails = quotation.QuoteDetails,
                QuotationDate = quotation.QuotationDate,
                CustId = quotation.CustId,
                EmpId = quotation.EmpId,
                BikeModelId = quotation.BikeModelId
            };

            _dbbikeDealerContext.Quotations.Add(quote);
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
            var delQuotation = _dbbikeDealerContext.Quotations.FirstOrDefault(x => x.QuoteId == id);
            if (delQuotation == null)
            {
                return NotFound();
            }

            _dbbikeDealerContext.Quotations.Remove(delQuotation);
            _dbbikeDealerContext.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, Quotation quotation)
        {
            var editQuotation = _dbbikeDealerContext.Quotations.FirstOrDefault(x => x.QuoteId == id);
            if (editQuotation == null || id == 0)
            {
                return NotFound();
            }

            editQuotation.QuoteDetails = quotation.QuoteDetails;
            editQuotation.QuotationDate = quotation.QuotationDate;
            editQuotation.CustId = quotation.CustId;
            editQuotation.EmpId = quotation.EmpId;
            editQuotation.BikeModelId = quotation.BikeModelId;

            _dbbikeDealerContext.Quotations.Update(editQuotation);
            _dbbikeDealerContext.SaveChanges();
            return Ok();
        }


    }
}
