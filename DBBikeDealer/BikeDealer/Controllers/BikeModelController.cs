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
        public ActionResult <List<BikeModel>> GetAll()
        {
            var bikeModel = from bikemodel in _dbbikeDealerContext.BikeModels
                            join bikecompany in _dbbikeDealerContext.BikeCompanies on bikemodel.BikeCompId equals bikecompany.BikeCompId
                            select new BikeModel
                            {
                                BikeComp = new BikeCompany
                                {
                                    Name = bikecompany.Name,
                                    //BikeCompId= bikecompany.BikeCompId
                                },
                                
                                ModelName = bikemodel.ModelName,
                                ModelYear = bikemodel.ModelYear,
                                Price = bikemodel.Price,
                            };  
            return Ok(bikeModel.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<BikeModel> Get(int id)
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
                                },
                                ModelName = bikemodel.ModelName,
                                ModelYear = bikemodel.ModelYear,
                                Price = bikemodel.Price,
                            });

            return Ok(bikeModel);
        }

        [HttpPost]
        public ActionResult<BikeModel> Add(BikeModel model)
        {

            BikeModel newBikeModel = new BikeModel()
            {
                
                ModelName = model.ModelName,
                ModelYear = model.ModelYear,
                Price = model.Price,
                BikeCompId = model.BikeCompId, //Will use Drop down menu for company Name or search suggestion

            };

            _dbbikeDealerContext.Add(newBikeModel);
            
            _dbbikeDealerContext.SaveChanges();
            return Ok();

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
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, [FromBody] BikeModel model)
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
            }
            _dbbikeDealerContext.Update(editBikeModel);
            _dbbikeDealerContext.SaveChanges();

            return Ok();
        }



    }
}
