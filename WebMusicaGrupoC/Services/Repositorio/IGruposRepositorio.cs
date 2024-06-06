using WebMusicaGrupoC.Models;

namespace WebMusicaGrupoC.Services.Repositorio
{
    public interface IGruposRepositorio
    {
        List<Grupos> DameTodos();
        Grupos? DameUno(int Id);
        bool EliminarElemento(int Id);
        bool AgregarElemento(Grupos grupo);
        void ModificarElemento(/*int Id,*/ Grupos grupo);
    }
}
