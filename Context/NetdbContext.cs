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
        public DbSet<Usuario> Usuarios { get; set; }


        // Criar tabela
        /*        protected override void OnModelCreating(ModelBuilder modelBuilder)
                {
                    modelBuilder.Entity<Usuario>()
                        .ToTable("AspNetUsers", t => t.ExcludeFromMigrations());
                }*/



        // Popular tabela com dados
        /*        protected override void OnModelCreating(ModelBuilder modelBuilder)
                {
                    modelBuilder.Entity<Usuario>().HasData(
                     new Usuario
                     {
                         Id = 1,
                         Nome = "Ricardo bav",
                         Senha = "qwe123",
                         token = "",
                         Permissao = 3
                     });
                }*/
    }


}
