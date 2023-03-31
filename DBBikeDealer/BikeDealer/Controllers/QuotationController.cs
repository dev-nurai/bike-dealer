using BikeDealer.Dtos.QuotationDto;
using BikeDealer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
        public ActionResult<List<QuotationDto>> GetAll()
        {
            var quotation = (from quote in _dbbikeDealerContext.Quotations
                             join cust in _dbbikeDealerContext.Customers
                                 on quote.CustId equals cust.CustId
                             join emp in _dbbikeDealerContext.Employees
                                 on quote.EmpId equals emp.EmpId
                             join bikemodel in _dbbikeDealerContext.BikeModels
                                 on quote.BikeModelId equals bikemodel.BikeModelId
                             select new QuotationDto
                             {
                                 QuoteId = quote.QuoteId,
                                 QuoteDetails = quote.QuoteDetails,
                                 QuotationDate = quote.QuotationDate,
                                 CustomerName = cust.Name,
                                 EmployeeName = emp.Name,
                                 BikeDetails = new BikeDetailsDto
                                 {
                                     BikeName = bikemodel.BikeComp.Name,
                                     BikeModel = bikemodel.ModelName,
                                     Price = bikemodel.Price,
                                 }

                             }).ToList();

            return Ok(quotation);
        }

        [HttpGet("{id}")]
        public ActionResult<QuotationDto> Get(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var quotation = (from quote in _dbbikeDealerContext.Quotations
                            join cust in _dbbikeDealerContext.Customers
                                on quote.CustId equals cust.CustId
                            join emp in _dbbikeDealerContext.Employees
                                on quote.EmpId equals emp.EmpId
                            join bikemodel in _dbbikeDealerContext.BikeModels
                                on quote.BikeModelId equals bikemodel.BikeModelId
                            join updateby in _dbbikeDealerContext.Employees
                                on quote.UpdatedBy equals updateby.EmpId
                            where quote.QuoteId == id
                            select new QuotationDto
                            {
                                QuoteId = quote.QuoteId,
                                QuoteDetails = quote.QuoteDetails,
                                QuotationDate = quote.QuotationDate,
                                Remarks = quote.Remarks,
                                UpdateDate = quote.UpdateDate,

                                CustomerName = cust.Name,
                                EmployeeName = emp.Name,
                                UpdateBy = updateby.Name,

                                BikeDetails = new BikeDetailsDto
                                {
                                    BikeName = bikemodel.BikeComp.Name,
                                    BikeModel = bikemodel.ModelName,
                                    Price = bikemodel.Price,
                                }


                            }).FirstOrDefault();
            if(quotation == null)
            {
                return NotFound();
            }

            return Ok(quotation);

        }

        [HttpPost]
        public ActionResult Add(AddQuotationDto quotation)
        {

            AddQuotationDto quote = new()
            {
                CustomerId = quotation.CustomerId,
                EmployeeId = quotation.EmployeeId,
                bikeModelId = quotation.bikeModelId,
                QuoteDetails = quotation.QuoteDetails,
                QuotationDate = quotation.QuotationDate,
            };

            Quotation newQuotation = new Quotation()
            {
                CustId = quote.CustomerId,
                EmpId = quote.EmployeeId,
                BikeModelId = quote.bikeModelId,
                QuotationDate = quote.QuotationDate,
                QuoteDetails = quote.QuoteDetails,
            };


            _dbbikeDealerContext.Quotations.Add(newQuotation);
            _dbbikeDealerContext.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Edit(AddQuotationDto quotation)
        {
            var editQuotation = _dbbikeDealerContext.Quotations.FirstOrDefault(x => x.QuoteId == quotation.QuoteId);
            if (editQuotation == null || quotation.QuoteId == 0)
            {
                return NotFound();
            }

            editQuotation.QuoteDetails = quotation.QuoteDetails;
            editQuotation.BikeModelId = quotation.bikeModelId;
            editQuotation.UpdateDate = quotation.UpdateDate;
            editQuotation.Remarks = quotation.Remarks;
            editQuotation.UpdatedBy = quotation.UpdateBy;

            _dbbikeDealerContext.Quotations.Update(editQuotation);
            _dbbikeDealerContext.SaveChanges();
            return Ok();
        }



    }
}
