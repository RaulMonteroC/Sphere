using System.Data.Entity;

namespace Sphere.Test.FakeDb
{
    public class FakeContext : DbContext
    {
        public DbSet<Person> People { get; set; }

        public FakeContext() : base("Default"){ }        
    }
}
