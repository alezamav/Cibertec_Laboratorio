﻿using System;
using System.Collections.Generic;
using System.Linq;
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

     
                 
        public ActionResult Index()
        {
            //return View(_repository.GetList());
            //return View(_repository.GetListBySize(15));
            return View(_repository.GetList().OrderByDescending(p => p.ModifiedDate).Take(15));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Person person)
        {
            if (!ModelState.IsValid) return View(person);
            person.rowguid = Guid.NewGuid();
            person.ModifiedDate = DateTime.Now;
            person.BusinessEntity = new BusinessEntity
            {
                rowguid = person.rowguid,
                ModifiedDate = person.ModifiedDate
            };

            _repository.Add(person);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            //var person = _repository.GetById(id);
            var person = _repository.GetById(x=> x.BusinessEntityID==id);
            if (person == null) return RedirectToAction("Index");
            return View(person);
        }

        [HttpPost]
        public ActionResult Edit(Person person)
        {
            if (!ModelState.IsValid) return View(person);
            _repository.Update(person);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var person = _repository.GetById(x => x.BusinessEntityID == id);
            if (person == null) return RedirectToAction("Index");
            return View(person);
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
            return View(person);
        }
    }
}