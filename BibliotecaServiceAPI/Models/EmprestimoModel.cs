namespace BibliotecaServiceAPI.Models
{
    public class EmprestimoModel
    {
        public int Id { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime DataDevolucao { get; set; }
        public int LivroId { get; set; }
        public virtual LivroModel? Livro { get; set; }
        public bool Devolvido { get; set; }
        public Enum.StatusLivro Status { get; set; }
    }
}
