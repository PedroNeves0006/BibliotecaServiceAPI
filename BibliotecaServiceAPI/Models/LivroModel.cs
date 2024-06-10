namespace BibliotecaServiceAPI.Models
{
    public class LivroModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string? Isbn { get; set; }
        public int? Ano { get; set; }
        public Enum.StatusLivro Status { get; set; }
        public int? UsuarioId { get; set; }
        public virtual UsuarioModel? Usuario { get; set; }
    }
}
