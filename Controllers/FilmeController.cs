sing AutoMapper;
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

    [HttpPost]
    public IActionResult AddFilme([FromBody]CreateFilmeDto createFilmeDto)
    {
        Filme filme = _mapper.Map<Filme>(createFilmeDto);
        _context.Filmes.Add(filme);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetFilmeById), new {id = filme.Id}, filme);
    }

    [HttpGet]
    public IEnumerable<ReadFilmeDto> GetAllFilmes([FromQuery] int skip, [FromQuery] int take = 10)
    {
        return _mapper.Map<List<ReadFilmeDto>>(_context.Filmes.Skip(skip).Take(take));
    }

    [HttpGet("{id}")]
    public IActionResult GetFilmeById(int Id)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == Id);
        var filmeDto = _mapper.Map<ReadFilmeDto>(filme);
        return filme == null ? NotFound() : Ok(filmeDto);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateFilme(int Id, [FromBody]UpdateFilmeDto updateFilmeDto)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == Id);
        
        if (filme == null) return NotFound();
        
        _mapper.Map(updateFilmeDto, filme);
        _context.SaveChanges();
        
        return filme == null ? NotFound() : Ok(filme);
        //return NoContent();
    }

    [HttpPatch("{id}")]
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

    [HttpDelete("{id}")]
    public IActionResult DeleteFilme(int Id){

        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == Id);
        if(filme == null) return NotFound();

        _context.Remove(filme);
        _context.SaveChanges();
        return NoContent();
    }
}