using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;
using Worklio.Entities;
using Worklio.Services;

namespace Worklio.Tests
{
    [TestClass]
    public class CountyTest
    {
        [TestMethod]
        public void CountryTestMethod()
        {
            using (var serv = new CountryService())
            {
                var test = Task.Run(() => serv.GetAll()).Result;
                Assert.IsInstanceOfType(test, typeof(IList<Country>));
                Assert.IsNotNull(test);
            }
        }
    }
}