using BikeDealer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

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
        public ActionResult<List<Employee>> GetAll()
        {
            var allEmp = from employee in _dbbikeDealerContext.Employees
                         join designation in _dbbikeDealerContext.EmployeesDesignations
                         on employee.Designation equals designation.EmpDesignationId
                         select new Employee
                         {
                             EmpId = employee.EmpId,
                             Name = employee.Name,
                             Salary = employee.Salary,
                             DateOfJoining = employee.DateOfJoining,
                             DateOfResign = employee.DateOfResign,
                             DesignationNavigation = new EmployeesDesignation
                             {
                                 EmpDesignationId = designation.EmpDesignationId,
                                 Designation = designation.Designation
                             }
                         };

            return Ok(allEmp.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<Employee> Get(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var employee = (from emp in _dbbikeDealerContext.Employees
                            join designation in _dbbikeDealerContext.EmployeesDesignations
                             on emp.Designation equals designation.EmpDesignationId
                            where emp.EmpId == id
                            select new Employee
                            {
                                EmpId = emp.EmpId,
                                Name = emp.Name,
                                Salary = emp.Salary,
                                DateOfJoining = emp.DateOfJoining,
                                DateOfResign = emp.DateOfResign,
                                DesignationNavigation = new EmployeesDesignation
                                {
                                    EmpDesignationId = designation.EmpDesignationId,
                                    Designation = designation.Designation
                                }
                            }).FirstOrDefault(x => x.EmpId == id);


            return Ok(employee);
        }

        [HttpPost]
        public ActionResult<Employee> Add([FromBody] Employee employee)
        {
            Employee newEmp = new Employee()
            {
                //EmpId = employee.EmpId,
                Name = employee.Name,
                Salary = employee.Salary,
                DateOfJoining = employee.DateOfJoining,
                Designation = employee.Designation,
            };

            _dbbikeDealerContext.Employees.Add(newEmp);

            _dbbikeDealerContext.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var delEmployee = _dbbikeDealerContext.Employees.FirstOrDefault(x => x.EmpId == id);
            if (delEmployee == null || id == 0)
            {
                return NotFound();
            }
            _dbbikeDealerContext.Employees.Remove(delEmployee);
            _dbbikeDealerContext.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, [FromBody] Employee employee)
        {
            var editEmployee = _dbbikeDealerContext.Employees.FirstOrDefault(x => x.EmpId == id);
            if (editEmployee == null || id == 0)
            {
                return NotFound();
            }

            editEmployee.Name = employee.Name;
            editEmployee.Salary = employee.Salary;
            editEmployee.Designation = employee.Designation;
            editEmployee.DateOfJoining = employee.DateOfJoining;
            editEmployee.DateOfResign = employee.DateOfResign;

            //_dbbikeDealerContext.Update(editEmployee);
            _dbbikeDealerContext.SaveChanges();
            return Ok();
        }

    }
}
    