using System.Linq.Expressions;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using WebMusicaGrupoC.Models;

namespace WebMusicaGrupoC.Services.Repositorio
{
    public class EFGenericRepositorio<T> :IGenericRepositorio<T> where T : class
    {
        private readonly GrupoCContext _context = new();
        public List<T> DameTodos()
        {
            return _context.Set<T>().AsNoTracking().ToList();
        }

        public T? DameUno(int Id)
        {
            return _context.Set<T>().Find(Id);
        }

        public bool EliminarElemento(int Id)
        {
            var elemento=DameUno(Id);
            _context.Set<T>().Remove(elemento);
            _context.SaveChanges();
            return true;
        }

        public bool AgregarElemento(T elemento)
        {
            _context.Set<T>().Add(elemento);
            _context.SaveChanges();
            return true;
        }

        public void ModificarElemento(int Id, T elemento)
        {
            _context.Entry(elemento).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public List<T> Filtra(Expression<Func<T, bool>> predicado)
        {
            return _context.Set<T>().Where<T>(predicado).ToList();
        }
    }
}
