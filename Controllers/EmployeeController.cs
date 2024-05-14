using EmployeeManagement.Data;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;


namespace EmployeeManagement.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _db;
        public EmployeeController(ApplicationDbContext db)
        {
            _db = db;
        }

        private byte[] ConvertImageToByteArray(string imagePath)
        {
            Image image = Image.FromFile(imagePath);
            var ms = new MemoryStream();

            image.Save(ms, format: ImageFormat.Jpeg);

            return ms.ToArray();
        }



        // GET: EmployeeController
        public ActionResult Index()
        {
            List<Employee> employees = _db.employees.Include(e => e.Department).ToList();
            return View(employees);
        }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  
        // GET: EmployeeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EmployeeController/Create
        public ActionResult Create()
        {
            var emp_dept = new Emp_Dept
            {
                employee = new Employee(),
                departments = _db.departments.ToList(),
            };


            return View(emp_dept);
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Employee employee, IFormFile Pfile)
        {
            byte[] fileData = null;
            if (Pfile != null && Pfile.Length != 0)
            {
                using (var ms = new MemoryStream())
                {
                    await Pfile.CopyToAsync(ms);
                    fileData = ms.ToArray();
                }

                employee.Picture = fileData;
            }else
            {
                employee.Picture = ConvertImageToByteArray("Assets\\no-profile-pic-icon-7.jpg");
            }

            
            if (ModelState.IsValid)
            {
                await _db.employees.AddAsync(employee);
                await _db.SaveChangesAsync();
                TempData["success"] = "Added Successfully";
				return RedirectToAction("Index");
            }

            var emp_dept = new Emp_Dept
            {
                employee = employee,
                departments = _db.departments.ToList(),
            };

            return View(emp_dept);
        }

       
        public async Task<ActionResult> Edit(int? id)
        {

            if (id == null || id == 0)
            {
                return NotFound();
            }
            Employee emp = await _db.employees.FirstOrDefaultAsync(e => e.EmployeeId == id);
         
            if (emp == null)
            {
                return NotFound();
            }

            if (emp != null) 
            {
                var emp_dept = new Emp_Dept
                {
                    employee = emp,
                    departments = _db.departments.ToList(),
                };

                return View(emp_dept);
            }
            

            return View("Index");
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Employee employee, IFormFile Pfile)
        {
            
                byte[] fileData = null;
                if (Pfile != null && Pfile.Length != 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        await Pfile.CopyToAsync(ms);
                        fileData = ms.ToArray();
                    }

                    employee.Picture = fileData;
                }else
                {
                    Employee emp1 = await _db.employees.AsNoTracking().FirstOrDefaultAsync(e => e.EmployeeId == employee.EmployeeId);
                    employee.Picture = emp1.Picture;
                }

            

            if (ModelState.IsValid)
            {
                try
                {
                    _db.employees.Update(employee);
                    await _db.SaveChangesAsync();
                    TempData["success"] = "Successfully Edited";
                    return RedirectToAction("Index");
                }
                catch
                {

                }
                
            }

            var emp_dept = new Emp_Dept
            {
                employee = employee,
                departments = _db.departments.ToList(),
            };

            return View(emp_dept);
        }

        
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Employee emp = await _db.employees.FirstOrDefaultAsync(e => e.EmployeeId == id);
            if (emp == null)
            {
                return NotFound();
            }

            TempData["empName"] = emp.FirstName + " " + emp.LastName;
            TempData["empId"] = emp.EmployeeId;
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(int id)
        {
            Employee emp = await _db.employees.FirstOrDefaultAsync(e => e.EmployeeId == id);
            _db.employees.Remove(emp);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
