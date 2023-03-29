namespace BikeDealer.Dtos.EmployeeDto
{
    public class GetEmployeeDto
    {
        public int EmpId { get; set; }
        public string? Name { get; set; }
        public int? Salary { get; set; }
        public DateTime? DateOfJoining { get; set; }
        public DateTime? DateOfResign { get; set; }
        public DesignationDetails DesignationDetails { get; set; }

    }

    public class DesignationDetails
    {
        public string Designation { get; set; }
    }

}
