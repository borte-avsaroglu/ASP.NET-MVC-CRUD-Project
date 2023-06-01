﻿using Microsoft.AspNetCore.Mvc;
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
            var employees = await mvcDemoDbContext.Employees.ToListAsync();
            return View(employees);
        }

        [HttpGet]
        public IActionResult Add()
        {
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
                Department = addEmployeeRequest.Department
            };

            await mvcDemoDbContext.Employees.AddAsync(employee);
            await mvcDemoDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var employee = await mvcDemoDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

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
                    Department = employee.Department
                };
                return await Task.Run(() => View("View", viewModel));
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModel model)
        {
            var employee = await mvcDemoDbContext.Employees.FindAsync(model.Id);

            if (employee != null)
            {
                employee.Fname = model.Fname;
                employee.Lname = model.Lname;
                employee.Email = model.Email;
                employee.DateOfBirth = model.DateOfBirth;
                employee.Salary = model.Salary;
                employee.Department = model.Department;

                await mvcDemoDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel model)
        {
            var employee = mvcDemoDbContext.Employees.Find(model.Id);

            if(employee != null)
            {
                mvcDemoDbContext.Employees.Remove(employee);
                await mvcDemoDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
