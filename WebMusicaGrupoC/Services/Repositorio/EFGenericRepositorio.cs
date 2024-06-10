using System.Linq.Expressions;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using WebMusicaGrupoC.Models;

namespace WebMusicaGrupoC.Services.Repositorio
{
    public class EFGenericRepositorio<T> :IGenericRepositorio<T> where T : class
    {
        private readonly GrupoCContext _context = new();
        public async Task<List<T>> DameTodos()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> DameUno(int Id)
        {
            return  await _context.Set<T>().FindAsync(Id);
        } 

        public async Task<bool> EliminarElemento(int Id)
        {
            var elemento=DameUno(Id);
            _context.Set<T>().Remove(elemento as T);
             _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AgregarElemento(T elemento)
        {
            _context.Set<T>().AddAsync(elemento);
            _context.SaveChangesAsync();
            return true;
        }

        public void ModificarElemento(int Id, T elemento)
        {
            _context.Entry(elemento).State = EntityState.Modified;
            _context.SaveChangesAsync();
        }

        public async Task<List<T>> Filtra(Expression<Func<T, bool>> predicado)
        {
            return await _context.Set<T>().Where<T>(predicado).ToListAsync();
        }
    }
}
