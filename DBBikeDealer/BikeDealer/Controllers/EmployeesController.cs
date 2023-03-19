﻿using BikeDealer.Models;
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
            return Ok(_dbbikeDealerContext.Employees.ToList());
        }

        [HttpGet("{id}")]
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

        [HttpPost]
        public ActionResult<Employee> Add(Employee employee)
        {
            _dbbikeDealerContext.Employees.Add(employee);
            _dbbikeDealerContext.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var delEmployee = _dbbikeDealerContext.Employees.FirstOrDefault(x=> x.EmpId ==id);
            if(delEmployee == null || id == 0)
            {
                return NotFound();
            }
            _dbbikeDealerContext.Employees.Remove(delEmployee);
            _dbbikeDealerContext.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, Employee employee)
        {
            var editEmployee = _dbbikeDealerContext.Employees.FirstOrDefault(x=> x.EmpId ==id);
            if(editEmployee == null || id == 0)
            {
                return NotFound();
            }
            else
            {
                editEmployee.Name = employee.Name;
                editEmployee.Salary = employee.Salary;
                editEmployee.DateOfJoining = employee.DateOfJoining;
                editEmployee.DateOfResign = employee.DateOfResign;
                //designation is foreign key
            }
            return NoContent();
        }

    }
}
