using System.Drawing.Text;
using Microsoft.EntityFrameworkCore;
using WebMusicaGrupoC.Models;

namespace WebMusicaGrupoC.Services.Repositorio
{
    public class EFGruposRepositorio : IGruposRepositorio
    {
        private readonly GrupoCContext _context = new();
        public bool AgregarElemento(Grupos grupo)
        {
            if (DameUno(grupo.Id) != null)
            {
                return false;
            }
            else
            {
                _context.Grupos.Add(grupo);
                _context.SaveChanges();
                return true;
            }
        }
        public List<Grupos> DameTodos()
        {

            return _context.Grupos.AsNoTracking().ToList();
        }

        public Grupos? DameUno(int Id)
        {
            return _context.Grupos.Find(Id);
        }

        public bool EliminarElemento(int Id)
        {
            if (DameUno(Id) != null)
            {
                _context.Grupos.Remove(DameUno(Id));
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ModificarElemento(/*int Id,*/ Grupos grupo)
        {
            //var recupera = DameUno(Id);
            //if (recupera != null)
            //{
            //    EliminarElemento(Id);
            //}
            //AgregarElemento(grupo);

            _context.Grupos.Update(grupo);
            _context.SaveChangesAsync();
        }
    }
}
