using Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.UI.Controllers
{
    public class CategoryController(IUnitOfWork unitOfWork) : Controller
    {
        public async Task<IActionResult> GetAllCategory()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        public async Task<IActionResult> Update()
        {
            return View();
        }

        public async Task<IActionResult> SoftDelete()
        {
            return View();
        }

        public async Task<IActionResult> Delete()
        {
            return View();
        }
    }
}
