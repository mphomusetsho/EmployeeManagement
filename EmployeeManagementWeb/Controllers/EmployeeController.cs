using EmployeeManagement.DataAccess;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EmployeeManagementWeb.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly String highestRole = "ceo";

        public EmployeeController(ApplicationDbContext db)
        {
            _db = db;
        }

        //Displays the list of employee
        //GET
        public IActionResult Index()
        {
            IEnumerable<Employee> objEmployeeList = _db.Employees;
            return View(objEmployeeList);
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
                _db.Employees.Add(obj);
                _db.SaveChanges();
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
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var employee = _db.Employees.Find(id);
            //var employee = _db.Employees.SingleOrDefault(x => x.EmployeeNumber == empNum);

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
                ModelState.AddModelError(String.Empty, "Only CEOs cannot have managers");
            }

            if (ModelState.IsValid && obj.EmployeeNumber != 0)
            {
                _db.Employees.Update(obj);
                _db.SaveChanges();
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

            var employee = _db.Employees.Find(id);
            //var employee = _db.Employees.SingleOrDefault(x => x.EmployeeNumber == empNum);

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

            _db.Employees.Remove(obj);
            _db.SaveChanges();
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
}
