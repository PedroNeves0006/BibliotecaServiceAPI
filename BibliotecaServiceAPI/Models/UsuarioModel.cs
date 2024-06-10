namespace BibliotecaServiceAPI.Models
{
    public interface UsuarioModel
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
    }
}
