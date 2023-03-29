using BikeDealer.Models;

namespace BikeDealer.Dtos.EmployeeDto
{
    public class GetEmployeebyIdDTO
    {
        public int EmpId { get; set; }
        public string? Name { get; set; }
        public int? Salary { get; set; }
        public DateTime? DateOfJoining { get; set; }
        public DateTime? DateOfResign { get; set; }
        public EmployeesDesignationDto EmployeesDesignationDto { get; set; }
        public OrdersbyEmp OrdersbyEmp { get; set; }
        public QuotationbyEmp QuotationbyEmp { get; set; }

    }

    public class EmployeesDesignationDto
    {
        public string Designation { get; set; }
    }

    public class OrdersbyEmp
    {
        public int TotalOrder { get; set; }
    }

    public class QuotationbyEmp
    {
        public int QuotationEmp { get; set; }
    }


}
