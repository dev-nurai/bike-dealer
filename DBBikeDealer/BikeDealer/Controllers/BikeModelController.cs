using BikeDealer.Dtos.BikeDto;
using BikeDealer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BikeDealer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BikeModelController : ControllerBase
    {
        private readonly DbbikeDealerContext _dbbikeDealerContext;

        public BikeModelController(DbbikeDealerContext dbbikeDealerContext)
        {
            _dbbikeDealerContext = dbbikeDealerContext;
        }

        [HttpGet]
        public ActionResult <List<BikeModelDto>> GetAll()
        {
            var bikeModelDetails = (from bikemodel in _dbbikeDealerContext.BikeModels
                            join bikecompany in _dbbikeDealerContext.BikeCompanies
                            on bikemodel.BikeCompId equals bikecompany.BikeCompId
                            select new BikeModelDto
                            {
                                BikeCompanyDetails = new BikeCompanyDto
                                {
                                    BikeName = bikecompany.Name,
                                },
                                BikeModelId = bikemodel.BikeModelId,
                                BikeModel = bikemodel.ModelName,
                                ModelYear = bikemodel.ModelYear,
                                Price = (long)bikemodel.Price,
                            }).ToList();


            return Ok(bikeModelDetails);
        }

        [HttpGet("{id}")]
        public ActionResult<BikeModelDto> Get(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var bikeModelDetail = (from bikemodel in _dbbikeDealerContext.BikeModels
                             join bikecompany in _dbbikeDealerContext.BikeCompanies
                             on bikemodel.BikeCompId equals bikecompany.BikeCompId
                             where bikemodel.BikeModelId == id
                             select new BikeModelDto
                             {
                                 BikeCompanyDetails = new BikeCompanyDto
                                 {
                                     BikeName = bikecompany.Name,
                                 },
                                 BikeModelId = bikemodel.BikeModelId,
                                 BikeModel = bikemodel.ModelName,
                                 ModelYear = bikemodel.ModelYear,
                                 Price = (long)bikemodel.Price,
                             }).FirstOrDefault();

            if (bikeModelDetail == null)
            {
                return NotFound();
            }


            //Number of unit sold is remaining ------------------------------------------------------------------

            return Ok(bikeModelDetail);
        }

        [HttpPost]
        public ActionResult<BikeModelDto> Add(BikeModelDto model)
        {
            BikeModelDto addBikeModelDto = new()
            {
                BikeModel = model.BikeModel,
                ModelYear = model.ModelYear,
                Price = model.Price,
                BikeCompId = model.BikeCompId,
            };

            BikeModel newBikeModel = new BikeModel()
            {
                ModelName = addBikeModelDto.BikeModel,
                ModelYear = addBikeModelDto.ModelYear,
                Price = addBikeModelDto.Price,
                BikeCompId = addBikeModelDto.BikeCompId,
            };

            _dbbikeDealerContext.BikeModels.Add(newBikeModel);
            
            _dbbikeDealerContext.SaveChanges();
            return Ok();

        }


        [HttpPut("{id}")]
        public IActionResult Edit(BikeModelDto model)
        {
            var editBikeModel = _dbbikeDealerContext.BikeModels.FirstOrDefault(x=> x.BikeModelId == model.BikeModelId);
            if(editBikeModel == null || model.BikeModelId == 0)
            {
                return NotFound();
            }
                editBikeModel.BikeCompId = model.BikeCompId;
                editBikeModel.ModelName = model.BikeModel;
                editBikeModel.ModelYear = model.ModelYear;
                editBikeModel.Price = model.Price;
            
            _dbbikeDealerContext.BikeModels.Update(editBikeModel);
            _dbbikeDealerContext.SaveChanges();

            return Ok();
        }



    }
}
