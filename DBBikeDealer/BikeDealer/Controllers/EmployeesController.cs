using BikeDealer.Dtos.EmployeeDto;
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
        public ActionResult<List<EmployeesDto>> GetAll()
        {
            var allEmp = (from employee in _dbbikeDealerContext.Employees
                          join designation in _dbbikeDealerContext.EmployeesDesignations
                          on employee.Designation equals designation.EmpDesignationId
                          select new EmployeesDto
                          {
                              EmpId = employee.EmpId,
                              Name = employee.Name,
                              Salary = employee.Salary,
                              DateOfJoining = employee.DateOfJoining,
                              DateOfResign = employee.DateOfResign,
                              DesignationDetails = new EmpDesignationDetailsDTO
                              {
                                  DesignationId = designation.EmpDesignationId,
                                  Designation = designation.Designation
                              }
                          }).ToList();

            return Ok(allEmp);

        }

        [HttpGet("{id}")]
        public ActionResult<EmployeesDto> Get(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var employee = (from emp in _dbbikeDealerContext.Employees
                            join designation in _dbbikeDealerContext.EmployeesDesignations
                             on emp.Designation equals designation.EmpDesignationId
                            where emp.EmpId == id
                            select new EmployeesDto
                            {
                                EmpId = emp.EmpId,
                                Name = emp.Name,
                                Salary = emp.Salary,
                                DateOfJoining = emp.DateOfJoining,
                                DateOfResign = emp.DateOfResign,
                                DesignationDetails = new EmpDesignationDetailsDTO
                                {
                                    Designation = designation.Designation
                                }

                            }).FirstOrDefault();

            if (employee == null)
            {
                return NotFound();
            };


            //Employee.Quotes and .Orders

            return Ok(employee);
        }

        [HttpPost]
        public ActionResult<EmployeesDto> Add(EmployeesDto employee)
        {

            EmpDesignationDetailsDTO empDesignation = new EmpDesignationDetailsDTO()
            {
                DesignationId = employee.DesignationDetails.DesignationId,
            };

            //Adding User to DTO

            EmployeesDto addEmployeeDto = new EmployeesDto()
            {

                Name = employee.Name,
                Salary = employee.Salary,
                DateOfJoining = employee.DateOfJoining,
                DesignationDetails = empDesignation,
                
            };

            //Adding from DTO to Employee => table

            Employee newEmp = new Employee()
            {
                Name = addEmployeeDto.Name,
                Salary = addEmployeeDto.Salary,
                DateOfJoining = addEmployeeDto.DateOfJoining,
                Designation = addEmployeeDto.DesignationDetails.DesignationId
            };

            _dbbikeDealerContext.Employees.Add(newEmp);
            _dbbikeDealerContext.SaveChanges();
            return Ok();
        }


        [HttpPut("{id}")]
        public IActionResult Edit(EmployeesDto employee)
        {
            var editEmployee = _dbbikeDealerContext.Employees.FirstOrDefault(x => x.EmpId == employee.EmpId);
            if (editEmployee == null || employee.EmpId == 0)
            {
                return NotFound();
            }

            EmpDesignationDetailsDTO employeesDesignationDto = new EmpDesignationDetailsDTO()
            {
                DesignationId = employee.DesignationDetails.DesignationId
            };

            editEmployee.Name = employee.Name;
            editEmployee.Salary = employee.Salary;
            editEmployee.Designation = employee.DesignationDetails.DesignationId;
            editEmployee.DateOfJoining = employee.DateOfJoining;
            editEmployee.DateOfResign = employee.DateOfResign;

            _dbbikeDealerContext.Update(editEmployee);
            _dbbikeDealerContext.SaveChanges();
            return Ok();

        }

    }
}
