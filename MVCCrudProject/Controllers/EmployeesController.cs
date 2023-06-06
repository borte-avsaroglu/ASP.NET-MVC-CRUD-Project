using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCCrudProject.Data;
using MVCCrudProject.Models;
using MVCCrudProject.Models.Domain;

namespace MVCCrudProject.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly MVCDemoDbContext mvcDemoDbContext;

        public EmployeesController(MVCDemoDbContext mvcDemoDbContext)
        {
            this.mvcDemoDbContext = mvcDemoDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Employee> employees = mvcDemoDbContext.Employees.Include(e => e.Department).ToList();
            return View(employees);

        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {

            var departments = await mvcDemoDbContext.Departments.ToListAsync();

            ViewBag.Departments = departments;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Fname = addEmployeeRequest.Fname,
                Lname = addEmployeeRequest.Lname,
                Email = addEmployeeRequest.Email,
                DateOfBirth = addEmployeeRequest.DateOfBirth,
                Salary = addEmployeeRequest.Salary,
                DeptID = addEmployeeRequest.DeptID,
            };

            await mvcDemoDbContext.Employees.AddAsync(employee);
            await mvcDemoDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var employee = await mvcDemoDbContext.Employees.FirstOrDefaultAsync(employee => employee.Id == id); 
            var departments = await mvcDemoDbContext.Departments.ToListAsync();

            ViewBag.Departments = departments;

            if (employee != null)
            {
                var viewModel = new UpdateEmployeeViewModel()
                {
                    Id = employee.Id,
                    Fname = employee.Fname,
                    Lname = employee.Lname,
                    Email = employee.Email,
                    DateOfBirth = employee.DateOfBirth,
                    Salary = employee.Salary,
                    DeptID = employee.DeptID
                };
                return await Task.Run(() => View("Edit", viewModel));
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateEmployeeViewModel model)
        {
            var employee = await mvcDemoDbContext.Employees.FindAsync(model.Id);

            if (employee != null)
            {
                employee.Fname = model.Fname;
                employee.Lname = model.Lname;
                employee.Email = model.Email;
                employee.DateOfBirth = model.DateOfBirth;
                employee.Salary = model.Salary;
                employee.DeptID = model.DeptID;

                await mvcDemoDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel model)
        {
            var employee = mvcDemoDbContext.Employees.Find(model.Id);
            var response = new { success = false, message = "Deletion failed." };

            if (employee != null)
            {
                mvcDemoDbContext.Employees.Remove(employee);
                await mvcDemoDbContext.SaveChangesAsync();

                response = new { success = true, message = "Deletion successful." };

            }
            return Json(response);
        }
    }
}
