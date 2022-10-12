using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Models.ViewModels
{
    public class EmployeeLevelViewModel
    {
        public EmployeeLevel EmployeeLevel { get; set; }
        public IEnumerable<SelectListItem> EmployeeLevelsList { get; set; }
    }
}
