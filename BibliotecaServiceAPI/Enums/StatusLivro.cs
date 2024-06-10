using System.ComponentModel;

namespace BibliotecaServiceAPI.Enums
{
    public enum StatusLivro
    {
        [Description("Livro disponível")]
        Disponivel = 1,
        [Description("Livro está alugado")]
        Indisponivel = 2
    }
}
