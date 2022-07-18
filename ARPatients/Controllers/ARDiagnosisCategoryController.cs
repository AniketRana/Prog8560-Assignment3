using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ARPatients.Models;

namespace ARPatients.Controllers
{
    public class ARDiagnosisCategoryController : Controller
    {
        private readonly PatientsContext _context;

        public ARDiagnosisCategoryController(PatientsContext context)
        {
            _context = context;
        }

        // GET: ARDiagnosisCategory
        // This is a master method that is used to call the appropriate page.
        // When you click on the Diagnosis tab in the menu, it will take you to the Diagnosis page and display the list.
        public async Task<IActionResult> Index()
        {
              return _context.DiagnosisCategories != null ? 
                          View(await _context.DiagnosisCategories.ToListAsync()) :
                          Problem("Entity set 'PatientsContext.DiagnosisCategories'  is null.");
        }

        // GET: ARDiagnosisCategory/Details/5
        //It will show the specific model's details 
        //To learn more about a specific Diagnosis model, click here.
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DiagnosisCategories == null)
            {
                return NotFound();
            }

            var diagnosisCategory = await _context.DiagnosisCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (diagnosisCategory == null)
            {
                return NotFound();
            }

            return View(diagnosisCategory);
        }

        // GET: ARDiagnosisCategory/Create
        // To create a new DiagnosisCategory
        public IActionResult Create()
        {
            return View();
        }

        // POST: ARDiagnosisCategory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // When we click on save button while creating new Diagnosis category this method is called
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] DiagnosisCategory diagnosisCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(diagnosisCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(diagnosisCategory);
        }

        // GET: ARDiagnosisCategory/Edit/5
        // To modify the particular DiagnosisCategory
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DiagnosisCategories == null)
            {
                return NotFound();
            }

            var diagnosisCategory = await _context.DiagnosisCategories.FindAsync(id);
            if (diagnosisCategory == null)
            {
                return NotFound();
            }
            return View(diagnosisCategory);
        }

        // POST: ARDiagnosisCategory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //to update DiagnosisCategory by id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] DiagnosisCategory diagnosisCategory)
        {
            if (id != diagnosisCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(diagnosisCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiagnosisCategoryExists(diagnosisCategory.Id))
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
            return View(diagnosisCategory);
        }

        // GET: ARDiagnosisCategory/Delete/5
        // Delete the particular DiagnosisUnit
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DiagnosisCategories == null)
            {
                return NotFound();
            }

            var diagnosisCategory = await _context.DiagnosisCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (diagnosisCategory == null)
            {
                return NotFound();
            }

            return View(diagnosisCategory);
        }

        // POST: ARDiagnosisCategory/Delete/5
        // To confirm delete particular DiagnosisUnit
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DiagnosisCategories == null)
            {
                return Problem("Entity set 'PatientsContext.DiagnosisCategories'  is null.");
            }
            var diagnosisCategory = await _context.DiagnosisCategories.FindAsync(id);
            if (diagnosisCategory != null)
            {
                _context.DiagnosisCategories.Remove(diagnosisCategory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //Exit from the DiagnosisCategory
        private bool DiagnosisCategoryExists(int id)
        {
          return (_context.DiagnosisCategories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
