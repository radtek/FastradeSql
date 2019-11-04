using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Domains;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend_fastread.Controllers
{

//Definimos nossa rota do controller e dizemos que é um controller de API
[Route ("api/[controller]")]
[ApiController]

    public class EnderecoController : ControllerBase {
    fastradeContext _contexto = new fastradeContext ();

    //GET: api/Endereco
    /// <summary>
    /// Aqui são Todos endereços
    /// </summary>
    /// <returns>Lista de endeço</returns>
    [HttpGet]
    [Authorize(Roles = "3")]
    public async Task<ActionResult<List<Endereco>>> Get () {

        var Enderecos = await _contexto.Endereco.ToListAsync ();

        if(Enderecos == null){
            return NotFound ();
        }

        return Enderecos;
        
    }
    //GET: api/Endereco/2
    /// <summary>
    /// Aqui Pegamos apenas Um Endereço
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Pegamos apenas um endereço</returns>
    [HttpGet ("{id}")]
    [Authorize(Roles = "3")]
    public async Task<ActionResult<Endereco>> Get (int id){

        var Enderecos = await _contexto.Endereco.FindAsync (id);
        if(Enderecos == null){
            return NotFound ();
        }
        return Enderecos;
    }

    //POST api/Endereco
    /// <summary>
    /// Enviamos dados do endereço
    /// </summary>
    /// <param name="endereco"></param>
    /// <returns>Enviamos os dados do endereço</returns>
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Endereco>> Post (Endereco endereco) {

        try{
            //tratamos contra ataques de SQL Injection
            await _contexto.AddAsync (endereco);

            //Salvamos efetivamente o nosso objeto no banco de dados
            await _contexto.SaveChangesAsync ();
        }catch (DbUpdateConcurrencyException){
            throw;
        }
        return endereco;
    }
    /// <summary>
    /// Alteramos os dados do endereço
    /// </summary>
    /// <param name="id"></param>
    /// <param name="endereco"></param>
    /// <returns>Alteramos os dados do endereço</returns>
    [HttpPut ("{id}")]
    [Authorize]
    public async Task<ActionResult> Put (int id, Endereco endereco){

        //Se o id do objeto não existir
        //ele retorna erro 401

        if(id != endereco.IdEndereco){
            return BadRequest ();
        }
        //comparamos os atributos que foram modificado atraves do EF
        _contexto.Entry (endereco).State = EntityState.Modified;

        try{
            await _contexto.SaveChangesAsync();
       
        }catch(DbUpdateConcurrencyException){
            //verificamos se o objeto inserido realmente existe no banco
            var endereco_valido = await _contexto.Endereco.FindAsync (id);

            if(endereco_valido == null){
                return NotFound();

            }else{
                throw;
            }
        }
        //Nocontent = Retorna 204, sem nada
        return NoContent ();
        
    }
    //DELETE api/endereco/id
    /// <summary>
    /// Deletamos os dados do endereço
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Deletamos os dados do endereço</returns>
    [HttpDelete ("{id}")]
    [Authorize(Roles = "3")]
    public async Task<ActionResult<Endereco>> Delete (int id){

        var endereco = await _contexto.Endereco.FindAsync(id);
        if(endereco == null){
            return NotFound();
        }
        _contexto.Endereco.Remove(endereco);
        await _contexto.SaveChangesAsync();

        return endereco;
    }      
    }
}
