using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Domains;
using backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories {
    public class UsuarioRepository : IUsuario {
        public async Task<Usuario> Alterar (Usuario usuario) {
            using (fastradeContext _contexto = new fastradeContext ()) {
                _contexto.Entry (usuario).State = EntityState.Modified;
                await _contexto.SaveChangesAsync ();
            }
            return usuario;

        }

        public async Task<Usuario> BuscarPorID (int id) {
            using (fastradeContext _contexto = new fastradeContext ()) {
                return await _contexto.Usuario.Include ("IdEnderecoNavigation").Include ("IdTipoUsuarioNavigation").FirstOrDefaultAsync (e => e.IdUsuario == id);
            }
        }

        public async Task<Usuario> Excluir (Usuario usuario) {
            using (fastradeContext _contexto = new fastradeContext ()) {
                _contexto.Usuario.Remove (usuario);
                await _contexto.SaveChangesAsync ();
                return usuario;
            }
        }

        public async Task<List<Usuario>> Listar () {
            using (fastradeContext _contexto = new fastradeContext ()) {
                return await _contexto.Usuario.Include ("IdEnderecoNavigation").Include ("IdTipoUsuarioNavigation").ToListAsync ();
            }
        }

        public async Task<Usuario> Salvar (Usuario usuario) {
            using (fastradeContext _contexto = new fastradeContext ()) {
                await _contexto.AddAsync (usuario);
                await _contexto.SaveChangesAsync ();
                return usuario;
            }
        }
    }
}