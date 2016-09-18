using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebDeveloper.Filters;
using WebDeveloper.Model;
using WebDeveloper.Repository;

namespace WebDeveloper.Areas.Personnel.Controllers
{
    //[AuditControl]
    public class PersonController : PersonBaseController<Person>
    {
        // GET: Person
        // Address / BussinessEntity / BussinessEntityAddress / BussinessEntityContact / EmailAddress / PersonPhone
        //http://antoniogonzalezm.es/google-hacking-46-ejemplos-hacker-contrasenas-usando-google-enemigo-peor/
        //private PersonRepository _repository = new PersonRepository();   
       // private IRepository<Person> _repository;

        public PersonController (IRepository<Person> repository)
            :base(repository)
        {

        }
     
                 
        public ActionResult Index()
        {
            //return View(_repository.GetList());
            //return View(_repository.GetListBySize(15));
            return View(_repository.GetList().OrderByDescending(p => p.ModifiedDate).Take(15));
        }

        public ActionResult List(int? page,int? size)
        {
            if (!page.HasValue || !size.HasValue) {
                page = 1;
                size = 15;
            }
            return PartialView("_List",_repository.PaginatedList((x => x.ModifiedDate), page.Value, size.Value));
        }

        public int PageTotal(int rows)
        {
            //int CountPage = 0;
            //if (!size.HasValue)
            //{
            //    size = 15;
            //}
            //if (size <= 0) { size = 1; }
            //int CountRows = _repository.GetList().Count();
            //CountPage =Convert.ToInt32(Math.Round(Convert.ToDouble(CountRows) / Convert.ToDouble(size)));
            //return CountPage;
            if (rows <= 0) return rows;
            var count = _repository.GetList().Count;
            return count % rows > 0 ? (count / rows) + 1 : count / rows;
        }

        public ActionResult Create()
        {
            return PartialView("_Create");
        }

        [HttpPost]
        public ActionResult Create(Person person)
        {
            if (!ModelState.IsValid) return PartialView("_Create",person);
            person.rowguid = Guid.NewGuid();
            person.ModifiedDate = DateTime.Now;
            person.BusinessEntity = new BusinessEntity
            {
                rowguid = person.rowguid,
                ModifiedDate = person.ModifiedDate
            };

            _repository.Add(person);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public ActionResult Edit(int id)
        {
            //var person = _repository.GetById(id);
            var person = _repository.GetById(x=> x.BusinessEntityID==id);
            if (person == null) return RedirectToAction("Index");
            return PartialView("_Edit",person);
        }

        [HttpPost]
        public ActionResult Edit(Person person)
        {
            if (!ModelState.IsValid) return PartialView("_Edit", person);
            _repository.Update(person);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public ActionResult Delete(int id)
        {
            var person = _repository.GetById(x => x.BusinessEntityID == id);
            if (person == null) return RedirectToAction("Index");
            return PartialView("_Delete", person);
        }

        [HttpPost]
        public ActionResult Delete(Person person)
        {
            //  person = _repository.GetCompletePersonById(person.BusinessEntityID);   
           // person = _repository.GetById(x => x.BusinessEntityID == person.BusinessEntityID);
            _repository.Delete(person);
            return RedirectToAction("Index");
        }


        public ActionResult Details(int id)
        {
            var person = _repository.GetById(x => x.BusinessEntityID == id);
            if (person == null) return RedirectToAction("Index");
            return PartialView("_Details", person);
        }
    }
}