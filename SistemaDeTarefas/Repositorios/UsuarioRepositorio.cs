using Microsoft.EntityFrameworkCore;
using SistemaDeTarefas.Data;
using SistemaDeTarefas.Models;
using SistemaDeTarefas.Repositorios.Interfaces;

namespace SistemaDeTarefas.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly SistemaDeTarefasDBContext _dbContext;
        public UsuarioRepositorio(SistemaDeTarefasDBContext sistemaDeTarefasDBContext)
        {
            _dbContext = sistemaDeTarefasDBContext;
        }
        public async Task<List<UsuarioModel>> BuscarTodosUsuarios()
        {
            return await _dbContext.Usuarios.ToListAsync();
        }

        public async Task<UsuarioModel> BuscarUsuarioById(int id)
        {
            return await _dbContext.Usuarios.FirstOrDefaultAsync(x => x.id == id);
        }
        public async Task<UsuarioModel> Adicionar(UsuarioModel usuario)
        {
            await _dbContext.Usuarios.AddAsync(usuario);
            await _dbContext.SaveChangesAsync();
            return usuario;
        }
        public async Task<UsuarioModel> Atualizar(UsuarioModel usuario, int id)
        {
            UsuarioModel usuarioModelById =  await BuscarUsuarioById(id);
           if(usuarioModelById == null)
            {
                throw new Exception($"Usuário com Id: {id} selecionado não existe");
            }
           
           usuarioModelById.Nome = usuario.Nome;
           usuarioModelById.Email = usuario.Email;


            _dbContext.Usuarios.Update(usuarioModelById);
            await _dbContext.SaveChangesAsync();
            return usuarioModelById;
        }

        public async Task<bool> Apagar(int id)
        {
            UsuarioModel usuarioModelById = await BuscarUsuarioById(id);
            if (usuarioModelById == null)
            {
                throw new Exception($"Usuário com Id: {id} selecionado não existe");
            }
            _dbContext.Usuarios.Remove(usuarioModelById);
            await _dbContext.SaveChangesAsync();
            return true;
        }



    }
}
