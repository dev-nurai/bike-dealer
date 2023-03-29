namespace BikeDealer.Dtos.EmployeeDto
{
    public class EditEmployeeDTO
    {
        public int EmpId { get; set; }
        public string? Name { get; set; }
        public int? Salary { get; set; }
        public DateTime? DateOfJoining { get; set; }
        public DateTime? DateOfResign { get; set; }
        public EditEmpDesignation EditEmpDesignation { get; set; }
    }

    public class EditEmpDesignation
    {
        public int DesignationId { get; set; }
    }
}
