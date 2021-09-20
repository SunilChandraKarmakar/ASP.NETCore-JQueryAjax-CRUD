using JQueryAjaxCrud.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using static JQueryAjaxCrud.Helper;

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
        [NoDirectAccess]
        public async Task<IActionResult> Upsert(int id = 0)
        {
            if (id == 0)
                return View(new Employee());
            else
            {
                Employee existEmployee = await _db.Employees.FindAsync(id);

                if (existEmployee == null)
                    return NotFound();

                return View(existEmployee);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(Employee employee)
        {
            if(ModelState.IsValid)
            {
               if(employee.Id == 0)
               {
                    _db.Employees.Add(employee);
                    await _db.SaveChangesAsync();
               } 
               else
               {
                    _db.Update(employee);
                    await _db.SaveChangesAsync();
               }

                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", _db.Employees.ToList()) });
            }

            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Upsert", employee) });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            Employee existEmployee = await _db.Employees.FindAsync(id);

            if (existEmployee == null)
                return NotFound();

            _db.Employees.Remove(existEmployee);
            await _db.SaveChangesAsync();
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAll", _db.Employees.ToList()) });
        }
    }
}
