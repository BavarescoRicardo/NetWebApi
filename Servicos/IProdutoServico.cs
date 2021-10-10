using NetWebApi.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetWebApi.Servicos
{
    public interface IProdutoServico
    {
        Task<IEnumerable<Produto>> GetProdutos();
        Task<Produto> GetProduto(int id);
        Task<IEnumerable<Produto>> GetProdutosByNome(string nome);
        Task CreateProduto(Produto produto);
        Task UpdateProduto(Produto produto);
        Task DeleteProduto(Produto produto);

    }
}
