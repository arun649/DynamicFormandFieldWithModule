using DemoOCR.Data;
using DemoOCR.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoOCR.Controllers
{
    public class FormFieldController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FormFieldController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var formFields = await _context.FormFields.Include(f => f.Form).ToListAsync();
            return View(formFields);
        }

        // GET: FormField/Create
        public IActionResult Create()
        {
            ViewBag.Forms = _context.Forms.ToList(); // Populate form dropdown
            return View();
        }

        // POST: FormField/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FormField formField)
        {
            if (formField.Id==0)
            {
                _context.Add(formField);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Forms = _context.Forms.ToList(); // Repopulate forms if validation fails
            return View(formField);
        }

        // GET: FormField/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formField = await _context.FormFields.FindAsync(id);
            if (formField == null)
            {
                return NotFound();
            }

            ViewBag.Forms = _context.Forms.ToList(); // Populate form dropdown
            return View(formField);
        }

        // POST: FormField/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FormField formField)
        {
            if (id != formField.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(formField);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormFieldExists(formField.Id))
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

            ViewBag.Forms = _context.Forms.ToList(); // Repopulate forms if validation fails
            return View(formField);
        }

        // GET: FormField/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formField = await _context.FormFields
                .Include(f => f.Form)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (formField == null)
            {
                return NotFound();
            }

            return View(formField);
        }

        // POST: FormField/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var formField = await _context.FormFields.FindAsync(id);
            _context.FormFields.Remove(formField);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FormFieldExists(int id)
        {
            return _context.FormFields.Any(e => e.Id == id);
        }
    }
}
