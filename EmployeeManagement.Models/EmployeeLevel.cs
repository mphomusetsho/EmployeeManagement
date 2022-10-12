using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class EmployeeLevel
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Level description")]
        [Required]
        public string LevelDescr { get; set; }
    }
}
