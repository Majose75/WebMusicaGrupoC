using System.Linq.Expressions;
using WebMusicaGrupoC.Models;

namespace WebMusicaGrupoC.Services.Repositorio
{
    public interface IGenericRepositorio<T> where T: class
    {
        Task<List<T>> DameTodos();
        Task<T?> DameUno(int? id);
        Task<bool> EliminarElemento(int id);
        Task<bool> AgregarElemento(T elemento);
        void ModificarElemento(int id, T elemento);
        Task<List<T>> Filtra(Expression<Func<T, bool>> predicado);
    }
}
