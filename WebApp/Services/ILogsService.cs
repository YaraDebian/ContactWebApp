using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Services
{
    public interface ILogsService
    {
        public void AddLogs(int id, Contact contact);
        public IEnumerable<Log> GetLogs(int contactId);
    }
}
