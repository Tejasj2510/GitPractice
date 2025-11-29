using EmployeeDetails_AJAX_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDetails_AJAX_.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly CompanyDbContext _context;

        public EmployeesController(CompanyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchString)
        {
            var emp = _context.Employees.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                emp = emp.Where(e =>
                    e.Name.Contains(searchString) ||
                    e.Surname.Contains(searchString));
            }

            var employees = await emp.ToListAsync();

            return View(employees);
        }


        [HttpGet]
        public async Task<IActionResult> DetailsPartial(int id)
        {
            var emp = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (emp == null)
                return NotFound();

            return PartialView("_DetailsPartial", emp);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> Create(Employee emp)
        {
            if (ModelState.IsValid)
            {
                await _context.Employees.AddAsync(emp);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(emp);
        }

       
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var emp = await _context.Employees.FindAsync(id);

            if (emp == null)
                return NotFound();

            return View(emp); 
        }

        [HttpPost, ActionName("Delete")]
   
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var emp = await _context.Employees.FindAsync(id);

            if (emp == null)
                return NotFound();

            _context.Employees.Remove(emp);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }



        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var emp = await _context.Employees.FindAsync(id);

            if (emp == null)
                return NotFound();

            return View(emp);
        }

        
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Employee emp)
        {
            if (id != emp.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(emp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return BadRequest("Update failed due to concurrency issue.");
                }

                return RedirectToAction(nameof(Index));
            }

            return View(emp);
        }

        [HttpGet]
        public IActionResult Addition()
        {
            return PartialView("AdditionPartial");
        }

        [HttpPost]
        public JsonResult AdditionCalculate(int a, int b)
        {
            int sum = a + b;
            return Json(sum);
        }

        public async Task<IActionResult> Search(string term)
        {
            var query = _context.Employees.AsQueryable();

            if (!string.IsNullOrEmpty(term))
            {
                query = query.Where(e =>
                    e.Name.Contains(term) ||
                    e.Surname.Contains(term));
            }

            var employees = await query.ToListAsync();
            return PartialView("_EmployeeList", employees);
        }

    }
}
