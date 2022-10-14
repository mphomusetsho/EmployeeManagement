using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeNumber { get; set; }
        [Required]
        [DisplayName("First name")]
        public string FirstName { get; set; }
        [DisplayName("Other names")]
        public string? OtherNames { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        [DisplayName("Date of birth")]
        public DateTime BirthDate { get; set; }
        [Required]
        [Range(1000, 1000000)]
        public int Salary { get; set; }
        [Required]
        public string Level { get; set; }
        [Required]
        public string Role { get; set; }
        [DisplayName("Manager names")]
        public string? ManagerNames { get; set; }
        [DisplayName("ManagerID")]
        public int? ManagerID { get; set; }

        //public List<SelectListItem> Roles { get; } = new List<SelectListItem>
        //{
        //    new SelectListItem {Value = "CEO", Text = "CEO"},
        //    new SelectListItem {Value = "DepartmentManager", Text = "Department manager"}
        //};
    }
}
