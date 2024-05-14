namespace EmployeeManagement.Models
{
    public class Emp_Dept
    {
        public Employee employee { get; set; }
        public List<Department> departments { get; set; }   
    }
}
