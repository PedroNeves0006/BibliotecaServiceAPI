using BibliotecaServiceAPI.Data;
using BibliotecaServiceAPI.Models;
using BibliotecaServiceAPI.Repositorios.Interfaces;

namespace BibliotecaServiceAPI.Repositorios
{
    public class EmprestimoRepositorio : IEmprestimoRepositorio
    {
        private readonly BibliotecaAPIDBContext _dbContext;

        public EmprestimoRepositorio(BibliotecaAPIDBContext bibliotecaAPIDBContext)
        {
            _dbContext = bibliotecaAPIDBContext;
        }
        public async Task<List<EmprestimoModel>> BuscarTodosEmprestimos()
        {
            return await _dbContext.Emprestimo
                .Include(x => x.Livro)
                .ToListAsync();
        }

        public async Task<EmprestimoModel> BuscarPorId(int id)
        {
            return await _dbContext.Emprestimo
                .Include(x => x.Livro)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<EmprestimoModel> Adicionar(EmprestimoModel emprestimo, EmprestimoModel status)
        {
            if (emprestimo.Status == Enum.StatusLivro.Disponivel)
            {
                _dbContext.Emprestimo.Add(emprestimo);
                _dbContext.SaveChanges();
            }
            else
            {
                throw new Exception($"Livro indisponível para empréstimo");
            }

            return emprestimo;
        }

        public async Task<EmprestimoModel> Atualizar(EmprestimoModel emprestimo, int id)
        {
            EmprestimoModel emprestimoPorId = await BuscarPorId(id);

            if (emprestimoPorId == null)
            {
                throw new Exception($"Empréstimo para o Id: {id} não foi encontrado no banco de dados");
            }

            emprestimoPorId.DataEmprestimo = emprestimo.DataEmprestimo;
            emprestimoPorId.DataDevolucao = emprestimo.DataDevolucao;

            _dbContext.Emprestimo.Update(emprestimoPorId);
            _dbContext.SaveChanges();
            return emprestimoPorId;
        }
        public async Task<bool> Apagar(int id)
        {
            EmprestimoModel emprestimoPorId = await BuscarPorId(id);

            if (emprestimoPorId == null)
            {
                throw new Exception($"Emprestimo para o Id: {id} não foi encontrado no banco de dados");
            }

            _dbContext.Emprestimo.Remove(emprestimoPorId);
            _dbContext.SaveChanges();
            return true;
        }

        public async Task<EmprestimoModel> RegistrarDevolucao(int id)
        {
            var emprestimo = await BuscarPorId(id);

            if (emprestimo == null)
            {
                throw new Exception("Empréstimo não encontrado.");
            }

            if (emprestimo.Devolvido == true)
            {
                throw new Exception("Este livro já foi devolvido.");
            }

            var hoje = DateTime.Now;

            if (hoje > emprestimo.DataDevolucao)
            {
                throw new Exception("A data de devolução está expirada.");
            }

            emprestimo.Devolvido = true;
            _dbContext.Emprestimo.Update(emprestimo);
            await _dbContext.SaveChangesAsync();

            return emprestimo;
        }
    }
}
