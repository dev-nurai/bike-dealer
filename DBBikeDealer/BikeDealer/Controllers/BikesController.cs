using BikeDealer.Dtos.BikeDto;
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
        public ActionResult<List<GetBikeDto>> GetAll()
        {
            var bikeCompanyList = _dbbikeDealerContext.BikeCompanies.ToList();

            List<GetBikeDto> getBikeDtos = new List<GetBikeDto>();

            foreach (var company in bikeCompanyList)
            {
                GetBikeDto getBikeDto = new GetBikeDto();
                getBikeDto.BikeId = company.BikeCompId;
                getBikeDto.BikeName = company.Name;

                getBikeDtos.Add(getBikeDto);
            }

            return Ok(getBikeDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<GetBikeDto> Get(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var bikeCompany = _dbbikeDealerContext.BikeCompanies.FirstOrDefault(x => x.BikeCompId == id);
            if (bikeCompany == null)
            {
                return NotFound();
            }
            GetBikeDto getBikeDto = new()
            {
                BikeId = bikeCompany.BikeCompId,
                BikeName = bikeCompany.Name,
            };

            return Ok(getBikeDto);
        }

        [HttpPost]
        public ActionResult<AddBikeCompanyDto> Create(AddBikeCompanyDto bikeCompany)
        {

            if (bikeCompany == null)
            {
                return BadRequest();
            }
            AddBikeCompanyDto addBikeCompanyDto = new()
            {
                BikeName = bikeCompany.BikeName
            };

            BikeCompany newBikeCompanyName = new()
            {
                Name = addBikeCompanyDto.BikeName
            };
            _dbbikeDealerContext.BikeCompanies.Add(newBikeCompanyName);
            _dbbikeDealerContext.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]

        public IActionResult Delete(int id)
        {
            var delBikeCompany = _dbbikeDealerContext.BikeCompanies.FirstOrDefault(x => x.BikeCompId == id);
            if (id == 0 || delBikeCompany == null)
            {
                return NotFound();
            }
            _dbbikeDealerContext.Remove(delBikeCompany);
            _dbbikeDealerContext.SaveChanges();
            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult Edit(GetBikeDto bikeCompany)
        {
            var editBikeCompany = _dbbikeDealerContext.BikeCompanies.FirstOrDefault(x => x.BikeCompId == bikeCompany.BikeId);
            if (editBikeCompany == null || bikeCompany.BikeId == 0)
            {
                return NotFound();
            }

            editBikeCompany.Name = bikeCompany.BikeName;
            _dbbikeDealerContext.BikeCompanies.Update(editBikeCompany);
            _dbbikeDealerContext.SaveChanges();
            return Ok();
        }

    }
}
