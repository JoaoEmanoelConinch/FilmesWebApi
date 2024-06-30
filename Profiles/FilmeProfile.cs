using AutoMapper;
using FilmesWebApi.Data.Dtos;
using FilmesWebApi.Models;

namespace FilmesWebApi.Profiles;

public class FilmeProfile : Profile
{

    public FilmeProfile()
    {
        CreateMap<CreateFilmeDto, Filme>();
        CreateMap<UpdateFilmeDto, Filme>();
        CreateMap<Filme, UpdateFilmeDto>();
        CreateMap<Filme, ReadFilmeDto>();
    }

}
