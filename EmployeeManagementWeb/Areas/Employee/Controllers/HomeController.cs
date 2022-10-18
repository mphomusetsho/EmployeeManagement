using EmployeeManagement.DataAccess;
using EmployeeManagement.DataAccess.Repository;
using EmployeeManagement.DataAccess.Repository.IRepository;
using EmployeeManagement.DataAccess.Service;
using EmployeeManagement.Models;
using EmployeeManagement.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace EmployeeManagementWeb.Controllers;
[Area("Employee")]
//[Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
public class HomeController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<HomeController> _logger;
    private readonly IUserService _userService;
    private readonly ApplicationDbContext _db;
    public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, 
        IUserService userService, ApplicationDbContext db)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _userService = userService;
        _db = db;
    }

    public IActionResult Index()
    {
        var userId = _userService.GetUserId();
        if (userId == null)
        {
            ViewData["UserNotFound"] = "You are not logged in";
        } else
        {
            //var user = _unitOfWork.ApplicationUser.GetFirstOrDefault(x => x.Id == userId);
            var user = _db.ApplicationUsers.FirstOrDefault(x => x.Id == userId);
            if (user == null)
            {
                return NotFound();
            }

            var employeeNumber = user.EmployeeNumber;
            //ViewDetails(employeeNumber);
            var employee = _unitOfWork.Employee.GetFirstOrDefault(x => x.EmployeeNumber == employeeNumber);

            if (employee == null)
            {
                ViewData["EmployeeNotFound"] = "You are not yet registered as an employee";
            }
            //else
            //{
            //    ViewDetails(employee);
            //}
            else
            {
                ViewData["EmployeeNumber"] = employee.EmployeeNumber;
                ViewData["FirstName"] = employee.FirstName;
                ViewData["Surname"] = employee.Surname;
                ViewData["DateOfBirth"] = employee.BirthDate;
                ViewData["Role"] = employee.Role;
                ViewData["Salary"] = employee.Salary;
                ViewData["ManagerNames"] = employee.ManagerNames;
            }
        }
        

        return View();
    }

    public IActionResult ViewDetails(Employee employee)
    {
        //if (id == null || id == 0)
        //{
        //    return NotFound();
        //}

        //var employee = _db.Employees.Find(id);
        // employee = _unitOfWork.Employee.GetFirstOrDefault(x => x.EmployeeNumber == id);

        //if (employee == null)
        //{
        //    return NotFound();
        //}
        return View(employee);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public List<Employee> GetData()
    {
        var data = new List<Employee>();
        DataTable myData = GetDataTable();

        //Get the CEO and add them to the list
        Employee ceo = _unitOfWork.Employee.GetFirstOrDefault(x => x.ManagerID == null);
        data.Add(ceo);

        foreach (DataRow row in myData.Rows)
        {   
            var empName = row["FirstName"].ToString();
            var empID = row["EmployeeNumber"].ToString();
            var empSurname = row["Surname"].ToString();
            var empDob = row["BirthDate"].ToString();
            var empSalary = row["Salary"].ToString();
            var empRole = row["Role"].ToString();
            var mngrID = row["ManagerID"].ToString();
            var mngrName = row["ManagerNames"].ToString();

            data.Add(new Employee
            {
                EmployeeNumber = Int32.Parse(empID),
                FirstName = empName,
                Surname = empSurname,
                BirthDate = DateTime.Parse(empDob),
                Salary = Int32.Parse(empSalary),
                Role = empRole,
                ManagerID = Int32.Parse(mngrID),
                ManagerNames = mngrName,
            }); 
        }

        return data;
    }

    public DataTable GetDataTable()
    {
        var conn = "Server=MCEN2000\\SQLEXPRESS01;Database=EmpManagement;Trusted_Connection=True;";


        //var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        
        DataTable data = new DataTable();
        string query = "select EmployeeNumber, FirstName, Surname, BirthDate, Role, Salary, ManagerID, " +
            "ManagerNames\r\nfrom Employees\r\nwhere ManagerID != 0";
        //string query = "select a.EmployeeNumber as EmployeeNumber, a.FirstName, a.Surname,\r\na.Role," +
        //    " a.Salary,a.BirthDate, b.ManagerID, b.FirstName\r\nfrom Employees a\r\ninner join Employees b on b.ManagerID = a.EmployeeNumber";
        SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
        DataSet ds = new DataSet();
        adapter.Fill(ds);
        return ds.Tables[0];
    }

    #region API CALLS
    [HttpGet]
    //public IActionResult GetAll()
    //{
    //    var employeeList = _unitOfWork.Employee.GetAll();
    //    return Json(new { data = employeeList });
    //}
    public List<Employee> GetAll()
    {
        var employeeList = _unitOfWork.Employee.GetAll();
        //return Json(new { data = employeeList });
        return (List<Employee>)employeeList;
    }


    #endregion
}


