using System.Linq.Expressions;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using WebMusicaGrupoC.Models;

namespace WebMusicaGrupoC.Services.Repositorio
{
    public class EfGenericRepositorio<T> :IGenericRepositorio<T> where T : class
    {
        private readonly GrupoCContext _context = new();
        public async Task<List<T>> DameTodos()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> DameUno(int? id)
        {
            return  await _context.Set<T>().FindAsync(id);
        } 

        public async Task<bool> EliminarElemento(int id)
        {
            var elemento = await DameUno(id);
            if (elemento != null)
            {
                _context.Set<T>().Remove(elemento);
                await _context.SaveChangesAsync();
            }

            return true;
        }

        public async Task<bool> AgregarElemento(T elemento)
        {
            await _context.Set<T>().AddAsync(elemento);
            await _context.SaveChangesAsync();
            return true;
        }

        public async void ModificarElemento(int id, T elemento)
        {
              _context.Entry(elemento).State = EntityState.Modified;
             await _context.SaveChangesAsync();
        }

        public async Task<List<T>> Filtra(Expression<Func<T, bool>> predicado)
        {
            return await _context.Set<T>().Where<T>(predicado).ToListAsync();
        }
    }
}
