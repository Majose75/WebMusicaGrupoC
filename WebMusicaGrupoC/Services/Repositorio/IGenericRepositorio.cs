using System.Linq.Expressions;
using WebMusicaGrupoC.Models;

namespace WebMusicaGrupoC.Services.Repositorio
{
    public interface IGenericRepositorio<T> where T: class
    {
        Task<List<T>> DameTodos();
        Task<T?> DameUno(int Id);
        Task<bool> EliminarElemento(int Id);
        Task<bool> AgregarElemento(T elemento);
        void ModificarElemento(int Id, T elemento);
        Task<List<T>> Filtra(Expression<Func<T, bool>> predicado);
    }
}
