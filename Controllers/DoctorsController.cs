using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hospital_Project.Data;
using Hospital_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Hospital_Project.Models.ModelView;
using System.Globalization;

namespace Hospital_Project.Controllers
{
    [Authorize]
    public class DoctorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DoctorsController(ApplicationDbContext context)
        {
            _context = context;
        }
        //GET
        public IActionResult FindBy()
        {
            ViewData["Title"] = "Search";
            return View(new DoctorModelView {});
        }
        //POST
        [HttpPost]
        public async Task<IActionResult> FindBy(DoctorModelView doctorModelView)
        {
            ViewData["Title"] = "Search";
            if (ModelState.IsValid)
            {
                doctorModelView.Patients = await _context.tblPatients.Where(
                    b => b.PatientFullName.ToLower().Contains(doctorModelView.PatientFullName.ToLower())
                    || b.FileNo == doctorModelView.FileNo).ToListAsync();
                
            }
            return View(doctorModelView);
        }

        public async Task<IActionResult> masterDetail(int id)
        {
            if (id == 0) {
                id = await _context.tblDoctors.MinAsync(d => d.DoctorId);
            }
            Doctor? doctor = await _context.tblDoctors!
                .Include(d => d!.Appointments)!
                .ThenInclude(dp => dp.Patient)
                .FirstOrDefaultAsync(d=>d.DoctorId == id);
            if (doctor == null)
            {
                return NotFound();
            }
            ViewData["TotalPatients"] = doctor.Appointments!.Count();
            ViewData["Title"] = $"Doctor: {doctor.DoctorFullName}";
            List<int> ages = new List<int>();
            foreach (var dp in doctor.Appointments!)
            {
                var dob = dp.Patient!.DateOfBirth;
                if (dob.HasValue)
                {
                    int age = DateTime.Today.Year - dob.Value.Year;
                    if (DateTime.Today < dob.Value.AddYears(age)) age--;
                    ages.Add(age);
                }
                else
                {
                    ages.Add(0); // Or any default/placeholder for unknown age
                }
            }

            ViewData["Ages"] = ages;
            return View(doctor);
        }





        public async Task<IActionResult> Report()
        {
            ViewData["Title"] = "Doctor Appointments Report";

            if (await _context.tblDoctors.CountAsync() > 0)
            {
                ICollection<Doctor> report = await _context.tblDoctors!
                    .Include(d => d.Appointments)!
                        .ThenInclude(a => a.Patient).OrderBy(e=>e.DoctorId).ToListAsync();
                ViewBag.report= report;
                return View();
            }
            return RedirectToAction(nameof(Index));
        }


        // GET: Doctors
        public async Task<IActionResult> Index(int SortBy)
        {
            switch (SortBy)
            {
                case 1:
                    return View(await _context.tblDoctors.OrderBy(b => b.DoctorFullName).ToListAsync());
                case 2:
                    return View(await _context.tblDoctors.OrderByDescending(b => b.DoctorFullName).ToListAsync());
                case 3:
                    return View(await _context.tblDoctors.OrderBy(b => b.YearsOfExperience).ToListAsync());
                case 4:
                    return View(await _context.tblDoctors.OrderByDescending(b => b.YearsOfExperience).ToListAsync());
                case 5:
                    return View(await _context.tblDoctors.OrderBy(b => b.Age).ThenBy(b => b.DoctorFullName).ToListAsync());
                case 6:
                    return View(await _context.tblDoctors.OrderByDescending(b => b.Age).ThenByDescending(b => b.DoctorFullName).ToListAsync());
                default:
                    return View(await _context.tblDoctors.OrderBy(b => b.DoctorFullName).ToListAsync());
            }
        }

        // GET: Doctors/Details/5
        [Route("Details/{id:int}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _context.tblDoctors
                .FirstOrDefaultAsync(m => m.DoctorId == id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // GET: Doctors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Doctors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DoctorId,DoctorFullName,Age,Speclization,DoctorEmail,DoctorPhoneNumber,YearsOfExperience")] Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(doctor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(doctor);
        }

        // GET: Doctors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _context.tblDoctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }

        // POST: Doctors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DoctorId,DoctorFullName,Age,Speclization,DoctorEmail,DoctorPhoneNumber,YearsOfExperience")] Doctor doctor)
        {
            if (id != doctor.DoctorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doctor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorExists(doctor.DoctorId))
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
            return View(doctor);
        }
        [Authorize(Roles = "Admin")]
        // GET: Doctors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _context.tblDoctors
                .FirstOrDefaultAsync(m => m.DoctorId == id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // POST: Doctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var doctor = await _context.tblDoctors.FindAsync(id);
            if (doctor != null)
            {
                _context.tblDoctors.Remove(doctor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoctorExists(int id)
        {
            return _context.tblDoctors.Any(e => e.DoctorId == id);
        }
    }
}
