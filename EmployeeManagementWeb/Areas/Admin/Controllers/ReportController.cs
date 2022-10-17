using EmployeeManagement.DataAccess.Repository.IRepository;
using EmployeeManagement.Models;
using EmployeeManagement.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EmployeeManagementWeb.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = SD.Role_Admin)]
public class ReportController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    public ReportController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public IActionResult Index()
    {
        IEnumerable<Employee> objEmployeeList = _unitOfWork.Employee.GetAll();
        return View(objEmployeeList);
    }
}
