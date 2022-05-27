namespace unitTestAssignment.cs.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public int departmentId { get; set; }
        public virtual Department? Department { get; set; }

    }
}
