using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Data;
using EmployeeManagement.Models;

namespace EmployeeManagement.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DepartmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index()
        {
            return View(await _context.departments.ToListAsync());
                          
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.departments == null)
            {
                return NotFound();
            }

            var department = await _context.departments
                .FirstOrDefaultAsync(m => m.DepartmentId == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department department)
        {
            if (ModelState.IsValid)
            {
                _context.departments.Add(department);
                await _context.SaveChangesAsync();
				TempData["success"] = "Added Successfully";
				return RedirectToAction("Index");
            }
            return View(department);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.departments == null)
            {
                return NotFound();
            }

            var department = await _context.departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Department department)
        {
            if (id != department.DepartmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                 _context.Update(department);
                 await _context.SaveChangesAsync();
				 TempData["success"] = "Successfully Edited";
				 return RedirectToAction("Index");
            }
            return View(department);
        }

       
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.departments == null)
            {
                return NotFound();
            }

            var department = await _context.departments
                .FirstOrDefaultAsync(m => m.DepartmentId == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            var department = await _context.departments.FindAsync(id);
            if (department != null)
            {
                _context.departments.Remove(department);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

       
    }
}
