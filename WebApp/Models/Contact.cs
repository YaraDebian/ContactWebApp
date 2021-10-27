using WebApp.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApp
{
    public class Contact
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Email { get; set; }
        [Required]
        public long PhoneNumber { get; set; }
        public ICollection<Log> Logs { get; set; }
    }
}
