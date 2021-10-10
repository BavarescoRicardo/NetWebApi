using Microsoft.EntityFrameworkCore;
using NetWebApi.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetWebApi.Context
{
    public class NetdbContext : DbContext
    {
        public NetdbContext(DbContextOptions<NetdbContext> options) : base(options)
        {

        }

        public DbSet<Produto> Produtos { get; set; }

/*        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>().HasData(
            new Produto
            {
                Id = 1,
                Nome = "Net visualsutido",
                Quantidade = 1,
                Valor = 2,
                Opcao = 1
            });
        }*/
    }


    
}
