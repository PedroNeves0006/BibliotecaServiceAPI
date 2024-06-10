using BibliotecaServiceAPI.Data;
using BibliotecaServiceAPI.Models;
using BibliotecaServiceAPI.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaServiceAPI.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        
            private readonly BibliotecaServiceAPIDBContext _dbContext;

            public UsuarioRepositorio(BibliotecaServiceAPIDBContext bibliotecaAPIDBContext)
            {
                _dbContext = bibliotecaAPIDBContext;
            }

            public async Task<List<UsuarioModel>> BuscarTodosUsuarios()
            {
                return await _dbContext.Usuarios.ToListAsync();

            }
            public async Task<UsuarioModel> BuscarPorId(int id)
            {
                return await _dbContext.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
            }

            public async Task<UsuarioModel> Adicionar(UsuarioModel usuario)
            {
                _dbContext.Usuarios.Add(usuario);
                _dbContext.SaveChanges();
                return usuario;
            }

            public async Task<UsuarioModel> Atualizar(UsuarioModel usuario, int id)
            {
                UsuarioModel usuarioPorId = await BuscarPorId(id);

                if (usuarioPorId == null)
                {
                    throw new Exception($"Usuário para o Id: {id} não foi encontrado no banco de dados");
                }

                usuarioPorId.Nome = usuario.Nome;
                usuarioPorId.Email = usuario.Email;

                _dbContext.Usuarios.Update(usuarioPorId);
                _dbContext.SaveChanges();
                return usuarioPorId;
            }

            public async Task<bool> Apagar(int id)
            {
                UsuarioModel usuarioPorId = await BuscarPorId(id);

                if (usuarioPorId == null)
                {
                    throw new Exception($"Usuário para o Id: {id} não foi encontrado no banco de dados");
                }

                _dbContext.Usuarios.Remove(usuarioPorId);
                _dbContext.SaveChanges();
                return true;
            }
        }
    }
