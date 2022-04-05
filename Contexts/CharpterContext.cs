using Charpter.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Charpter.WebApi.Contexts
{
    public class CharpterContext : DbContext
    {
        public CharpterContext()
        {
        }

        public CharpterContext(DbContextOptions<CharpterContext> options) : base(options)
        {
        }

        // vamos utilizar esse metodo para configurar o banco de dados
        protected override void
            OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //cada provedor tem sua sintaxe para especificaco
                optionsBuilder.UseSqlServer(
                    "Server=localhost; Database=chapter; User Id=sa; Password=ADMIN@admin; Trusted_Connection=false; MultipleActiveResultSets=true;");
            }
        }

        //dbset representa as entidades que serao utilizadas
        public DbSet<Livro>? Livros { get; set; }
    }
}