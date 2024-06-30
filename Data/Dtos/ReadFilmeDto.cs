using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesWebApi.Data.Dtos;

public class ReadFilmeDto
{
    public int Id {get;set;}
    public string Titulo {get; set;}
    public int Tempo {get; set;}
    public string Genero {get; set;}
    public string Diretor {get; set;}

    public DateTime RequestTime {get; set;} = DateTime.Now;
}