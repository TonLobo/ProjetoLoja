using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Loja.Models;

namespace Loja.Data
{
    public class LojaContext : DbContext
    {
        public LojaContext (DbContextOptions<LojaContext> options)
            : base(options)
        {
        }

        public DbSet<Carro> Carro { get; set; } = default!;
        public DbSet<Cliente> Cliente { get; set; } = default!;
        public DbSet<Desconto> Desconto { get; set; } = default!;
        public DbSet<Usuario> Usuario { get; set; } = default!;
        public DbSet<Venda> Venda { get; set; } = default!;
    }
}
