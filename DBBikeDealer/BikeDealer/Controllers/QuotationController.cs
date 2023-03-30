using BikeDealer.Dtos.QuotationDto;
using BikeDealer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
        public ActionResult<List<GetQuotationDto>> GetAll()
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
            List<GetQuotationDto> getQuotationDtos = new List<GetQuotationDto>();

            foreach (var quote in quotation)
            {
                BikeDetails bikeDetails = new BikeDetails()
                {
                    BikeName = quote.BikeModel.BikeComp.Name,
                    BikeModel = quote.BikeModel.ModelName,
                    Price = quote.BikeModel.Price,
                };
                GetQuotationDto getQuotationDto = new GetQuotationDto()
                {
                    QuoteId = quote.QuoteId,
                    CustomerName = quote.Cust.Name,
                    EmployeeName = quote.Emp.Name,
                    QuoteDetails = quote.QuoteDetails,
                    QuotationDate = quote.QuotationDate,
                    BikeDetails = bikeDetails,
                };
                getQuotationDtos.Add(getQuotationDto);
            }
            

            return Ok(getQuotationDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<GetQuotationbyIdDto> Get(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var quotation = (from quote in _dbbikeDealerContext.Quotations
                            join cust in _dbbikeDealerContext.Customers on quote.CustId equals cust.CustId
                            join emp in _dbbikeDealerContext.Employees on quote.EmpId equals emp.EmpId
                            join bikemodel in _dbbikeDealerContext.BikeModels on quote.BikeModelId equals bikemodel.BikeModelId
                            where quote.QuoteId == id
                            select new Quotation
                            {
                                QuoteId = quote.QuoteId,
                                QuoteDetails = quote.QuoteDetails,
                                QuotationDate = quote.QuotationDate,
                                Remarks = quote.Remarks,
                                UpdateDate = quote.UpdateDate,
                                
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
                                

                            }).FirstOrDefault();
            if(quotation == null)
            {
                return NotFound();
            }
            BikeDetail bikeDetail = new BikeDetail()
            {
                BikeName = quotation.BikeModel.BikeComp.Name,
                BikeModel = quotation.BikeModel.ModelName,
                Price = quotation.BikeModel.Price,
            };

            GetQuotationbyIdDto getQuotationbyIdDto = new GetQuotationbyIdDto()
            {
                QuoteId = quotation.QuoteId,
                CustomerName = quotation.Cust.Name,
                CustomerNumber = quotation.Cust.Number,
                EmployeeName = quotation.Emp.Name,
                BikeDetail = bikeDetail,
                QuotationDate = quotation.QuotationDate,
                QuoteDetails = quotation.QuoteDetails,
                UpdateDate = quotation.UpdateDate,
                Remarks = quotation.Remarks,
                UpdatedBy = quotation.Emp.Name,

            };

            return Ok(getQuotationbyIdDto);

        }

        [HttpPost]
        public ActionResult Add(AddQuotationDto quotation)
        {

            AddQuotationDto quote = new AddQuotationDto()
            {
                CustomerId = quotation.CustomerId,
                EmployeeId = quotation.EmployeeId,
                bikeModel = quotation.bikeModel,
                QuoteDetails = quotation.QuoteDetails,
                QuotationDate = quotation.QuotationDate,
            };

            Quotation newQuotation = new Quotation()
            {
                CustId = quote.CustomerId,
                EmpId = quote.EmployeeId,
                BikeModelId = quote.bikeModel,
                QuotationDate = quote.QuotationDate,
                QuoteDetails = quote.QuoteDetails,
            };


            _dbbikeDealerContext.Quotations.Add(newQuotation);
            _dbbikeDealerContext.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Edit(EditQuotesDto quotation)
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
            editQuotation.UpdatedBy = quotation.UpdatedBy;

            _dbbikeDealerContext.Quotations.Update(editQuotation);
            _dbbikeDealerContext.SaveChanges();
            return Ok();
        }



    }
}
