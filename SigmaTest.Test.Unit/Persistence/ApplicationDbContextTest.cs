using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SigmaTest.Domain.Entities;
using SigmaTest.DataAccess;

namespace SigmaTest.Test.Unit.Persistence
{
    public class ApplicationDbContextTest
    {
        [Test]
        public void CanInsertshortnerIntoDatabasee()
        {
            using var context = new ApplicationDbContext();
            var shortner = new Shortner();
            context.Shortner.Add(shortner);
            Assert.AreEqual(EntityState.Added, context.Entry(shortner).State);
        }
    }
}
