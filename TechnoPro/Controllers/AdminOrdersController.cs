using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechnoPro.Services;

namespace TechnoPro.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("Admin/Orders/{action=Index}/{id?}")]
    public class AdminOrdersController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly int PageSize = 10;

        public AdminOrdersController(ApplicationDbContext context)
        {
            this.context = context;
        }


        public IActionResult Index()
        {
            var orders = context.Orders.OrderByDescending(o => o.Id).ToList();

            ViewBag.Orders = orders;

            return View();
        }
    }
}
