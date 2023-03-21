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
        //Employee Designation
        [HttpGet]
        public ActionResult<List<EmployeesDesignation>> GetAll()
        {
            return Ok(_dbbikeDealerContext.EmployeesDesignations.ToList());
        }

        [HttpGet("Get/{id}")]
        public ActionResult<EmployeesDesignation> Get(int id)
        {
            if(id == 0)
            {
                return NotFound();
            }
            var employeebyId = _dbbikeDealerContext.EmployeesDesignations.FirstOrDefault(x=> x.EmpDesignationId == id);
            if(employeebyId == null)
            {
                return NotFound();
            }
            return Ok(employeebyId);

        }

        [HttpPost("Add")]
        public ActionResult<EmployeesDesignation> Create(EmployeesDesignation employee)
        {
            _dbbikeDealerContext.EmployeesDesignations.Add(employee);
            _dbbikeDealerContext.SaveChanges();
            return Ok();
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var delEmployee = _dbbikeDealerContext.EmployeesDesignations.FirstOrDefault(x=> x.EmpDesignationId ==id);
            if(delEmployee == null || id == 0)
            {
                return NotFound();
            }
            _dbbikeDealerContext.EmployeesDesignations.Remove(delEmployee);
            _dbbikeDealerContext.SaveChanges();
            return Ok();
        }
        [HttpPut("Edit/{id}")]
        public IActionResult Edit([FromBody] EmployeesDesignation employee)
        {
            var editEmployee = _dbbikeDealerContext.EmployeesDesignations.FirstOrDefault(x=> x.EmpDesignationId == employee.EmpDesignationId);
            if(editEmployee == null || employee.EmpDesignationId == 0)
            {
                return NotFound();
            }
            
            editEmployee.Designation = employee.Designation;
            _dbbikeDealerContext.SaveChanges();
            
            return Ok();
        }
        

    }
}
