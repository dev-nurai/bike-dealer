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
        public ActionResult<List<GetEmployeeDto>> GetAll()
        {
            var allEmp = (from employee in _dbbikeDealerContext.Employees
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
                          });

            var getEmployees = allEmp.ToList();

            List<GetEmployeeDto> getEmployeeDtos = new();

            foreach (var employee in getEmployees)
            {

                DesignationDetails designationDetails = new()
                {
                    Designation = employee.DesignationNavigation.Designation,
                };

                GetEmployeeDto getEmployeeDto = new GetEmployeeDto()
                {
                    
                    EmpId = employee.EmpId,
                    Name = employee.Name,
                    Salary = employee.Salary,
                    DateOfJoining = employee.DateOfJoining,
                    DateOfResign = employee.DateOfResign,
                    DesignationDetails = designationDetails,

                };
                getEmployeeDtos.Add(getEmployeeDto);

            }

            return Ok(getEmployeeDtos);

        }

        [HttpGet("{id}")]
        public ActionResult<GetEmployeebyIdDTO> Get(int id)
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

                            }).FirstOrDefault();

            if (employee == null)
            {
                return NotFound();
            };

            EmployeesDesignationDto employeesDesignationDto = new EmployeesDesignationDto()
            {
                Designation = employee.DesignationNavigation.Designation,
            };

            OrdersbyEmp ordersbyEmp = new OrdersbyEmp()
            {
                TotalOrder = employee.OrderEmps.Count()
            };

            GetEmployeebyIdDTO getEmployeebyIdDTO = new()
            {
                EmpId = employee.EmpId,
                Name = employee.Name,
                Salary = employee.Salary,
                DateOfJoining = employee.DateOfJoining,
                DateOfResign = employee.DateOfResign,
                EmployeesDesignationDto = employeesDesignationDto,
                OrdersbyEmp = ordersbyEmp,

            };


            //Employee.Quotes and .Orders

            return Ok(getEmployeebyIdDTO);
        }

        [HttpPost]
        public ActionResult<GetEmployeeDto> Add(AddEmployeeDto employee)
        {
            EmpDesignation empDesignation = new EmpDesignation()
            {
                DesignationId = employee.Designation.DesignationId,
            };

            //Adding User to DTO

            AddEmployeeDto addEmployeeDto = new AddEmployeeDto()
            {

                Name = employee.Name,
                Salary = employee.Salary,
                DateOfJoining = employee.DateOfJoining,
                Designation = empDesignation,
                
            };

            //Adding from DTO to Employee => table

            Employee newEmp = new Employee()
            {
                Name = addEmployeeDto.Name,
                Salary = addEmployeeDto.Salary,
                DateOfJoining = addEmployeeDto.DateOfJoining,
                Designation = addEmployeeDto.Designation.DesignationId
            };

            _dbbikeDealerContext.Employees.Add(newEmp);
            _dbbikeDealerContext.SaveChanges();
            return Ok();
        }


        [HttpPut("{id}")]
        public IActionResult Edit(EditEmployeeDTO employee)
        {
            var editEmployee = _dbbikeDealerContext.Employees.FirstOrDefault(x => x.EmpId == employee.EmpId);
            if (editEmployee == null || employee.EmpId == 0)
            {
                return NotFound();
            }

            EditEmpDesignation employeesDesignationDto = new EditEmpDesignation()
            {
                DesignationId = employee.EditEmpDesignation.DesignationId
            };

            editEmployee.Name = employee.Name;
            editEmployee.Salary = employee.Salary;
            editEmployee.Designation = employee.EditEmpDesignation.DesignationId;
            editEmployee.DateOfJoining = employee.DateOfJoining;
            editEmployee.DateOfResign = employee.DateOfResign;

            _dbbikeDealerContext.Update(editEmployee);
            _dbbikeDealerContext.SaveChanges();
            return Ok();

        }

    }
}
