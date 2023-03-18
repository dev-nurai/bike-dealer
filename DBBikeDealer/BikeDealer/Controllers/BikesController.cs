using BikeDealer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BikeDealer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BikesController : ControllerBase
    {
        private readonly DbbikeDealerContext _dbbikeDealerContext;

        public BikesController(DbbikeDealerContext dbbikeDealerContext)
        {
            _dbbikeDealerContext = dbbikeDealerContext;
        }

        //Bike Company

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<BikeCompany>> Get()
        {
            return Ok(_dbbikeDealerContext.BikeCompanies.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<BikeCompany> Get(int id)
        {
            if(id == 0)
            {
                return NotFound();
            }

            var bikeCompanybyId = _dbbikeDealerContext.BikeCompanies.FirstOrDefault(x=> x.BikeCompId == id);
            if(bikeCompanybyId == null)
            {
                return NotFound();
            }
            return Ok(bikeCompanybyId);
        }

        [HttpPost]
        public ActionResult<BikeCompany> Create(BikeCompany bikeCompany)
        {
            _dbbikeDealerContext.BikeCompanies.Add(bikeCompany);
            _dbbikeDealerContext.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]

        public IActionResult Delete(int id)
        {
            var delBikeCompany = _dbbikeDealerContext.BikeCompanies.FirstOrDefault(x=> x.BikeCompId ==id);
            if(id == 0 || delBikeCompany == null)
            {
                return NotFound();
            }
            _dbbikeDealerContext.Remove(delBikeCompany);
            _dbbikeDealerContext.SaveChanges();
            return NoContent();
        }
        [HttpPut("{id}")]
        public IActionResult Edit(int id, BikeCompany bikeCompany)
        {
            var editBikeCompany = _dbbikeDealerContext.BikeCompanies.FirstOrDefault(x=> x.BikeCompId == id);
            if(editBikeCompany == null || id == 0)
            {
                return NotFound();
            }
            else
            {
                editBikeCompany.Name = bikeCompany.Name;
            }
            _dbbikeDealerContext.BikeCompanies.Update(editBikeCompany);
            _dbbikeDealerContext.SaveChanges();
            return NoContent();
        }
        [HttpPatch("{id}")]
        public IActionResult PartialEdit(int id, BikeCompany bikeCompany)
        {
            var partialEdit = _dbbikeDealerContext.BikeCompanies.FirstOrDefault(x=> x.BikeCompId == id);

            if(bikeCompany.Name != null)
            {
                partialEdit.Name = bikeCompany.Name;
            }
            _dbbikeDealerContext.BikeCompanies.Update(partialEdit);
            _dbbikeDealerContext.SaveChanges();
            return NoContent();
        }

    }
}
