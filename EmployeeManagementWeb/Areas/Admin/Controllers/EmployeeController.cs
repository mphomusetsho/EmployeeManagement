using EmployeeManagement.DataAccess;
using EmployeeManagement.DataAccess.Repository;
using EmployeeManagement.DataAccess.Repository.IRepository;
using EmployeeManagement.Models;
using EmployeeManagement.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeManagementWeb.Controllers;
[Area("Admin")]
public class EmployeeController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly string highestRole = "ceo";

    public EmployeeController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    //Displays the list of employees
    //GET
    public IActionResult Index()
    {
        IEnumerable<Employee> objEmployeeList = _unitOfWork.Employee.GetAll();
        return View(objEmployeeList);
    }

    //Adds an employee
    //GET
    public IActionResult Create()
    {
        IEnumerable<SelectListItem> levelsList = _unitOfWork.EmployeeLevel.GetAll().
            Select(x => new SelectListItem
            {
                Text = x.LevelDescr
            });
        ViewData["list"] = levelsList;
        //EmployeeLevelViewModel employeeLevelVM = new()
        //{
        //    EmployeeLevel = new(),
        //    EmployeeLevelsList = _unitOfWork.EmployeeLevel.GetAll().
        //    Select(x => new SelectListItem
        //    {
        //        Text = x.LevelDescr
        //    })
        //};

        return View();
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Employee obj)
    {
        if (obj.ManagerNames == null && !obj.Role.ToLower().Contains(highestRole))
        {
            ModelState.AddModelError("managernames", "Only CEOs cannot have managers");
        }

        //Assuming that a maximum age to get newly hired is 60.
        if (getAge(obj.BirthDate) < 15 || getAge(obj.BirthDate) > 60)
        {
            ModelState.AddModelError("birthdate", "Age is invalid");
        }

        if (ModelState.IsValid)
        {
            _unitOfWork.Employee.Add(obj);
            _unitOfWork.Save();
            TempData["success"] = "Employee added successfully.";
            return RedirectToAction("Index");
        }

        return View(obj);
    }

    //Edits employee details
    //GET
    [HttpGet]

    public IActionResult Edit(int? id)
    {
        IEnumerable<SelectListItem> levelsList = _unitOfWork.EmployeeLevel.GetAll().
            Select(x => new SelectListItem
            {
                Text = x.LevelDescr
            });
        ViewData["list"] = levelsList;

        if (id == null || id == 0)
        {
            return NotFound();
        }

        //var employee = _db.Employees.Find(id);
        var employee = _unitOfWork.Employee.GetFirstOrDefault(x => x.EmployeeNumber == id);

        if (employee == null)
        {
            return NotFound();
        }
        return View(employee);
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Employee obj)
    {
        //Assuming that a maximum age to get be employed is 67.
        if (getAge(obj.BirthDate) < 15 || getAge(obj.BirthDate) > 67)
        {
            ModelState.AddModelError("birthdate", "Age is invalid");
        }

        if (obj.ManagerNames == null && !obj.Role.ToLower().Contains(highestRole))
        {
            ModelState.AddModelError(string.Empty, "Only CEOs cannot have managers");
        }

        if (ModelState.IsValid && obj.EmployeeNumber != 0)
        {
            _unitOfWork.Employee.Update(obj);
            _unitOfWork.Save();
            TempData["success"] = "Employee updated successfully.";
            return RedirectToAction("Index");
        }

        return View(obj);
    }

    //Deletes an employee record
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        //var employee = _db.Employees.Find(id);
        var employee = _unitOfWork.Employee.GetFirstOrDefault(x => x.EmployeeNumber == id);

        if (employee == null)
        {
            return NotFound();
        }
        return View(employee);
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(Employee obj)
    {
        if (obj == null)
        {
            return NotFound();
        }

        _unitOfWork.Employee.Remove(obj);
        _unitOfWork.Save();
        TempData["success"] = "Employee deleted successfully.";

        return RedirectToAction("Index");
    }

    private int getAge(DateTime birthDate)
    {
        // Save today's date.
        var today = DateTime.Today;

        // Calculate the age.
        var age = today.Year - birthDate.Year;

        // Go back to the year in which the person was born in case of a leap year
        if (birthDate.Date > today.AddYears(-age))
        {
            age--;
        }

        return age;
    }
}

