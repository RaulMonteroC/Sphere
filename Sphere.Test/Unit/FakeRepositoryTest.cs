using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Sphere.Core;
using Sphere.Test.FakeDb;

namespace Sphere.Test.Unit
{
    [TestFixture]
    public class FakeRepositoryTest
    {
        private Repository<Person> repository;
        private Person fakePerson;

        [SetUp]
        public void SetUp()
        {
            repository = new FakeRepository<Person>();
            fakePerson = LoadEntityData();
        }

        [Test]
        public void AddEntity()
        {
            var total = repository.GetAll().Count();
            repository.Add(fakePerson);

            var totalAfterInsert = repository.GetAll().Count();

            Assert.Greater(totalAfterInsert, total);
        }

        [Test]
        public void UpdateEntity()
        {
            repository.Add(fakePerson);
            var personFromStorage = repository.GetAll().FirstOrDefault();
            var previousName = personFromStorage.Name;
            personFromStorage.Name = personFromStorage.Name + new Random().Next(0, 10);

            repository.Update(personFromStorage);

            Assert.AreNotEqual(personFromStorage.Name, previousName);
        }

        [Test]
        public void DeleteEntity()
        {
            repository.Add(fakePerson);
            var total = repository.GetAll().Count();
            repository.Delete(m => m.Name.Contains("Jhon"));

            var totalAfterDelete = repository.GetAll().Count();

            Assert.Less(totalAfterDelete, total);
        }

        [Test]
        public void GetEntity()
        {
            repository.Add(fakePerson);
            var personFromStorage = repository.Get(m => m.Name == "Jhon");

            Assert.NotNull(personFromStorage);
        }

        [Test]
        public void GetAllEntities()
        {
            repository.Add(fakePerson);
            repository.Add(fakePerson);
            repository.Add(fakePerson);

            var entities = repository.GetAll();

            Assert.Greater(entities.Count(), 0);
        }

        [Test]
        public void FindEntities()
        {
            repository.Add(fakePerson);
            var entities = repository.Find(m => m.Name == "Jhon");

            Assert.Greater(entities.Count(), 0);
        }        

        private Person LoadEntityData()
        {
            var person = new Person()
            {
                Id = 0,
                Name = "Jhon",
                LastName = "Doe",
                BirthDate = DateTime.Now,
                Email = "jhondoe@gmail.com"
            };

            return person;
        }
    }
}
