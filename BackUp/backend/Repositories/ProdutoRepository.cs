using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Domains;
using backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories {
    public class ProdutoRepository : IProduto {
        public async Task<Produto> Alterar (Produto produto) {
            using (fastradeContext _contexto = new fastradeContext ()) {
                _contexto.Entry (produto).State = EntityState.Modified;
                await _contexto.SaveChangesAsync ();
            }
            return produto;
        }

        public async Task<Produto> BuscarPorID (int id) { }

        public async Task<Produto> Excluir (Produto produto) { }

        public async Task<List<Produto>> Listar () { }

        public async Task<Produto> Salvar (Produto produto) { }
    }
}