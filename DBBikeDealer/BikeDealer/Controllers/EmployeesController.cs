using BikeDealer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BikeDealer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly DbbikeDealerContext _dbbikeDealerContext;

        public EmployeesController(DbbikeDealerContext dbbikeDealerContext)
        {
            _dbbikeDealerContext = dbbikeDealerContext;
        }

        [HttpGet]
        public ActionResult <List<Employee>> Get()
        {
            var allEmp = from employee in _dbbikeDealerContext.Employees
                         join designation in _dbbikeDealerContext.EmployeesDesignations on employee.Designation equals designation.EmpDesignationId
                         select new
                         {
                             id = employee.EmpId,
                             name = employee.Name,
                             salary = employee.Salary,
                             dateOfJoinining = employee.DateOfJoining,
                             dateOfResign = employee.DateOfResign,
                             designation = employee.DesignationNavigation,
                             //designation1 = employee.Designation,
                         }; 

            return Ok(allEmp.ToList());
        }

        [HttpGet("Get/{id}")]
        public ActionResult<Employee> Get(int id)
        {
            if(id == 0)
            {
                return NotFound();
            }
            var employeebyId = _dbbikeDealerContext.Employees.FirstOrDefault(x=> x.EmpId == id);
            if(employeebyId == null)
            {
                return NotFound();
            }

            return Ok(employeebyId);
        }

        [HttpPost("Add")]
        public ActionResult<Employee> Add(Employee employee)
        {
            _dbbikeDealerContext.Employees.Add(employee);
            _dbbikeDealerContext.SaveChanges();
            return Ok();
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var delEmployee = _dbbikeDealerContext.Employees.FirstOrDefault(x=> x.EmpId ==id);
            if(delEmployee == null || id == 0)
            {
                return NotFound();
            }
            _dbbikeDealerContext.Employees.Remove(delEmployee);
            _dbbikeDealerContext.SaveChanges();
            return Ok();
        }

        [HttpPut("Edit/{id}")]
        public IActionResult Edit([FromBody] Employee employee)
        {
            var editEmployee = _dbbikeDealerContext.Employees.FirstOrDefault(x=> x.EmpId == employee.EmpId);
            if(editEmployee == null || employee.EmpId == 0)
            {
                return NotFound();
            }
            else
            {
                editEmployee.Name = employee.Name;
                editEmployee.Salary = employee.Salary;
                editEmployee.Designation = employee.Designation;
                editEmployee.DateOfJoining = employee.DateOfJoining;
                editEmployee.DateOfResign = employee.DateOfResign;
                
            }
            _dbbikeDealerContext.SaveChanges();
            return Ok();
        }

    }
}
