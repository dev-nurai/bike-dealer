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
        public ActionResult <List<GetBikeModelDto>> GetAll()
        {
            var bikeModel = from bikemodel in _dbbikeDealerContext.BikeModels
                            join bikecompany in _dbbikeDealerContext.BikeCompanies
                            on bikemodel.BikeCompId equals bikecompany.BikeCompId
                            select new BikeModel
                            {
                                BikeComp = new BikeCompany
                                {
                                    Name = bikecompany.Name,
                                    BikeCompId= bikecompany.BikeCompId
                                },
                                BikeModelId = bikemodel.BikeModelId,
                                ModelName = bikemodel.ModelName,
                                ModelYear = bikemodel.ModelYear,
                                Price = bikemodel.Price,
                            };

            List<GetBikeModelDto> getBikeDtos = new List<GetBikeModelDto>();

            foreach (var model in bikeModel)
            {
                GetBikeModelDto getBikeModelDto = new()
                {
                    Id = model.BikeModelId,
                    BikeName = model.BikeComp.Name,
                    BikeModel = model.ModelName,
                    ModelYear = (short)model.ModelYear,
                    Price = (long)model.Price,
                };
                getBikeDtos.Add(getBikeModelDto);
            };

            return Ok(getBikeDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<GetBikeModelDto> Get(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }

            var bikeModel = (from bikemodel in _dbbikeDealerContext.BikeModels
                            join bikecompany in _dbbikeDealerContext.BikeCompanies
                            on bikemodel.BikeCompId equals bikecompany.BikeCompId
                            where bikemodel.BikeModelId == id
                            select new BikeModel
                            {
                                BikeComp = new BikeCompany
                                {
                                    Name = bikecompany.Name,
                                    BikeCompId = bikecompany.BikeCompId
                                },
                                BikeModelId = bikemodel.BikeModelId,
                                ModelName = bikemodel.ModelName,
                                ModelYear = bikemodel.ModelYear,
                                Price = bikemodel.Price,
                            }).FirstOrDefault();

            if(bikeModel == null)
            {
                return NotFound();
            }

            GetBikeModelDto getBikeModelDto = new GetBikeModelDto()
            {
                Id = bikeModel.BikeModelId,
                BikeName = bikeModel.BikeComp.Name,
                BikeModel = bikeModel.ModelName,
                ModelYear = (short)bikeModel.ModelYear,
                Price = (long)bikeModel.Price,
            };
            
            //Number of unit sold is remaining ------------------------------------------------------------------

            return Ok(getBikeModelDto);
        }

        [HttpPost]
        public ActionResult<AddBikeModelDto> Add(AddBikeModelDto model)
        {
            AddBikeModelDto addBikeModelDto = new()
            {
                BikeComplId = model.BikeComplId,
                BikeModelName = model.BikeModelName,
                ModelYear = (short)model.ModelYear,
                Price = model.Price,
            };

            BikeModel newBikeModel = new BikeModel()
            {
                
                ModelName = addBikeModelDto.BikeModelName,
                ModelYear = addBikeModelDto.ModelYear,
                Price = addBikeModelDto.Price,
                BikeCompId = addBikeModelDto.BikeComplId, //Will use Drop down menu for company Name or search suggestion

            };

            _dbbikeDealerContext.BikeModels.Add(newBikeModel);
            
            _dbbikeDealerContext.SaveChanges();
            return Ok();

        }


        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    var delBikeModel = _dbbikeDealerContext.BikeModels.FirstOrDefault(x => x.BikeModelId == id);
        //    if(delBikeModel == null || id == 0)
        //    {
        //        return NotFound();
        //    }

        //    _dbbikeDealerContext.BikeModels.Remove(delBikeModel);
        //    _dbbikeDealerContext.SaveChanges();
        //    return Ok();
        //}

        [HttpPut("{id}")]
        public IActionResult Edit(EditBikeModelDto model)
        {
            var editBikeModel = _dbbikeDealerContext.BikeModels.FirstOrDefault(x=> x.BikeModelId == model.BikeModelId);
            if(editBikeModel == null || model.BikeModelId == 0)
            {
                return NotFound();
            }
                editBikeModel.BikeCompId = model.BikeCompId;
                editBikeModel.ModelName = model.BikeModelName;
                editBikeModel.ModelYear = model.ModelYear;
                editBikeModel.Price = model.Price;
            
            _dbbikeDealerContext.Update(editBikeModel);
            _dbbikeDealerContext.SaveChanges();

            return Ok();
        }



    }
}
