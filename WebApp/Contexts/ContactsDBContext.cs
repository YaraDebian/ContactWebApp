using Microsoft.EntityFrameworkCore;
using WebApp.Models;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WebApp.Contexts
{
    public partial class ContactsDBContext : DbContext
    {
        public ContactsDBContext(DbContextOptions<ContactsDBContext> options) : base(options) { }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Log> Log { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=YARA-DEBIAN;Database=ContactsDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>()
                .HasIndex(c => c.Email)
                .IsUnique();

            modelBuilder.Entity<Log>()
                .HasOne<Contact>(l => l.Contact)
                .WithMany(a => a.Logs)
                .HasForeignKey(b => b.ContactId)
                .OnDelete(DeleteBehavior.Restrict);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
