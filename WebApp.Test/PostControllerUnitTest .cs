using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebApp.Contexts;
using FluentAssertions;
using WebApp.Controllers;
using WebApp.Services;
using Xunit;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebApp.Test
{
    public class PostControllerUnitTest
    {
        ContactsDBContext _dBContext;
        private readonly ContactController _controller;
        private readonly IContactService _contactService;
        private readonly ILogsService _logsService;

        public PostControllerUnitTest()
        {
            var dbContextOptions = new DbContextOptionsBuilder<ContactsDBContext>()
                .UseSqlServer("Data Source=YARA-DEBIAN;Trusted_Connection=True;MultipleActiveResultSets=true")
                .Options;
            _dBContext = new ContactsDBContext(dbContextOptions);
            _logsService = new LogsService(_dBContext);
            _contactService = new ContactService(_dBContext, _logsService);
            _controller = new ContactController(_contactService, _logsService, _dBContext);
        }

        [Fact]
        public void Add_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var testItem = new Contact()
            {
                FirstName = "firstName",
                LastName = "lastName",
                Email = "yaradebian1@gmail.com",
                PhoneNumber = 123
            };
            var badResponse = _controller.Create(testItem);

            badResponse.Result.Should().BeOfType<BadRequestResult>(null);
        }
        [Fact]
        public void Add_ValidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            Contact testItem = new Contact()
            {
                FirstName = "firstName",
                LastName = "lastName",
                Email = "email4",
                PhoneNumber = 123
            };
            // Act
            var createdResponse = _controller.Create(testItem);
            // Assert
            Assert.IsType<Contact>(createdResponse.Value);
        }
        [Fact]
        public void Add_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            // Arrange
            Contact testItem = new Contact()
            {
                FirstName = "firstName",
                LastName = "lastName",
                Email = "email6",
                PhoneNumber = 123
            };
            // Act
            var createdResponse = _controller.Create(testItem);
            var item = createdResponse.Value as Contact;
            // Assert
            Assert.IsType<Contact>(item);
            Assert.Equal("firstName", item.FirstName);
            Assert.Equal("lastName", item.LastName);
            Assert.Equal("email6", item.Email);
            Assert.Equal(123, item.PhoneNumber);

        }

    }
}
