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

namespace WebApp.Test
{
    public class GetControllerUnitTest
    {
        ContactsDBContext _dBContext;
        private readonly ContactController _controller;
        private readonly IContactService _contactService;
        private readonly ILogsService _logsService;

        public GetControllerUnitTest()
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
        public void Get_WhenCalled_ReturnsOkResult()
        {
            var okResult = _controller.GetAll();
            Assert.IsType<List<Contact>>(okResult);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            var okResult = _controller.GetAll() as List<Contact>;
            var items = Assert.IsType<List<Contact>>(okResult.ToList());
            Assert.Equal(5, items.Count);
        }

    }
}
