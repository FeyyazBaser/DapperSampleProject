using DapperSampleProject.Models;
using DapperSampleProject.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace DapperSampleProject.Controllers
{
    public class DoctorController : Controller
    {
        private readonly DoctorRepository _doctorRepository;
        public DoctorController(DoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _doctorRepository.GetAllAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Save(Doctor doctor)
        {
            await _doctorRepository.AddAsync(doctor);
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Save()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Update(Doctor doctor)
        {
            doctor.CreatedDate = _doctorRepository.GetByIdAsync(doctor.Id).GetAwaiter().GetResult().CreatedDate;
            _doctorRepository.Update(doctor);
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Update(int id)
        {
            var doctor = await _doctorRepository.GetByIdAsync(id);
            return View(doctor);
        }
       
        public async Task<IActionResult> Remove(int id)
        {
            var doctor = await _doctorRepository.GetByIdAsync(id);
            _doctorRepository.Remove(doctor);
            return RedirectToAction(nameof(Index));

        }

    }
}
