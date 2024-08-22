using Employee_Management_system.DB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Employee_Management_system.Controllers
{
    public class EmployeeController : Controller
    {

        // GET: Employee
        EmployeeDbEntities2 dbobj = new EmployeeDbEntities2();
        public ActionResult Index(employee obj )
        {
            
            if (obj != null)
            {
                return View(obj);
            }
            else

                return View();
        }

        [HttpPost]
        public ActionResult AddEmployee(employee model)
        {
            if (ModelState.IsValid)
            {
                employee obj = new employee();
                obj.Id = model.Id;
                obj.Firstname = model.Firstname;
                obj.LastName = model.LastName;
                obj.Email = model.Email;
                obj.phone = model.phone;
                obj.Position = model.Position;
                obj.Salary = model.Salary;

                if (model.Id == 0)
                {
                    dbobj.employees.Add(obj);
                    dbobj.SaveChanges();
                }
                else
                {
                    dbobj.Entry(obj).State = EntityState.Modified;
                    dbobj.SaveChanges();
                }

            }
            ModelState.Clear();
            return View("Index");
        }
        public ActionResult Details(string SearchValue)
        {
            var res = dbobj.employees.ToList();
           

            if (string.IsNullOrEmpty(SearchValue))
            {

                return View(res);
            }

            else
            {
                var se = dbobj.employees.Where(c => c.Firstname.Contains(SearchValue) || c.LastName.Contains(SearchValue)).ToList();

                return View(se);
            }
            return View(res);

        }
        public ActionResult Delete(int id)
        {
            var res = dbobj.employees.Where(c => c.Id == id).First();
            return View("Delete", res);

        }
        public ActionResult confiredDelete(int id)
        {

            var res2 = dbobj.employees.Where(c => c.Id == id).First();
            dbobj.employees.Remove(res2);
            dbobj.SaveChanges();
            var res1 = dbobj.employees.ToList();
            return View("Details", res1);


        }
    }
}