using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Principal;
using System.Security.Claims;
using System.Collections;
using System.Collections.Generic;
using GigHub.Controllers.APIs;
using Moq;
using GigHub.Core.Repositories;
using GigHub.Tests.Extensions;

namespace GigHub.Tests.Controllers.Api
{
    [TestClass]
    public class GigsControllerTests
    {
        private GigsController _controller;
        public GigsControllerTests()
        {
           

            var mockUoW = new Mock<IUnitOfWork>();
            _controller = new GigsController(mockUoW.Object);
            _controller.MockCurrentUser("1","user1@domain.com");

        }

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
