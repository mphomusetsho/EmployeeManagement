using EmployeeManagement.DataAccess.Repository.IRepository;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementWeb.Areas.Admin.Controllers;
[Area("Admin")]
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
