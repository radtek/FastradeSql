using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class CatProdutoController : ControllerBase {
        fastradeContext _contexto = new fastradeContext ();

        //Get: Api/Catproduto
        [HttpGet]
        public async Task<ActionResult<List<CatProduto>>> Get () {

            var catprodutos = await _contexto.CatProduto.ToListAsync ();

            if (catprodutos == null) {
                return NotFound();
            }
            return catprodutos;
        }
        //Get: Api/Catproduto
        [HttpGet ("{id}")]
        public async Task<ActionResult<CatProduto>> Get(int id){
            var catproduto = await _contexto.CatProduto.FindAsync (id);

            if (catproduto == null){
                return NotFound ();
            }
            return catproduto;
        }
        //Post: Api/CatProduto
        [HttpPost]
        public async Task<ActionResult<CatProduto>> Post (CatProduto catproduto){
            try{
                await _contexto.AddAsync (catproduto);

                await _contexto.SaveChangesAsync();
                

                }catch (DbUpdateConcurrencyException){
                    throw;
            }
            return catproduto;
        }
        //Put: Api/CatProduto
        [HttpPut ("{id}")]
        public async Task<ActionResult> Put (int id, CatProduto catproduto){
            if(id != catproduto.IdCatProduto){
                
                return BadRequest ();
            }
            _contexto.Entry (catproduto).State = EntityState.Modified;
            try{
                await _contexto.SaveChangesAsync ();
            }catch (DbUpdateConcurrencyException){
                var catproduto_valido = await _contexto.CatProduto.FindAsync (id);

                if(catproduto_valido == null) {
                    return NotFound ();
                }else{
                    throw;
                }
            }
            return NoContent();
        }
         // DELETE api/CatProduto/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<CatProduto>> Delete(int id){

            var catproduto = await _contexto.CatProduto.FindAsync(id);
            if(catproduto == null){
                return NotFound();
            }

            _contexto.CatProduto.Remove(catproduto);
            await _contexto.SaveChangesAsync();

            return catproduto;
        }  
    }
}   