using JQueryAjaxCrud.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace JQueryAjaxCrud.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeDbContext _db; 

        public EmployeeController(EmployeeDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _db.Employees.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Upsert(int id = 0)
        {
            if (id == 0)
                return View();
            else
            {
                Employee existEmployee = await _db.Employees.FindAsync(id);

                if (existEmployee == null)
                    return NotFound();

                return View(existEmployee);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            if(ModelState.IsValid)
            {
                _db.Employees.Add(employee);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); 
            }

            return  View(employee);
        }
    }
}
