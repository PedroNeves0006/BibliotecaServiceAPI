using BibliotecaServiceAPI.Models;

namespace BibliotecaServiceAPI.Repositorios.Interfaces
{
    public interface IEmprestimoRepositorio
    {
        Task<List<EmprestimoModel>> BuscarTodosEmprestimos();
        Task<EmprestimoModel> BuscarPorId(int id);
        Task<EmprestimoModel> Adicionar(EmprestimoModel emprestimo, EmprestimoModel status);
        Task<EmprestimoModel> Atualizar(EmprestimoModel emprestimo, int id);
        Task<bool> Apagar(int id);
        Task<EmprestimoModel> RegistrarDevolucao(int id);
    }
}
