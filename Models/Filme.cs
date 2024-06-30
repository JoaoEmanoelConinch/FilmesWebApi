using System.ComponentModel.DataAnnotations;

namespace FilmesWebApi.Models
{
    public class Filme
    {
        [Key]
        [Required]
        public int Id {get;set;}

        [Required(ErrorMessage = "Titulo é obrigadtorio")]
        [MaxLength(100, ErrorMessage ="o maximo de caracteres do Titulo é 100")]
        public string Titulo {get; set;}
        
        [Required(ErrorMessage = "Tempo é obrigatorio")]
        [Range(70, 600, ErrorMessage = "o tempo de duração de filme deve estar entre 70 e 600 minutos")]
        public int Tempo {get; set;}
        
        [Required(ErrorMessage = "Genero é obrigatorio")]
        [MaxLength(50, ErrorMessage = "o maximo de caracteres do Genero é 50")]
        public string Genero {get; set;}

        [Required(ErrorMessage = "Diretor é obrigadtorio")]
        public string Diretor {get; set;}

    }
}