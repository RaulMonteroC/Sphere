using Sphere.Core;
using Sphere.Test.FakeDb;
using NUnit.Framework;
using System;
using System.Linq;

namespace Sphere.Test.Integration
{
    [TestFixture]
    public class ContextRepositoryTest
    {
        private Repository<Person> repository;
        private Person person;

        [SetUp]
        public void SetUp()
        {
            repository = new ContextRepository<Person>(new FakeContext());
            person = LoadEntityData();
        }

        [Test]
        public void CreateEntity()
        {
            repository.Add(person);
            Assert.AreNotEqual(0, person.Id);
        }

        [Test]
        public void UpdateEntity()
        {
            var existingPerson = repository.GetAll().FirstOrDefault();
            var previousName = existingPerson.Name;

            existingPerson.Name = existingPerson.Name + new Random().Next(0,10);
            repository.Update(existingPerson);

            Assert.AreNotEqual(previousName, existingPerson.Name);           
        }

        [Test]
        public void GetAllEntities()
        {
            var people = repository.GetAll();

            Assert.Greater(people.Count(), 0); 
        }

        [Test]
        public void GetEntity()
        {
            var person = repository.Get(m => m.Name == "Jhon");

            Assert.NotNull(person);
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
