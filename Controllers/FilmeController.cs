using AutoMapper;
using FilmesWebApi.Data;
using FilmesWebApi.Data.Dtos;
using FilmesWebApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FilmesWebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmeController : ControllerBase
{

    private FilmeContext _context;
    private IMapper _mapper;

    public FilmeController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Adiciona um filme ao banco de dados
    /// </summary>
    /// <param name="filmeDto">Objeto com os campos necessários para criação de um filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AddFilme([FromBody]CreateFilmeDto createFilmeDto)
    {
        Filme filme = _mapper.Map<Filme>(createFilmeDto);
        _context.Filmes.Add(filme);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetFilmeById), new {id = filme.Id}, filme);
    }

    /// <summary>
    /// Recupera todos os filmes do banco de dados
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<ReadFilmeDto> GetAllFilmes([FromQuery] int skip, [FromQuery] int take = 10)
    {
        return _mapper.Map<List<ReadFilmeDto>>(_context.Filmes.Skip(skip).Take(take));
    }

    /// <summary>
    /// Recupera um filme do banco de dados
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetFilmeById(int Id)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == Id);
        var filmeDto = _mapper.Map<ReadFilmeDto>(filme);
        return filme == null ? NotFound() : Ok(filmeDto);
    }

    /// <summary>
    /// atualisa um filme do banco de dados
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult UpdateFilme(int Id, [FromBody]UpdateFilmeDto updateFilmeDto)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == Id);
        
        if (filme == null) return NotFound();
        
        _mapper.Map(updateFilmeDto, filme);
        _context.SaveChanges();
        
        return filme == null ? NotFound() : Ok(filme);
        //return NoContent();
    }

    /// <summary>
    /// atualisa um filme do banco de dados
    /// </summary>
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult UpdateFilmePatch(int Id, JsonPatchDocument<UpdateFilmeDto> patch)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == Id);
        if (filme == null) return NotFound();

        var filmeToUpdate = _mapper.Map<UpdateFilmeDto>(filme);

        patch.ApplyTo(filmeToUpdate, ModelState);
        if(!TryValidateModel(filmeToUpdate))
        {
            return ValidationProblem(ModelState);
        }

        _mapper.Map(filmeToUpdate, filme);
        _context.SaveChanges();

        return filme == null ? NotFound() : Ok(filme);
        //return NoContent();
    }

    /// <summary>
    /// deleta um filme do banco de dados
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult DeleteFilme(int Id){

        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == Id);
        if(filme == null) return NotFound();

        _context.Remove(filme);
        _context.SaveChanges();
        return NoContent();
    }
}