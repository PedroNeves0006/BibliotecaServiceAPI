using BibliotecaServiceAPI.Data;
using BibliotecaServiceAPI.Models;
using BibliotecaServiceAPI.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaServiceAPI.Repositorios
{
    public class EmprestimoRepositorio : IEmprestimoRepositorio
    {
        private readonly BibliotecaServiceAPIDBContext _dbContext;

        public EmprestimoRepositorio(BibliotecaServiceAPIDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<EmprestimoModel>> BuscarTodosEmprestimos()
        {
            return await _dbContext.Emprestimos
                .Include(x => x.Livro)
                .ToListAsync();
        }

        public async Task<EmprestimoModel> BuscarPorId(int id)
        {
            return await _dbContext.Emprestimos
                .Include(x => x.Livro)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<EmprestimoModel> Adicionar(EmprestimoModel emprestimo)
        {
            
            var livro = await _dbContext.Livros.FindAsync(emprestimo.LivroId);

            if (livro == null)
            {
                throw new Exception("Livro não encontrado");
            }

            var status = livro.Status;

            if (status != Enums.StatusLivro.Disponivel)
            {
                throw new Exception($"Livro indisponível para empréstimo");
            }
            else
            {
                _dbContext.Emprestimos.Add(emprestimo);
                await _dbContext.SaveChangesAsync();
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

            _dbContext.Emprestimos.Update(emprestimoPorId);
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

            _dbContext.Emprestimos.Remove(emprestimoPorId);
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
            _dbContext.Emprestimos.Update(emprestimo);
            await _dbContext.SaveChangesAsync();

            return emprestimo;
        }
    }
}
