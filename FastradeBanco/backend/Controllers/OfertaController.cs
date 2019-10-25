using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class OfertaController : ControllerBase {
        fastradeContext _contexto = new fastradeContext ();
        
        //Get: Api/Oferta
        /// <summary>
        /// Aqui são todas as ofertas
        /// </summary>
        /// <returns>Lista de Ofertas</returns>
        [HttpGet]
        public async Task<ActionResult<List<Oferta>>> Get () {
            //Include("")
            var oferta = await _contexto.Oferta.Include("IdProdutoNavigation").Include("IdUsuarioNavigation").ToListAsync();
            if (oferta == null) {
                return NotFound();
            }
            return oferta;
        }
        //Get: Api/Oferta/2
        /// <summary>
        /// Mostramos uma oferta
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Mostramos unico ID de oferta</returns>
        [HttpGet ("{id}")]
        public async Task<ActionResult<Oferta>> Get(int id){
            var oferta = await _contexto.Oferta.Include("IdProdutoNavigation").Include("IdUsuarioNavigation").FirstOrDefaultAsync (e =>e.IdOferta ==id);

            if (oferta == null){
                return NotFound ();
            }
            return oferta;
        }
        //Post: Api/Oferta
        /// <summary>
        /// Enviamos mais dados de oferta
        /// </summary>
        /// <param name="oferta"></param>
        /// <returns>Envia uma oferta</returns>
        [HttpPost]
        public async Task<ActionResult<Oferta>> Post (Oferta oferta){
            try{
                await _contexto.AddAsync (oferta);

                await _contexto.SaveChangesAsync();
                

                }catch (DbUpdateConcurrencyException){
                    throw;
            }
            return oferta;
        }
        //Put: Api/Oferta
        /// <summary>
        /// Alteramos os dados de uma oferta
        /// </summary>
        /// <param name="id"></param>
        /// <param name="oferta"></param>
        /// <returns>Alteração de dados de uma oferta</returns>
        [HttpPut ("{id}")]
        public async Task<ActionResult> Put (int id, Oferta oferta){
            if(id != oferta.IdOferta){
                
                return BadRequest ();
            }
            _contexto.Entry (oferta).State = EntityState.Modified;
            try{
                await _contexto.SaveChangesAsync ();
            }catch (DbUpdateConcurrencyException){
                var oferta_valido = await _contexto.Receita.FindAsync (id);

                if(oferta_valido == null) {
                    return NotFound ();
                }else{
                    throw;
                }
            }
            return NoContent();
        }
         // DELETE api/Oferta/id
         /// <summary>
         /// Excluimos uma oferta
         /// </summary>
         /// <param name="id"></param>
         /// <returns>Excluir uma oferta</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Oferta>> Delete(int id){

            var oferta = await _contexto.Oferta.FindAsync(id);
            if(oferta == null){
                return NotFound();
            }

            _contexto.Oferta.Remove(oferta);
            await _contexto.SaveChangesAsync();

            return oferta;
        }  
    }
}