using Microsoft.EntityFrameworkCore;
using Biblioteca.Models.Models;

namespace Biblioteca.WebApi
{
    public partial class Context : Microsoft.EntityFrameworkCore.DbContext
    {
        public Context() {  }

        public Context(DbContextOptions<Context> options) : base(options) {  }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {  }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Livros>(livro => {

                livro.ToTable("Livros");
                livro.HasKey(x => x.LivroId);

            });

            OnModelCreatingPartial(builder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
