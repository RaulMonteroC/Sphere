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
        private Person mockPerson;

        [SetUp]
        public void SetUp()
        {
            repository = new ContextRepository<Person>(new FakeContext());
            mockPerson = LoadEntityData();
        }

        [TearDown]
        public void TearDown()
        {
            var query = "truncate table people";
            repository.Exec(query, null);
        }

        [Test]
        public void CreateRepositoryFromGlobalContext()
        {
            var repo = new ContextRepository<Person>(new FakeContext());

            Assert.NotNull(repo);
            Assert.AreEqual(typeof(ContextRepository<Person>),repo.GetType());
        }

        [Test]
        public void CreateRepositoryFromNewContext()
        {
            SphereConfig.GlobalContext = new FakeContext();
            var repo = new ContextRepository<Person>();

            Assert.NotNull(repo);
            Assert.AreEqual(typeof(ContextRepository<Person>), repo.GetType());
        }

        [Test]
        public void CreateEntity()
        {
            repository.Add(mockPerson);
            Assert.AreNotEqual(0, mockPerson.Id);
        }

        [Test]
        public void UpdateEntity()
        {
            repository.Add(mockPerson);
            var existingPerson = repository.GetAll().FirstOrDefault();
            var previousName = existingPerson.Name;

            existingPerson.Name = existingPerson.Name + new Random().Next(0,10);
            repository.Update(existingPerson);

            Assert.AreNotEqual(previousName, existingPerson.Name);           
        }

        [Test]
        public void DeleteEntity()
        {
            repository.Add(mockPerson);
            var people = repository.GetAll();
            var total = people != null ? people.Count() : 0;

            var person = repository.GetAll().FirstOrDefault();
            repository.Delete(m => m.Id == person.Id);

            var totalAfterDelete = repository.GetAll() != null ? repository.GetAll().Count() : 0;

            Assert.AreNotEqual(total, totalAfterDelete);
        }

        [Test]
        public void FindEntities()
        {
            repository.Add(mockPerson);
            var jhons = repository.Find(m => m.Name.Contains("Jhon"));

            Assert.Greater(jhons.Count(), 0);
        }

        [Test]
        public void GetAllEntities()
        {
            repository.Add(mockPerson);
            var people = repository.GetAll();

            Assert.Greater(people.Count(), 0); 
        }

        [Test]
        public void GetEntity()
        {
            repository.Add(mockPerson);
            var personFromDb = repository.Get(m => m.Name == "Jhon");

            Assert.NotNull(personFromDb);
        }

        [Test]
        public void ExecActionQuery()
        {
            var query = "insert into people values('test','test',getDate(),'test@somewhere.com')";
            var total = repository.GetAll() != null ? repository.GetAll().Count() : 0;
            repository.Exec(query, null);

            var totalAfterInsert = repository.GetAll() != null ? repository.GetAll().Count() : 0;

            Assert.Greater(totalAfterInsert, total);
        }

        [Test]
        public void RunSql()
        {
            repository.Add(mockPerson);
            var query = "select top 5 name from people";
            repository.Run<string>(query);
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
