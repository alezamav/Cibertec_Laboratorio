using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using WebDeveloper.API.Controllers;
using WebDeveloper.Areas.Personnel.Models;
using WebDeveloper.Model;
using WebDeveloper.Repository;
using Xunit;

namespace WebDeveloper.Tests.Controllers
{
    public class PersonAPIControllerTest
    {
        private PersonController controller;
        private IRepository<Person> _repository;
        private Mock<DbSet<Person>> personDbSetMock;
        private Mock<WebContextDb> webContextMock;

   

        [Fact(DisplayName = "ListAPIActionEmptyParametersTest")]
        public void ListAPIActionEmptyParametersTest()
        {
            ListConfigMockData();
            controller = new PersonController(_repository);
            var result = controller.List(null, null);

            var response = result as OkNegotiatedContentResult<IEnumerable<Person>>;
            var modelCount = response.Content.Count().Should().Be(10);
        }

        [Fact(DisplayName = "DetailAPIGetTest")]
        public void DetailAPIGetTest()
        {
            ListConfigMockData();
            controller = new PersonController(_repository);
            var result = controller.Details(2);

            var response = result as OkNegotiatedContentResult<Person>;
            var modelPerson = response.Content;

        }

        [Fact(DisplayName = "CreatePutTest")]
        public void CreatePutTest()
        {
            BasicConfigMockData();
            controller = new PersonController(_repository);
            var result = controller.Create(TestPersonOk());

            personDbSetMock.Verify(s => s.Add(It.IsAny<Person>()), Times.Once());
            webContextMock.Verify(c => c.SaveChanges(), Times.Once());
            
        }

        [Fact(DisplayName = "EditPostTest")]
        public void EditPostTest()
        {
            BasicConfigMockData();
            controller = new PersonController(_repository);
            var result = controller.Update(TestPersonEditOk());

            webContextMock.Verify(c => c.SaveChanges(), Times.Once());
        }

        [Fact(DisplayName = "DeleteTest")]
        public void DeleteTest()
        {
            ListConfigMockData();
            controller = new PersonController(_repository);
            var result = controller.Delete(TestPersonEditOk());

            webContextMock.Verify(c => c.SaveChanges(), Times.Once());
        }

        #region Configuration Values

        private Person TestPersonEditOk()
        {
            var person = new Person
            {
                PersonType = "SC",
                FirstName = "Test",
                LastName = "Test",
                EmailPromotion = 1
            };
            person.BusinessEntityID = 2;
            person.rowguid = Guid.NewGuid();
            person.ModifiedDate = DateTime.Now;
            person.BusinessEntity = new BusinessEntity
            {
                ModifiedDate = DateTime.Now,
                rowguid = person.rowguid,
                BusinessEntityID = 2
            };
            return person;
        }

        private Person TestPersonOk()
        {
            var person = new Person
            {
                PersonType = "SC",
                FirstName = "Test",
                LastName = "Test",
                EmailPromotion = 1
            };
            person.rowguid = Guid.NewGuid();
            person.ModifiedDate = DateTime.Now;
            person.BusinessEntity = new BusinessEntity
            {
                ModifiedDate = DateTime.Now,
                rowguid = person.rowguid
            };
            return person;
        }

        public void PersonMockList()
        {
            var persons = Enumerable.Range(1, 10).Select(i => new Person
            {
                BusinessEntityID= i,
                PersonType = "SC",
                FirstName = $"Name{i}",
                LastName = $"LastName{i}",
                ModifiedDate = DateTime.Now,
            }).AsQueryable();
            personDbSetMock = new Mock<DbSet<Person>>();
            personDbSetMock.As<IQueryable<Person>>().Setup(m => m.Provider).Returns(persons.Provider);
            personDbSetMock.As<IQueryable<Person>>().Setup(m => m.Expression).Returns(persons.Expression);
            personDbSetMock.As<IQueryable<Person>>().Setup(m => m.ElementType).Returns(persons.ElementType);
            personDbSetMock.As<IQueryable<Person>>().Setup(m => m.GetEnumerator()).Returns(() => persons.GetEnumerator());
        }

        private void ListConfigMockData()
        {
            personDbSetMock = new Mock<DbSet<Person>>();
            PersonMockList();

            webContextMock = new Mock<WebContextDb>();
            webContextMock.Setup(m => m.Person).Returns(personDbSetMock.Object);
            webContextMock.Setup(m => m.Set<Person>()).Returns(personDbSetMock.Object);

            _repository = new BaseRepository<Person>(webContextMock.Object);
            controller = new PersonController(_repository);
        }

        private void BasicConfigMockData()
        {
            personDbSetMock = new Mock<DbSet<Person>>();

            webContextMock = new Mock<WebContextDb>();
            webContextMock.Setup(m => m.Person).Returns(personDbSetMock.Object);
            webContextMock.Setup(m => m.Set<Person>()).Returns(personDbSetMock.Object);

            _repository = new BaseRepository<Person>(webContextMock.Object);
            controller = new PersonController(_repository);
        }

        #endregion



    }
}
