using Microsoft.EntityFrameworkCore;
using NetWebApi.Context;
using NetWebApi.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetWebApi.Servicos
{
    public class ProdutoServico : IProdutoServico
    {
        private readonly NetdbContext _context;

        public ProdutoServico(NetdbContext context)
        {
            _context = context;
        }

        public async Task CreateProduto(Produto produto)
        {
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProduto(Produto produto)
        {
            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
        }

        public async Task<Produto> GetProduto(int id)
        {
            try
            {
                var produto = await _context.Produtos.FindAsync(id);
                return produto;
            }
            catch
            {

                throw;
            }
        }

        public async Task<IEnumerable<Produto>> GetProdutos()
        {
            return await _context.Produtos.ToListAsync();
        }

        public async Task<IEnumerable<Produto>> GetProdutosByNome(string nome)
        {
            IEnumerable<Produto> produtos;
            if (!string.IsNullOrWhiteSpace(nome))
            {
                produtos = await _context.Produtos.Where(x => x.Nome.Contains(nome)).ToListAsync();
            }
            else
            {
                produtos = await GetProdutos();
            }
            return produtos;
        }

        public async Task UpdateProduto(Produto produto)
        {
            _context.Entry(produto).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
