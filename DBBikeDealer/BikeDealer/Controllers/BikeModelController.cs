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
        public ActionResult <List<BikeModel>> Get()
        {
            var bikeModel = from bikemodel in _dbbikeDealerContext.BikeModels
                            join bikecompany in _dbbikeDealerContext.BikeCompanies on bikemodel.BikeCompId equals bikecompany.BikeCompId
                            select new
                            {
                                bikename = bikecompany.Name,
                                modelname = bikemodel.ModelName,
                                bikemodelyear = bikemodel.ModelYear,
                                bikeprice = bikemodel.Price,
                            };
            return Ok(bikeModel.ToList());
        }

        [HttpGet("Get/{id}")]
        public ActionResult<BikeModel> Get(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }

            var bikeModelbyId = _dbbikeDealerContext.BikeModels.First(x=> x.BikeModelId == id);
            if (bikeModelbyId == null)
            {
                return NotFound();
            }

            return Ok(bikeModelbyId);
        }

        [HttpPost("Add")]
        public ActionResult<BikeModel> Add(BikeModel model)
        {
            BikeModel newBikeModel = new BikeModel()
            {
                ModelName = model.ModelName,
                ModelYear = model.ModelYear,
                Price = model.Price,
                BikeCompId = model.BikeCompId,
            };

            _dbbikeDealerContext.Add(newBikeModel);
            _dbbikeDealerContext.SaveChanges();
            return Ok();

        }


        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var delBikeModel = _dbbikeDealerContext.BikeModels.FirstOrDefault(x => x.BikeModelId == id);
            if(delBikeModel == null || id == 0)
            {
                return NotFound();
            }

            _dbbikeDealerContext.BikeModels.Remove(delBikeModel);
            _dbbikeDealerContext.SaveChanges();
            return Ok();
        }

        [HttpPut("Edit")]
        public IActionResult Edit([FromBody] BikeModel model)
        {
            var editBikeModel = _dbbikeDealerContext.BikeModels.FirstOrDefault(x=> x.BikeModelId == model.BikeModelId);
            if(editBikeModel == null || model.BikeModelId == 0)
            {
                return NotFound();
            }
            else
            {
                editBikeModel.ModelName = model.ModelName;
                editBikeModel.ModelYear = model.ModelYear;
                editBikeModel.Price = model.Price;
                editBikeModel.BikeComp = model.BikeComp;
            }
            _dbbikeDealerContext.Update(editBikeModel);
            _dbbikeDealerContext.SaveChanges();

            return Ok();
        }



    }
}
