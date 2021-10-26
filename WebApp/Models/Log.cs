using System;

namespace WebApp.Models
{
    public class Log
    {
        public int Id { get; set; }
        public int ContactId { get; set; }
        public string Field { get; set; }
        public string Previous { get; set; }
        public string New { get; set; }
        public DateTime Date { get; set; }
        public Contact Contact { get; set; }
    }
}
