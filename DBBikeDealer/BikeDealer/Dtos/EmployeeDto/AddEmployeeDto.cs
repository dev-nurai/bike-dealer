namespace BikeDealer.Dtos.EmployeeDto
{
    public class AddEmployeeDto
    {
        public string? Name { get; set; }
        public int? Salary { get; set; }
        public DateTime? DateOfJoining { get; set; }
        
        public EmpDesignation Designation { get; set; }
    }
    public class EmpDesignation
    {
        public int DesignationId { get; set; }
    }


}
