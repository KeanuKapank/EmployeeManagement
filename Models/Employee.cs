using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        [Required]
        [StringLength(50)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        public byte[]? Picture { get; set; }
        [Required(ErrorMessage = "Please select an Department")]
        [ForeignKey("DepartmentId")]
        [DisplayName("Deparment")]
        public int DepartmentId { get; set; }
        public virtual Department? Department { get; set; }
    }
}
