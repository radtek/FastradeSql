using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class ReceitaController : ControllerBase {
        fastradeContext _contexto = new fastradeContext ();

        //Get: Api/Receita
        [HttpGet]
        public async Task<ActionResult<List<Receita>>> Get () {

            var receitas = await _contexto.Receita.ToListAsync ();

            if (receitas == null) {
                return NotFound();
            }
            return receitas;    
        }
        //Get: Api/Receita
        [HttpGet ("{id}")]
        public async Task<ActionResult<Receita>> Get(int id){
            var receita = await _contexto.Receita.FindAsync (id);

            if (receita == null){
                return NotFound ();
            }
            return receita;
        }
        //Post: Api/Receita
        [HttpPost]
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
        [HttpPut ("{id}")]
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
        [HttpDelete("{id}")]
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