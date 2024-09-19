using DemoOCR.Data;
using DemoOCR.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DemoOCR.Controllers
{
    public class FormController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FormController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var forms = await _context.Forms.Include(f => f.Module).ToListAsync();
                return View(forms);
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                ModelState.AddModelError(string.Empty, "An error occurred while retrieving forms.");
                return View(new List<Form>()); // Return an empty list or handle as needed
            }
        }


        // GET: Form/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var form = await _context.Forms
                .Include(f => f.Module)
                .Include(f => f.FormFields)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (form == null)
            {
                return NotFound();
            }

            return View(form);
        }

        // GET: Form/Create
        public IActionResult Create()
        {
            ViewBag.Modules = _context.Modules.ToList();
            return View();
        }

        // POST: Form/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Form form)
        {
            if (form.Id == 0)
            {
                _context.Add(form);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Modules = _context.Modules.ToList(); // Repopulate modules if validation fails
            return View(form);
        }

        // GET: Form/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var form = await _context.Forms.FindAsync(id);
            if (form == null)
            {
                return NotFound();
            }
            ViewBag.Modules = await GetModuleList();// Populate dropdown with modules
            return View(form);
        }


        private async Task<IEnumerable<SelectListItem>> GetModuleList()
        {
            var countryList = await _context.Modules.ToListAsync();

            return countryList.Where(c => c.ModuleName != null)
                                  .Select(a => new SelectListItem
                                  {
                                      Value = a.Id.ToString(),
                                      Text = a.ModuleName
                                  }).OrderBy(c => c.Value).ToList();
        }

        // POST: Form/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Form form)
        {
            if (id != form.Id)
            {
                return NotFound();
            }

            if (form.Id !=0)
            {
                try
                {
                    _context.Update(form);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormExists(form.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Modules = await GetModuleList(); // Repopulate modules if validation fails
            return View(form);
        }

        // GET: Form/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var form = await _context.Forms
                .Include(f => f.Module)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (form == null)
            {
                return NotFound();
            }

            return View(form);
        }

        // POST: Form/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var form = await _context.Forms.FindAsync(id);
            _context.Forms.Remove(form);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Helper method to check if a Form exists
        private bool FormExists(int id)
        {
            return _context.Forms.Any(e => e.Id == id);
        }

        public async Task<IActionResult> RenderForm(int formId)
        {
            // Fetch the form and its associated fields and module
            var form = await _context.Forms
                .Include(f => f.FormFields)  // Include the FormFields collection
                .Include(f => f.Module)      // Include the associated Module
                .FirstOrDefaultAsync(f => f.Id == formId);

            if (form == null)
            {
                return NotFound();
            }

            return View(form);
        }


        [HttpPost]
        public async Task<IActionResult> SubmitForm(int formId, Dictionary<string, string> formValues)
        {
            // Here you will process the form values (e.g., save to the database or validate)

            // Example: Iterate over the submitted values
            foreach (var field in formValues)
            {
                Console.WriteLine($"{field.Key}: {field.Value}");
            }

            // After processing, redirect to a confirmation page or display a success message
            return RedirectToAction("FormSubmitted");
        }
    }
}
