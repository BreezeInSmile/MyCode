using Microsoft.AspNetCore.Mvc;
using MyCode.Models;
using MyCode.Services;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace MyCode.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly IOptions<MyCodeOptions> _myCodeOptions;
        public DepartmentController(IDepartmentService departmentService, IOptions<MyCodeOptions> myCodeOptions)
        {
            this._departmentService = departmentService;
            this._myCodeOptions = myCodeOptions;
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