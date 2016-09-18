using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDeveloper.Model;
using WebDeveloper.Repository;

namespace WebDeveloper.Areas.Personnel.Controllers
{
    public class BusinessEntityAddressController : PersonBaseController<BusinessEntityAddress>
    {
        // GET: Personnel/BusinessEntityAddress

        public BusinessEntityAddressController(IRepository<BusinessEntityAddress> repository)
            :base(repository)
        {

        }

        public ActionResult Index()
        {
            return View();
        }
    }
}