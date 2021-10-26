using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WebApp.Contexts;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly ILogsService _logsService;

        ContactsDBContext _dBContext;
        public ContactController(IContactService contactService, ILogsService logsService, ContactsDBContext dBContext)
        {
            _contactService = contactService;
            _logsService = logsService;
            _dBContext = dBContext;
        }

        [HttpGet]
        public IEnumerable<Contact> GetAll()
        {
            return _contactService.GetAll();
        }

        [HttpGet("~/api/contact/getPaginated")]
        [Route("[action]")]
        //[Route("/getPaginated")]

        //get api paginated and filtered
        public IEnumerable<Contact> Getall([FromQuery] ContactParams _params)
        {

            var contacts = _contactService.GetAppPaginated(_params);
            var metadata = new
            {
                contacts.TotalCount,
                contacts.PageSize,
                contacts.CurrentPage,
                contacts.TotalPages,
                contacts.HasNext,
                contacts.HasPrevious
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return contacts;
        }

        [HttpGet("{id}")]
        public ActionResult<Contact> Get(int id)
        {
            return _contactService.Get(id);
        }

        [HttpPost]
        public ActionResult<Contact> Create(Contact contact)
        {
            bool emailExists = _dBContext.Contact.Any(c => c.Email == contact.Email);
            if (emailExists)
            {
                return BadRequest();
            }
            return _contactService.Create(contact);
        }

        [HttpPut("{id}")]
        public ActionResult<Contact> Update(int id, Contact contact)
        {
            Contact toUpdate = _dBContext.Contact
                .AsNoTracking()
                .FirstOrDefault(c => c.Id == id);
            if (toUpdate == null)
                return NotFound();
            return _contactService.Update(id, contact);
        }

        [HttpDelete("{id}")]
        public ActionResult<Contact> Delete(int id)
        {
            Contact toDelete = _dBContext.Contact
                .AsNoTracking()
                .FirstOrDefault(c => c.Id == id);
            if(toDelete == null)
                return NotFound();
            return _contactService.Delete(toDelete);
        }

        [HttpGet("log/{id}")]
        public IEnumerable<Log> GetLogs(int id)
        {
            return _logsService.GetLogs(id);
        }
    }
}
