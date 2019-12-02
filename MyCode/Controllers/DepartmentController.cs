using Microsoft.AspNetCore.Mvc;
using MyCode.Models;
using MyCode.Services;
using System.Threading.Tasks;

namespace MyCode.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            this._departmentService = departmentService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Title = "Department Index";
            var department = await _departmentService.GetAll();

            return View(department);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Title = "Add Department";
            return View(new Department());
        }

        [HttpPost]
        public async Task<IActionResult> Add(Department model)
        {
            if (ModelState.IsValid)
            {
                await _departmentService.Add(model);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}