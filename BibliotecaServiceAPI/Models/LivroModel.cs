using BibliotecaServiceAPI.Enums;

namespace BibliotecaServiceAPI.Models
{
    public class LivroModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string? Isbn { get; set; }
        public int Ano { get; set; }
        public StatusLivro Status { get; set; }
       
    }
}
