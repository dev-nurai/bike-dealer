using BikeDealer.Dtos.EmployeeDto;
using BikeDealer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;

namespace BikeDealer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesDesignationController : ControllerBase
    {
        private readonly DbbikeDealerContext _dbbikeDealerContext;
        public EmployeesDesignationController(DbbikeDealerContext dbbikeDealerContext)
        {
            _dbbikeDealerContext = dbbikeDealerContext;
        }


        [HttpGet]
        public ActionResult<List<EmpDesignationDetailsDTO>> GetAll()
        {
            var empDesignationListDto = _dbbikeDealerContext.EmployeesDesignations.ToList();

            List<EmpDesignationDetailsDTO> empDesignationDetailsDTOs = new List<EmpDesignationDetailsDTO>();
            foreach (var empDesignation in empDesignationListDto)
            {
                EmpDesignationDetailsDTO emp = new EmpDesignationDetailsDTO()
                {
                    DesignationId = empDesignation.EmpDesignationId,
                    Designation = empDesignation.Designation,
                };
                empDesignationDetailsDTOs.Add(emp);
            }

            return Ok(empDesignationDetailsDTOs);
        }

        [HttpGet("{id}")]
        public ActionResult<EmpDesignationDetailsDTO> Get(int id)
        {
            if(id == 0)
            {
                return NotFound();
            }
            var employee = _dbbikeDealerContext.EmployeesDesignations.FirstOrDefault(x=> x.EmpDesignationId == id);
            if(employee == null)
            {
                return NotFound();
            }

            EmpDesignationDetailsDTO empDesignationDetails = new EmpDesignationDetailsDTO()
            {
                DesignationId = employee.EmpDesignationId,
                Designation = employee.Designation,
            };

            return Ok(empDesignationDetails);

        }

        [HttpPost]
        public ActionResult<EmpDesignationDetailsDTO> Create(EmpDesignationDetailsDTO employee)
        {
            EmpDesignationDetailsDTO empDesignationDetails = new EmpDesignationDetailsDTO()
            {
                DesignationId = employee.DesignationId,
                Designation = employee.Designation,
            };

            EmployeesDesignation employeesDesignation = new()
            {
                EmpDesignationId = empDesignationDetails.DesignationId,
                Designation = empDesignationDetails.Designation,
            };
            _dbbikeDealerContext.EmployeesDesignations.Add(employeesDesignation);
            _dbbikeDealerContext.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Edit(EmpDesignationDetailsDTO employee)
        {
            var editEmployee = _dbbikeDealerContext.EmployeesDesignations.FirstOrDefault(x=> x.EmpDesignationId == employee.DesignationId);
            if(editEmployee == null || employee.DesignationId == 0)
            {
                return NotFound();
            }
            
            editEmployee.Designation = employee.Designation;
            _dbbikeDealerContext.SaveChanges();
            
            return Ok();
        }
        

    }
}
