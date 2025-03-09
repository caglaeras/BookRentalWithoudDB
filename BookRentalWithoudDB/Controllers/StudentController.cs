using BookRentalWithoudDB.Data;
using BookRentalWithoudDB.Models;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookRentalWithoudDB.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentRespository _repository;

        public StudentController(StudentRespository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            StudentRespository repository = new StudentRespository();

            var students=repository.GetAllStudents();
            return View(students);
        }

        public IActionResult Detail(int id)
        {
            StudentRespository repository = new StudentRespository();
            var student = repository.GetStudent(id);
            if(student==null)
            {
                return NotFound();
            } else
            {
                return View(student);
            }

        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            StudentRespository respository = new StudentRespository();
            respository.Insert(student);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _repository.Delete(id);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var student = _repository.GetStudent(id);
            if (student == null)
            {
                return NotFound();
            }
            return View("Create", student); // Reuse Create view for editing
        }

        [HttpPost]
        public IActionResult Edit(Student student)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", student); // Show the form again with validation errors
            }

            _repository.Update(student);
            return RedirectToAction("Index");
        }

    }
}
