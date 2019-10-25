using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class ProdutoReceitaController : ControllerBase {
        fastradeContext _contexto = new fastradeContext ();

        //Get: Api/Produtoreceita
        /// <summary>
        /// Aqui são todos os produtos de uma receita
        /// </summary>
        /// <returns>Lista de produtos de uma receita</returns>
        [HttpGet]
        public async Task<ActionResult<List<ProdutoReceita>>> Get () {

            var produtoreceitas = await _contexto.ProdutoReceita.Include("IdProdutoNavigation").Include("IdReceitaNavigation").ToListAsync();

            if (produtoreceitas == null) {
                return NotFound();
            }
            return produtoreceitas;
        }
        //Get: Api/Produtoreceita
        /// <summary>
        /// Mostramos apenas uma ID de um produto receita
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Unico ID de um produto receita</returns>
        [HttpGet ("{id}")]
        public async Task<ActionResult<ProdutoReceita>> Get(int id){
            var produtoreceita = await _contexto.ProdutoReceita.Include("IdProdutoNavigation").Include("IdReceitaNavigation").FirstOrDefaultAsync (e => e.IdProdutoReceita == id);

            if (produtoreceita == null){
                return NotFound ();
            }
            return produtoreceita;
        }
        //Post: Api/ProdutoReceita
        /// <summary>
        /// Enviamos os dados de um produto receita
        /// </summary>
        /// <param name="produtoreceita"></param>
        /// <returns>Envia dados de um produto receita</returns>
        [HttpPost]
        public async Task<ActionResult<ProdutoReceita>> Post (ProdutoReceita produtoreceita){
            try{
                await _contexto.AddAsync (produtoreceita);

                await _contexto.SaveChangesAsync();
                

                }catch (DbUpdateConcurrencyException){
                    throw;
            }
            return produtoreceita;
        }
        //Put: Api/ProdutoReceita
        /// <summary>
        /// Alteramos dados de um produto receita
        /// </summary>
        /// <param name="id"></param>
        /// <param name="produtoreceita"></param>
        /// <returns>Alteração de dados produto receita</returns>
        [HttpPut ("{id}")]
        public async Task<ActionResult> Put (int id, ProdutoReceita produtoreceita){
            if(id != produtoreceita.IdProdutoReceita){
                
                return BadRequest ();
            }
            _contexto.Entry (produtoreceita).State = EntityState.Modified;
            try{
                await _contexto.SaveChangesAsync ();
            }catch (DbUpdateConcurrencyException){
                var produtoreceita_valido = await _contexto.ProdutoReceita.FindAsync (id);

                if(produtoreceita_valido == null) {
                    return NotFound ();
                }else{
                    throw;
                }
            }
            return NoContent();
        }
         // DELETE api/ProdutoReceita/id
         /// <summary>
         /// Excluimos dados de uma produto receita
         /// </summary>
         /// <param name="id"></param>
         /// <returns>Exclui dado de produto receita</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProdutoReceita>> Delete(int id){

            var produtoreceita = await _contexto.ProdutoReceita.FindAsync(id);
            if(produtoreceita == null){
                return NotFound();
            }

            _contexto.ProdutoReceita.Remove(produtoreceita);
            await _contexto.SaveChangesAsync();

            return produtoreceita;
        }  
    }
}   