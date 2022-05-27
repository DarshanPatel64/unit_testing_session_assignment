namespace unitTestAssignment.cs.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        
        public virtual ICollection<Employee>? Employees { get; set; }
    }
}
