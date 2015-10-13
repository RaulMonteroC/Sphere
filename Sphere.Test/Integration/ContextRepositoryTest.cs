using Sphere.Core;
using Sphere.Test.FakeDb;
using NUnit.Framework;
using System;

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
