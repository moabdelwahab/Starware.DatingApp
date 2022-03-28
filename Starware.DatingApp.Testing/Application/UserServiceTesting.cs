using Microsoft.VisualStudio.TestTools.UnitTesting;
using Starware.DatingApp.Application.Services;
using Starware.DatingApp.Testing.Mocking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starware.DatingApp.Testing.Application
{
    [TestClass]
    public class UserServiceTesting
    {

        [TestMethod]
        public async Task DeleteUserPhoto_ResponseOK()
        {
            //arrange 
            string photoUrl = "https://localhost:4200/image.png";
            var photoServive = new PhotoServiceMock();
            var userServive = new UserService(null, null, photoServive);
            
            //Act 
            var delete = await userServive.DeletePhoto("mohamed",photoUrl);
            var act = delete.StatusCode;
            var expect = System.Net.HttpStatusCode.OK;

            //Assert
            Assert.AreEqual(expect, act);
        }



    }
}
