using BibliotecaServiceAPI.Data;
using BibliotecaServiceAPI.Models;
using BibliotecaServiceAPI.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaServiceAPI.Repositorios
{
    public class LivroRepositorio : ILivroRepositorio
    {
       
            private readonly BibliotecaServiceAPIDBContext _dbContext;

            public LivroRepositorio(BibliotecaServiceAPIDBContext bibliotecaAPIDBContext)
            {
                _dbContext = bibliotecaAPIDBContext;
            }
            public async Task<List<LivroModel>> BuscarTodosLivros()
            {
                return await _dbContext.Livros.ToListAsync();

            }
            public async Task<LivroModel> BuscarPorId(int id)
            {
                return await _dbContext.Livros.FirstOrDefaultAsync(x => x.Id == id);
            }

            public async Task<LivroModel> Adicionar(LivroModel livro)
            {
                _dbContext.Livros.Add(livro);
                _dbContext.SaveChanges();
                return livro;
            }

            public async Task<LivroModel> Atualizar(LivroModel livro, int id)
            {
                LivroModel livroPorId = await BuscarPorId(id);

                if (livroPorId == null)
                {
                    throw new Exception($"Livro para o Id: {id} não foi encontrado no banco de dados");
                }

                livroPorId.Titulo = livro.Titulo;
                livroPorId.Autor = livro.Autor;
                livroPorId.Ano = livro.Ano;
                livroPorId.Isbn = livro.Isbn;

                _dbContext.Livros.Update(livroPorId);
                _dbContext.SaveChanges();
                return livroPorId;
            }

            public async Task<bool> Apagar(int id)
            {
                LivroModel livroPorId = await BuscarPorId(id);

                if (livroPorId == null)
                {
                    throw new Exception($"Livro para o Id: {id} não foi encontrado no banco de dados");
                }

                _dbContext.Livros.Remove(livroPorId);
                _dbContext.SaveChanges();
                return true;
            }
        }
    }
