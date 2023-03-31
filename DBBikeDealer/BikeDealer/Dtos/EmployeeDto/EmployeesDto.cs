namespace BikeDealer.Dtos.EmployeeDto
{
    public class EmployeesDto
    {
        public int EmpId { get; set; }
        public string? Name { get; set; }
        public int? Salary { get; set; }
        public DateTime? DateOfJoining { get; set; }
        public DateTime? DateOfResign { get; set; }
        public EmpDesignationDetailsDTO DesignationDetails { get; set; }

    }

    public class EmpDesignationDetailsDTO
    {
        public int DesignationId { get; set; }
        public string Designation { get; set; }
    }

}
