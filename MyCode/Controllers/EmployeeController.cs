using Microsoft.AspNetCore.Mvc;
using MyCode.Models;
using MyCode.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCode.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IDepartmentService departmentService, IEmployeeService employeeService)
        {
            this._departmentService = departmentService;
            this._employeeService = employeeService;
        }

        public async Task<IActionResult> Index(int departmentId)
        {
            var department = await _departmentService.GetById(departmentId);

            ViewBag.Title = $"Employee of {department.Name}";
            ViewBag.DepartmentId = departmentId;

            var employee = await _employeeService.GetByDepartmentId(departmentId);

            return View(employee);
        }

        [HttpGet]
        public IActionResult Add(int departmentId)
        {
            ViewBag.Title = "Add Employee";
            return View(new Employee
            {
                DepartmentId = departmentId
            });
        }

        [HttpPost]
        public async Task<IActionResult> Add(Employee employee)
        {
            if (ModelState.IsValid)
            {
                await _employeeService.add(employee);
            }

            return RedirectToAction(nameof(Index), new { departmentId = employee.DepartmentId });
        }

        public async Task<IActionResult> Fird(int employeeId)
        {
            var employee = await _employeeService.Fire(employeeId);

            return RedirectToAction(nameof(Index), new { departmentId = employee.DepartmentId });
        }
    }
}
