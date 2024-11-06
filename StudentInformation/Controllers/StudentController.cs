using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentInformation.EF;
using StudentInformation.Models;

namespace StudentInformation.Controllers
{
    public class StudentController : Controller
    {
        StudentInformationEntities1 db = new StudentInformationEntities1();

        [HttpGet]
        public ActionResult Create()
        {
            return View(new Student());
        }

        [HttpPost]
        public ActionResult Create(Student s)
        {
            if (ModelState.IsValid)
            {
                db.StudentInfoes.Add(new StudentInfo
                {
                    Name = s.Name,
                    Id = s.ID,
                    Email = s.Email,
                    Dob = s.DOB
                });
                db.SaveChanges();
                return RedirectToAction("List", "Student");
            }
            return View(s);
        }

        public ActionResult List()
        {
            var data = db.StudentInfoes.ToList();

            return View(data);
        }
    }
}