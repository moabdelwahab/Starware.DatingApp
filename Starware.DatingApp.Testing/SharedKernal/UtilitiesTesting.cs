using Microsoft.VisualStudio.TestTools.UnitTesting;
using Starware.DatingApp.SharedKernal.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starware.DatingApp.Testing.SharedKernal
{
    [TestClass]
    public class UtilitiesTesting
    {
        [TestMethod]
        public void GetAgeFromDate_Date_24094991_Return_30()
        {
            //Arrange 
            var birthDate = new DateTime(1991, 09, 24);

            //Act 
            var actual = birthDate.GetAgeFromDate();
            var expected = 30;
            
            //Assert
            Assert.AreEqual(actual, expected);
        }
    }
}
