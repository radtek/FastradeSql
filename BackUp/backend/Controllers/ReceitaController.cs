using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class ReceitaController : ControllerBase {
        fastradeContext _contexto = new fastradeContext ();

        //Get: Api/Receita
        /// <summary>
        /// Aqui são todas as receitas
        /// </summary>
        /// <returns>Lista de receita</returns>
        [HttpGet]
        [Authorize(Roles = "3")]
        public async Task<ActionResult<List<Receita>>> Get () {

            var receitas = await _contexto.Receita.ToListAsync ();

            if (receitas == null) {
                return NotFound();
            }
            return receitas;    
        }
        //Get: Api/Receita
        /// <summary>
        /// Mostramos uma unica receita
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Unico ID de uma receita</returns>
        [HttpGet ("{id}")]
        [Authorize(Roles = "3")]

        public async Task<ActionResult<Receita>> Get(int id){
            var receita = await _contexto.Receita.FindAsync (id);

            if (receita == null){
                return NotFound ();
            }
            return receita;
        }
        //Post: Api/Receita
        /// <summary>
        /// Enviamos dados de uma receita
        /// </summary>  
        /// <param name="receita"></param>
        /// <returns>Envia dados de uma receita</returns>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Receita>> Post (Receita receita){
            try{
                await _contexto.AddAsync (receita);

                await _contexto.SaveChangesAsync();
                

                }catch (DbUpdateConcurrencyException){
                    throw;
            }
            return receita;
        }
        //Put: Api/Receita
        /// <summary>
        /// Alteramos dados de uma receita
        /// </summary>
        /// <param name="id"></param>
        /// <param name="receita"></param>
        /// <returns>Alteração de dados de uma receita</returns>
        [HttpPut ("{id}")]
        [Authorize(Roles = "3")]
        public async Task<ActionResult> Put (int id, Receita receita){
            if(id != receita.IdReceita){
                
                return BadRequest ();
            }
            _contexto.Entry (receita).State = EntityState.Modified;
            try{
                await _contexto.SaveChangesAsync ();
            }catch (DbUpdateConcurrencyException){
                var receita_valido = await _contexto.Receita.FindAsync (id);

                if(receita_valido == null) {
                    return NotFound ();
                }else{
                    throw;
                }
            }
            return NoContent();
        }
         // DELETE api/Receita/id
         /// <summary>
         /// Excluimos dados de uma receita
         /// </summary>
         /// <param name="id"></param>
         /// <returns>Exclui dados de uma receita</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "3")]
        public async Task<ActionResult<Receita>> Delete(int id){

            var receita = await _contexto.Receita.FindAsync(id);
            if(receita == null){
                return NotFound();
            }

            _contexto.Receita.Remove(receita);
            await _contexto.SaveChangesAsync();

            return receita;
        }  
    }
}   