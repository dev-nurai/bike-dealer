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
            return Ok(_dbbikeDealerContext.BikeModels.ToList());
        }

        [HttpGet("{id}")]
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

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var delBikeModel = _dbbikeDealerContext.BikeModels.FirstOrDefault(x => x.BikeModelId == id);
            if(delBikeModel == null || id == 0)
            {
                return NotFound();
            }
            _dbbikeDealerContext.BikeModels.Remove(delBikeModel);
            _dbbikeDealerContext.SaveChanges();
            return NoContent();
        }

        [HttpPut]
        public IActionResult Edit(int id, BikeModel model)
        {
            var editBikeModel = _dbbikeDealerContext.BikeModels.FirstOrDefault(x=> x.BikeModelId == id);
            if(editBikeModel == null || id == 0)
            {
                return NotFound();
            }
            else
            {
                editBikeModel.ModelName = model.ModelName;
                editBikeModel.ModelYear = model.ModelYear;
                editBikeModel.Price = model.Price;
                // compid is a foreign key, will change when the base/master value change
            }

            return NoContent();
        }



    }
}
