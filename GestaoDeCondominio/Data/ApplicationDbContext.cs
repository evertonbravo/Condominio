using System.Text;
using GestaoDeCondominio.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GestaoDeCondominio.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Morador> Moradores { get; set; }
        public DbSet<Apartamento> Apartamentos { get; set; }

    }
}
