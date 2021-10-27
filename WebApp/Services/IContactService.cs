using System.Collections.Generic;
using WebApp.Helpers;
using WebApp.Models;

namespace WebApp.Services
{
    public interface IContactService 
    {
        List<Contact> GetAll();
        PagedList<Contact> GetAppPaginated(ContactParams pp);
        Contact Get(int id);
        Contact Create(Contact contact);
        Contact Update(int id, Contact contact);
        Contact Delete(Contact contact);
    }
}
