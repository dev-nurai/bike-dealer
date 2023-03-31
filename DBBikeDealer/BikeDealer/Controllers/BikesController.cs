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
        public ActionResult<List<BikeCompanyDto>> GetAll()
        {
            var bikeCompanyList = _dbbikeDealerContext.BikeCompanies.ToList();

            List<BikeCompanyDto> bikeCompDtos = new List<BikeCompanyDto>();

            foreach (var company in bikeCompanyList)
            {
                BikeCompanyDto bikeCompDto = new BikeCompanyDto()
                {
                    BikeId = company.BikeCompId,
                    BikeName = company.Name,
                };

                bikeCompDtos.Add(bikeCompDto);
            }

            return Ok(bikeCompDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<BikeCompanyDto> Get(int id)
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
            BikeCompanyDto bikeCompDto = new()
            {
                BikeId = bikeCompany.BikeCompId,
                BikeName = bikeCompany.Name,
            };

            return Ok(bikeCompDto);
        }

        [HttpPost]
        public ActionResult<BikeCompanyDto> Create(BikeCompanyDto bikeCompany)
        {

            if (bikeCompany == null)
            {
                return BadRequest();
            }
            BikeCompanyDto addBikeCompanyDto = new()
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
        public IActionResult Edit(BikeCompanyDto bikeCompany)
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
