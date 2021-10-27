using WebApp.Contexts;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using WebApp.Models;
using WebApp.Helpers;

namespace WebApp.Services
{
    public class ContactService : IContactService
    {
        ContactsDBContext dbContext;
        private readonly ILogsService _logsService;

        public ContactService(ContactsDBContext dbContext, ILogsService logsService)
        {
            this.dbContext = dbContext;
            _logsService = logsService;
        }

        public List<Contact> GetAll()
        {
            return dbContext.Contact.Include(c => c.Logs).ToList();
        }
        public Contact Get(int id)
        {
            return dbContext.Contact.Where(c => c.Id == id).FirstOrDefault();
        }
        public Contact Create(Contact contact)
        {
            if (contact != null)
            {
                try
                {
                    dbContext.Contact.Add(contact);
                    dbContext.SaveChanges();
                }
                catch(DbUpdateException e)
                {
                    Debug.WriteLine("Contact Email already exists");
                    return default;
                }
            }
            return contact;
        }

        public Contact Delete(Contact contact)
        {
            var logs = _logsService.GetLogs(contact.Id);
            try
            {
                dbContext.Entry(contact).State = EntityState.Deleted;
                dbContext.Log.RemoveRange(logs);
            }
            catch(ArgumentNullException e)
            {
                Debug.WriteLine("Contact does not exist");
            }
            dbContext.SaveChanges();
            return contact;
        }

        public Contact Update(int id, Contact contact)
        {
            _logsService.AddLogs(id, contact);
            dbContext.Entry(contact).State = EntityState.Modified;
            dbContext.SaveChanges();
            dbContext.Contact.Update(contact);
            dbContext.SaveChanges();
            dbContext.ChangeTracker.DetectChanges();
            return contact;
        }

        public PagedList<Contact> GetAppPaginated(ContactParams _params)
        {
            var contacts = this.GetAll().AsQueryable();
            return PagedList<Contact>.ToPagedList(contacts, _params.Pagenumber, _params.PageSize);
        }
    }
}
