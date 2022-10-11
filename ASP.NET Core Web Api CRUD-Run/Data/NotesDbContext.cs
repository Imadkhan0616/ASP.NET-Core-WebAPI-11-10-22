using ASP.NET_Core_Web_Api_CRUD_Run.Model.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_Core_Web_Api_CRUD_Run.Data
{
    public class NotesDbContext : DbContext
    {
        public NotesDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Note> Notes { get; set; }
    }
}
