using EmployeeManagement.DataAccess;
using EmployeeManagement.DataAccess.Repository.IRepository;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EmployeeManagementWeb.Controllers;
[Area("Admin")]
public class EmployeeLevelController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public EmployeeLevelController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    //Displays the list of employee levels
    //GET
    public IActionResult Index()
    {
        IEnumerable<EmployeeLevel> objEmployeeLevelList = _unitOfWork.EmployeeLevel.GetAll();
        return View(objEmployeeLevelList);
    }

    //Adds an employee
    //GET
    public IActionResult Create()
    {
        return View();
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(EmployeeLevel obj)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.EmployeeLevel.Add(obj);
            _unitOfWork.Save();
            TempData["success"] = "EmployeeLevel added successfully.";
            return RedirectToAction("Index");
        }

        return View(obj);
    }

    //Edits employee details
    //GET
    [HttpGet]

    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        var employeeLevel = _unitOfWork.EmployeeLevel.GetFirstOrDefault(x => x.Id == id);

        if (employeeLevel == null)
        {
            return NotFound();
        }
        return View(employeeLevel);
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(EmployeeLevel obj)
    {
        if (ModelState.IsValid && obj.Id != 0)
        {
            _unitOfWork.EmployeeLevel.Update(obj);
            _unitOfWork.Save();
            TempData["success"] = "EmployeeLevel updated successfully.";
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

        var employeeLevel = _unitOfWork.EmployeeLevel.GetFirstOrDefault(x => x.Id == id);

        if (employeeLevel == null)
        {
            return NotFound();
        }
        return View(employeeLevel);
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(EmployeeLevel obj)
    {
        if (obj == null)
        {
            return NotFound();
        }

        _unitOfWork.EmployeeLevel.Remove(obj);
        _unitOfWork.Save();
        TempData["success"] = "EmployeeLevel deleted successfully.";

        return RedirectToAction("Index");
    }
}

