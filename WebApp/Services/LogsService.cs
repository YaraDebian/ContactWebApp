using Microsoft.EntityFrameworkCore;
using ObjectsComparer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Contexts;
using WebApp.Models;

namespace WebApp.Services
{
    public class LogsService : ILogsService
    {
        ContactsDBContext dBContext;
        public LogsService(ContactsDBContext dbContext)
        {
            this.dBContext = dbContext;
        }
        public void AddLogs(int id, Contact contact)
        {
            var oldContact = dBContext.Contact.Where(c => c.Id == id).AsNoTracking().FirstOrDefault();
            var comparer = new ObjectsComparer.Comparer<Contact>();

            //Compare objects  
            IEnumerable<Difference> differences;
            var isEqual = comparer.Compare(oldContact, contact, out differences);
            if (!isEqual)
            {
                Log log = new Log()
                {
                    ContactId = id,
                    Field = differences.ToList().Select(d => d.MemberPath).FirstOrDefault(),
                    Previous = differences.ToList().Select(d => d.Value1).FirstOrDefault(),
                    New = differences.ToList().Select(d => d.Value2).FirstOrDefault(),
                    Date = DateTime.Now
                };
                dBContext.Log.Add(log);
                dBContext.SaveChanges();
            }
            return;
        }

        public IEnumerable<Log> GetLogs(int contactId)
        {
            return dBContext.Log.Where(l => l.ContactId == contactId);
        }
    }
}
