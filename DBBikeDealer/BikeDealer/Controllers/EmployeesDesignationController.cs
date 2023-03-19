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

        [HttpGet("/employeedesitnation/{id}")]
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

        [HttpPost]
        public ActionResult<EmployeesDesignation> Create(EmployeesDesignation employee)
        {
            _dbbikeDealerContext.EmployeesDesignations.Add(employee);
            _dbbikeDealerContext.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var delEmployee = _dbbikeDealerContext.EmployeesDesignations.FirstOrDefault(x=> x.EmpDesignationId ==id);
            if(delEmployee == null || id == 0)
            {
                return NotFound();
            }
            _dbbikeDealerContext.EmployeesDesignations.Remove(delEmployee);
            _dbbikeDealerContext.SaveChanges();
            return NoContent();
        }
        [HttpPut("{id}")]
        public IActionResult Edit(int id, EmployeesDesignation employee)
        {
            var editEmployee = _dbbikeDealerContext.EmployeesDesignations.FirstOrDefault(x=> x.EmpDesignationId ==id);
            if(editEmployee == null || id == 0)
            {
                return NotFound();
            }
            else
            {
                editEmployee.Designation = employee.Designation;
            }
            return NoContent();
        }

        //Employees




    }
}
