using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCode.Services;
namespace MyCode.ViewComponents{
    public class CompanySummaryViewComponent: ViewComponent{
        
        private readonly IDepartmentService _departmentService;

        public class CompanySummaryViewComponent(IDepartmentService departmentService){

        }
    }
}