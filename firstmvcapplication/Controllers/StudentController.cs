using firstmvcapplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace firstmvcapplication.Controllers
{
    public class StudentController : Controller
    {
        private logic dbHelper = new logic();

        // GET: Student
        public ActionResult Index()
        {
            List<Student> students = dbHelper.GetStudents();
            return View(students);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        [HttpPost]
        public ActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                dbHelper.InsertStudent(student);
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int id)
        {
            List<Student> students = dbHelper.GetStudents();
            Student student = students.FirstOrDefault(s => s.Id == id);
            return View(student);
        }

        // POST: Student/Edit/5
        [HttpPost]
        public ActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                dbHelper.UpdateStudent(student);
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Student/Delete/5

        [HttpGet]
        public ActionResult Delete(int id,FormCollection formCollection)
        {
         var del = dbHelper.GetStudents().Find(s => s.Id == id);
            return View(del);
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            dbHelper.DeleteStudent(id);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var student1 = dbHelper.GetStudents().Find(emp => emp.Id == id);
           
            Student student = dbHelper.GetStudents().FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return HttpNotFound($"No student found with ID {id}");
            }
            return View(student1);
        }
    }

}