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
    public class TipoUsuarioController : ControllerBase {
    fastradeContext _contexto = new fastradeContext ();

    //GET: api/TipoUsuario
    /// <summary>
    /// Aqui são todos os Tipos de Usuario
    /// </summary>
    /// <returns>Lista de tipo de usuario</returns>
    [HttpGet]
    [Authorize(Roles = "3")]   
    public async Task<ActionResult<List<TipoUsuario>>> Get () {

        var TipoUsuarios = await _contexto.TipoUsuario.ToListAsync ();

        if(TipoUsuarios == null){
            return NotFound ();
        }

        return TipoUsuarios;
        
    }
    //GET: api/TipoUsuario/2
    /// <summary>
    /// Pegamos um tipo de usuario
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Unico ID de um tipo de usuario</returns>
    [HttpGet ("{id}")]
    [Authorize(Roles = "3")]
    public async Task<ActionResult<TipoUsuario>> Get (int id){

        var TipoUsuarios = await _contexto.TipoUsuario.FindAsync (id);
        if(TipoUsuarios == null){
            return NotFound ();
        }
        return TipoUsuarios;
    }

    //POST api/TipoUsuario
    /// <summary>
    /// Envia dados de tipo de usuario
    /// </summary>
    /// <param name="tipousuario"></param>
    /// <returns>Envia dados de tipo de usuario</returns>
    [HttpPost]
    [Authorize(Roles = "3")]
    public async Task<ActionResult<TipoUsuario>> Post (TipoUsuario tipousuario) {

        try{
            //tratamos contra ataques de SQL Injection
            await _contexto.AddAsync (tipousuario);

            //Salvamos efetivamente o nosso objeto no banco de dados
            await _contexto.SaveChangesAsync ();
        }catch (DbUpdateConcurrencyException){
            throw;
        }
        return tipousuario;
    }
    /// <summary>
    /// Alteramos dados de tipo de usuario
    /// </summary>
    /// <param name="id"></param>
    /// <param name="tipousuario"></param>
    /// <returns>Alteração de dados de tipo de usuario</returns>
    [HttpPut ("{id}")]
    [Authorize(Roles = "3")]
    public async Task<ActionResult> Put (int id, TipoUsuario tipousuario){

        //Se o id do objeto não existir
        //ele retorna erro 401

        if(id != tipousuario.IdTipoUsuario){
            return BadRequest ();
        }
        //comparamos os atributos que foram modificado atraves do EF
        _contexto.Entry (tipousuario).State = EntityState.Modified;

        try{
            await _contexto.SaveChangesAsync();
       
        }catch(DbUpdateConcurrencyException){
            //verificamos se o objeto inserido realmente existe no banco
            var tipousuario_valido = await _contexto.TipoUsuario.FindAsync (id);

            if(tipousuario_valido == null){
                return NotFound();

            }else{
                throw;
            }
        }
        //Nocontent = Retorna 204, sem nada
        return NoContent ();
        
    }
    //DELETE api/tipousuario/id
    /// <summary>
    /// Excluimos dados de tipo de usuario
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Excluir dados de tipo de usuario</returns>
    [HttpDelete ("{id}")]
    [Authorize(Roles = "3")]
    public async Task<ActionResult<TipoUsuario>> Delete (int id){

        var tipousuario = await _contexto.TipoUsuario.FindAsync(id);
        if(tipousuario == null){
            return NotFound();
        }
        _contexto.TipoUsuario.Remove(tipousuario);
        await _contexto.SaveChangesAsync();

        return tipousuario;
    }      
    }
}
