    using System.Collections.Generic;
    using System.Threading.Tasks;
    using backend.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    namespace backend.Controllers {
        [Route ("api/[controller]")]
        [ApiController]
        public class UsuarioController : ControllerBase {
            fastradeContext _contexto = new fastradeContext ();

            //Get: Api/Produtoreceita
            /// <summary>
            /// Aqui são todos os usuarios
            /// </summary>
            /// <returns>Lista de usuarios</returns>
            [HttpGet]
            [Authorize(Roles = "3")]
            public async Task<ActionResult<List<Usuario>>> Get () {

                var usuarios = await _contexto.Usuario.Include("IdEnderecoNavigation").Include("IdTipoUsuarioNavigation").ToListAsync();

                if (usuarios == null) {
                    return NotFound();
                }
                return usuarios;
            }
            //Get: Api/Produtoreceita
            /// <summary>
            /// Mostramos os dados de um usuario
            /// </summary>
            /// <param name="id"></param>
            /// <returns>Unico ID de um usuario</returns>
            [HttpGet ("{id}")]
            [Authorize(Roles = "3")]
            public async Task<ActionResult<Usuario>> Get(int id){
                var usuario = await _contexto.Usuario.Include("IdEnderecoNavigation").Include("IdTipoUsuarioNavigation").FirstOrDefaultAsync (e => e.IdUsuario == id);

                if (usuario == null){
                    return NotFound ();
                }
                return usuario;
            }
            //Post: Api/Usuario
            /// <summary>
            /// Enviamos dados de um usuario
            /// </summary>
            /// <param name="usuario"></param>
            /// <returns>Envia dados de um usuario</returns>
            [HttpPost]
            [Authorize(Roles = "3")]
            public async Task<ActionResult<Usuario>> Post (Usuario usuario){
                try{
                    await _contexto.AddAsync (usuario);

                    await _contexto.SaveChangesAsync();
                    

                    }catch (DbUpdateConcurrencyException){
                        throw;
                }
                return usuario;
            }
            //Put: Api/Usuario
            /// <summary>
            /// Alteramos dados de um usuario
            /// </summary>
            /// <param name="id"></param>
            /// <param name="usuario"></param>
            /// <returns>Envia dados de um usuario</returns>
            [HttpPut ("{id}")]
            [Authorize]
            public async Task<ActionResult> Put (int id, Usuario usuario){
                if(id != usuario.IdUsuario){
                    
                    return BadRequest ();
                }
                _contexto.Entry (usuario).State = EntityState.Modified;
                try{
                    await _contexto.SaveChangesAsync ();
                }catch (DbUpdateConcurrencyException){
                    var usuario_valido = await _contexto.Usuario.FindAsync (id);

                    if(usuario_valido == null) {
                        return NotFound ();
                    }else{
                        throw;
                    }
                }
                return NoContent();
            }
            // DELETE api/Usuario/id
            /// <summary>
            /// Excluimos dados de um usuario
            /// </summary>
            /// <param name="id"></param>
            /// <returns>Excluir dados de um usuario</returns>
            [HttpDelete("{id}")]
            [Authorize(Roles = "3")]
            public async Task<ActionResult<Usuario>> Delete(int id){

                var usuario = await _contexto.Usuario.FindAsync(id);
                if(usuario == null){
                    return NotFound();
                }

                _contexto.Usuario.Remove(usuario);
                await _contexto.SaveChangesAsync();

                return usuario;
            }  
        }
    }   