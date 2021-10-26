using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Helpers;

namespace WebApp.Models
{
    public class ContactParams : QueryParameters
    {
        int ContactId { get; set; }
    }
}
