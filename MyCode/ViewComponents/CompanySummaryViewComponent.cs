using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCode.Services;
namespace MyCode.ViewComponents{
    public class CompanySummaryViewComponent: ViewComponent{
        
        private readonly IDepartmentService _departmentService;

        public CompanySummaryViewComponent(IDepartmentService departmentService){
            this._departmentService = departmentService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string title){
            ViewBag.Title = title;
            var summary = await _departmentService.GetCompanySummary();
            return View(summary);
        }
        
    }
}