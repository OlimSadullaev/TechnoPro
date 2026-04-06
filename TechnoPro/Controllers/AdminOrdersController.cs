using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechnoPro.Models;
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


        public IActionResult Index(int pageIndex)
        {
            IQueryable<Order> query = context.Orders
                .Include(o => o.Client)
                .Include(o => o.Items)
                .ThenInclude(oi => oi.Product);

            if(pageIndex <= 0)
            {
                pageIndex = 1;
            }

            decimal count = query.Count();
            int totalPages = (int)Math.Ceiling(count / PageSize);

            query = query
                .Skip((pageIndex - 1) * PageSize)
                .Take(PageSize);

            var orders = query.ToList();

            ViewBag.Orders = orders;
            ViewBag.PageIndex = pageIndex;
            ViewBag.TotalPages = totalPages;

            return View();
        }

        public IActionResult Details(int id)
        {
            var order = context.Orders
                .Include(o => o.Client)
                .Include(o => o.Items)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefault(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            ViewBag.NumOrders = context.Orders.Where(o => o.ClientId == order.ClientId).Count();
            return View();
        }
    }
}
