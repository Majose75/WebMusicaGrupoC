using Microsoft.EntityFrameworkCore;
using WebMusicaGrupoC.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebMusicaGrupoC.Services.Repositorio
{
    public class FakeGruposRepositorio: IGruposRepositorio
    {
        private List<Grupos> listaDeGrupos  = new();

        public FakeGruposRepositorio()
        {
            Grupos miGrupo = new()
            {
                Id = 1,
                Nombre = "Grupo Fake 01"
            };
            listaDeGrupos.Add(miGrupo);
            miGrupo = new()
            {
                Id = 2,
                Nombre = "Grupo Fake 02"
            };
            listaDeGrupos.Add(miGrupo);
            miGrupo = new()
            {
                Id = 3,
                Nombre = "Grupo Fake 03"
            };
            listaDeGrupos.Add(miGrupo);
            miGrupo = new()
            {
                Id = 4,
                Nombre = "Grupo Fake 04"
            };
            listaDeGrupos.Add(miGrupo);
            miGrupo = new()
            {
                Id = 5,
                Nombre = "Grupo Fake 05"
            };
            listaDeGrupos.Add(miGrupo);
        }

        public List<Grupos> DameTodos()
        {
            return this.listaDeGrupos;
        }

        public Grupos? DameUno(int Id)
        {
            return this.listaDeGrupos.FirstOrDefault(x => x.Id==Id);
        }

        public bool EliminarElemento(int Id)
        {
            return listaDeGrupos.Remove(DameUno(Id));
        }

        public bool AgregarElemento(Grupos grupo)
        {
           this.listaDeGrupos.Add(grupo);
           return true;
        }

        public void ModificarElemento(Grupos grupo)
        {
            EliminarElemento(grupo.Id);
            AgregarElemento(grupo);
        }
    }
}
