using DapperSampleProject.Models;
using DapperSampleProject.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Numerics;

namespace DapperSampleProject.Controllers
{
    public class PatientController : Controller
    {
        private readonly PatientRepository _patientRepository;
        private readonly DoctorRepository _doctorRepository;
        public PatientController(PatientRepository patientRepository, DoctorRepository doctorRepository)
        {
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _patientRepository.GetAllAsync());
        }

        public async Task<IActionResult> Save()
        {
            var doctors = await _doctorRepository.GetAllAsync();

            ViewBag.doctors = new SelectList(doctors, "Id", "FullName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Save(Patient patient)
        {
            await _patientRepository.AddAsync(patient);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            var patient = await _patientRepository.GetByIdAsync(id);

            var doctors = await _doctorRepository.GetAllAsync();

            ViewBag.doctors = new SelectList(doctors, "Id", "FullName");

            return View(patient);
        }

        [HttpPost]
        public IActionResult Update(Patient patient)
        {
            patient.CreatedDate = _doctorRepository.GetByIdAsync(patient.DoctorId).GetAwaiter().GetResult().CreatedDate;
            _patientRepository.Update(patient);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(Patient patient)
        {
            _patientRepository.Remove(patient);
            return RedirectToAction(nameof(Index));
        }
    }
}
